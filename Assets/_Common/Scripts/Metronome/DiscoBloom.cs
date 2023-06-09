using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Volume))]
public class DiscoBloom : MonoBehaviour
{
    [SerializeField] private float onTickBloomIntensity = 0.2f;
    [SerializeField] private float bloomUpDuration = 0.1f;
    [SerializeField] private float bloomDownDuration = 0.3f;

    private float initBloomIntensity;
    private Bloom bloom;

    private void Start()
    {
        GetComponent<Volume>().profile.TryGet(out bloom);
        initBloomIntensity = bloom.intensity.value;

        Metronome.Instance.OnTick += Metronome_OnTick;
    }

    private void Metronome_OnTick(Metronome sender)
    {
        DOTween.Kill(bloom, true);

        DOTween.Sequence(bloom)
            .Append(DOVirtual.Float(initBloomIntensity, onTickBloomIntensity, bloomUpDuration, BloomTweenUpdate))
            .Append(DOVirtual.Float(onTickBloomIntensity, initBloomIntensity, bloomDownDuration, BloomTweenUpdate));
    }

    private void BloomTweenUpdate(float intensity)
    {
        bloom.intensity.value = intensity;
    }

    private void OnDestroy()
    {
        DOTween.Kill(bloom);

        if (Metronome.Instance != null)
            Metronome.Instance.OnTick -= Metronome_OnTick;
    }
}