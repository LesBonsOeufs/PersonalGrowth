///-----------------------------------------------------------------
///   Author : Gabriel Bernabeu                    
///   Date   : 25/10/2022 09:34
///-----------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

public delegate void MetronomeEventHandler(Metronome sender);
public delegate void MetronomeUpdateEventHandler(Metronome sender, float audioDeltaTime);

[RequireComponent(typeof(MusicManager))]
public class Metronome : MonoBehaviour
{
    [SerializeField] private float tickGain = 0.5f;
    [SerializeField] private float accentAmplifierScale = 1.5f;
    [SerializeField] private int signatureHi = 4;
    [SerializeField] private int signatureLo = 4;

    [System.NonSerialized] public bool playBeats = false;
    [System.NonSerialized] public bool playEvents = false;

    private double nextTick = 0f;
    private float amp = 0f;
    private float phase = 0f;
    private double sampleRate = 0f;

    private int accentCounter;
    private double lastDspTime;
    private double nSamplesSinceLastTick = 0f;

    /// <summary>
    /// For audioDeltaTime, listen to OnUpdate event.
    /// </summary>
    public static float UsualDeltaTime => Time.deltaTime;

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }

        set
        {
            _isRunning = value;

            if (_isRunning)
                PrepareRun();
        }
    }
    private bool _isRunning = false;

    public float DurationBetweenTicks { get; private set; }
    public float Ratio { get; private set; }

    public event MetronomeEventHandler OnTick;
    private int onTickEventCounter = 0;

    public event MetronomeUpdateEventHandler OnUpdate;
    private List<float> audioDeltaTimes = new List<float>();

    public static event MetronomeEventHandler OnRunPrepared;

    public static Metronome Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        accentCounter = signatureHi;
    }

    private void PrepareRun()
    {
        DurationBetweenTicks = 60f / MusicManager.Instance.BPM * (4f / signatureLo);
        sampleRate = AudioSettings.outputSampleRate;

        lastDspTime = AudioSettings.dspTime;
        nextTick = lastDspTime * sampleRate
                   + DurationBetweenTicks * sampleRate - nSamplesSinceLastTick;
        
        OnRunPrepared?.Invoke(this);
    }

    public void ResetValues()
    {
        nSamplesSinceLastTick = 0f;
    }

#if !UNITY_WEBGL

    private void Update()
    {
        while (onTickEventCounter > 0)
        {
            OnTick?.Invoke(this);
            onTickEventCounter--;
        }

        while (audioDeltaTimes.Count > 0)
        {
            OnUpdate?.Invoke(this, audioDeltaTimes[0]);
            audioDeltaTimes.RemoveAt(0);
        }
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        if (!IsRunning)
            return;

        double lSamplesPerTick = sampleRate * DurationBetweenTicks;
        double lDspTime = AudioSettings.dspTime;
        double lCurrentSample = lDspTime * sampleRate;
        int lTrueDataLength = data.Length / channels;
        float lAddedWaveValue;

        for (int i = 0; i < lTrueDataLength; i++)
        {
            if (playBeats)
            {
                lAddedWaveValue = tickGain * amp * Mathf.Sin(phase);

                for (int j = 0; j < channels; j++)
                    data[i * channels + j] += lAddedWaveValue;
            }

            while (lCurrentSample + i >= nextTick)
            {
                nextTick += lSamplesPerTick;
                amp = 1f;

                if (++accentCounter > signatureHi)
                {
                    accentCounter = 1;
                    amp *= accentAmplifierScale;
                }

                if (playEvents)
                    onTickEventCounter++;

                nSamplesSinceLastTick -= lSamplesPerTick;

                if (nSamplesSinceLastTick < 0f)
                    nSamplesSinceLastTick = 0f;
            }

            phase += amp * 0.3f;
            amp *= 0.993f;
        }

        double lLastSample = lastDspTime * sampleRate;
        nSamplesSinceLastTick += lCurrentSample - lLastSample;
        Ratio = Mathf.Clamp01((float)(nSamplesSinceLastTick / lSamplesPerTick));

        if (playEvents)
            audioDeltaTimes.Add((float)(lDspTime - lastDspTime));

        lastDspTime = lDspTime;
    }

#else

    private void Update()
    {
        if (!IsRunning)
            return;

        double lSamplesPerTick = sampleRate * DurationBetweenTicks;
        double lDspTime = AudioSettings.dspTime;
        double lCurrentSample = lDspTime * sampleRate;

        while (lCurrentSample >= nextTick)
        {
            nextTick += lSamplesPerTick;
            amp = 1f;

            if (++accentCounter > signatureHi)
            {
                accentCounter = 1;
                amp *= accentAmplifierScale;
            }

            if (playEvents)
                OnTick?.Invoke(this);

            nSamplesSinceLastTick -= lSamplesPerTick;

            if (nSamplesSinceLastTick < 0f)
                nSamplesSinceLastTick = 0f;
        }
            
        double lLastSample = lastDspTime * sampleRate;
        nSamplesSinceLastTick += lCurrentSample - lLastSample;
        Ratio = Mathf.Clamp01((float)(nSamplesSinceLastTick / lSamplesPerTick));

        if (playEvents)
            OnUpdate?.Invoke(this, (float)(lDspTime - lastDspTime));

        lastDspTime = lDspTime;
    }

#endif

    private void OnDestroy()
    {
        if (Instance != this)
            return;

        OnTick = null;
        OnUpdate = null;
    }
}