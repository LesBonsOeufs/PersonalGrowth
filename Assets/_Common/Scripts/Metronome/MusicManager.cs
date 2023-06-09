///-----------------------------------------------------------------
///   Author : Gabriel Bernabeu                    
///   Date   : 06/10/2022 16:38
///-----------------------------------------------------------------

using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [SerializeField] private bool useMusicBpm = false;
    [SerializeField] private int customBpm = 60;
    [SerializeField] private List<MusicInfo> levelMusics = default;
    [SerializeField] private float musicSwitchFadeOut = 1f;
    [SerializeField] private float musicSwitchFadeIn = 0.5f;

    private MusicInfo musicInfo;
    private AudioSource audioSource;
    private int musicIndex = 0;

    public int CurrentMusicTotalTicks { get; private set; }
    public float BPM { get; private set; }
    public float CurrentMusicDefaultBPM => musicInfo.BPM;

    public static MusicManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        Instance = this;
    }

    private void Start()
    {
        musicIndex = 0;
        SetInfos(levelMusics[musicIndex]);
    }

    public void FadeToNext()
    {
        int lNewMusicIndex = musicIndex + 1;

        if (lNewMusicIndex < levelMusics.Count)
        {
            musicIndex = lNewMusicIndex;
            Stop(musicSwitchFadeOut, OnSwitchFadeOutComplete);
        }
    }

    private void OnSwitchFadeOutComplete()
    {
        SetInfos(levelMusics[musicIndex]);
        Play(musicSwitchFadeIn);
    }

    private void SetInfos(MusicInfo newInfos)
    {
        musicInfo = newInfos;
        audioSource.clip = musicInfo.Clip;

        if (!useMusicBpm)
            SetBPM(customBpm);
        else
            SetBPM(CurrentMusicDefaultBPM);

        CurrentMusicTotalTicks = (int)(BPM * (audioSource.clip.length / 60f));
    }

    private void SetBPM(float bpm)
    {
        BPM = bpm;
        audioSource.pitch = BPM / CurrentMusicDefaultBPM;
    }

    public void Play(float fadeInDuration = 0f)
    {
        audioSource.DOKill(true);
        float lBaseVolume = audioSource.volume;

        audioSource.Play();
        audioSource.volume = 0f;
        audioSource.DOFade(lBaseVolume, fadeInDuration);

        Metronome lInstance = Metronome.Instance;
        lInstance.ResetValues();
        lInstance.playBeats = true;
        lInstance.playEvents = true;
        lInstance.IsRunning = true;
    }

    public void Pause()
    {
        audioSource.DOKill(true);
        audioSource.Pause();
        Metronome.Instance.IsRunning = false;
    }

    public void Stop(float fadeOutDuration = 0f, UnityAction onComplete = null)
    {
        audioSource.DOKill(true);
        float lBaseVolume = audioSource.volume;
        Metronome.Instance.IsRunning = false;

        audioSource.DOFade(0f, fadeOutDuration).OnComplete(
            () => 
            {
                audioSource.Stop();
                audioSource.volume = lBaseVolume;
                onComplete?.Invoke();
            })
            .SetUpdate(true);
    }

    public void SetVolume(float newVolume, float fadeDuration = 0f, bool doKill = true)
    {
        audioSource.DOKill(true);
        audioSource.DOFade(newVolume, fadeDuration).SetUpdate(true);
    }

    private void OnDestroy()
    {
        if (Instance != this)
            return;

        audioSource.DOKill();
        Instance = null;
    }
}