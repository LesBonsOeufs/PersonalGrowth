using DG.Tweening;
using UnityEngine;

public class ScaleOnTick : MonoBehaviour
{
    [SerializeField] private float punchStrength = -0.037f;
    [SerializeField] private float punchDuration = 0.25f;

    private void Start()
    {
        Metronome.Instance.OnTick += Metronome_OnTick;
    }

    private void Metronome_OnTick(Metronome sender)
    {
        transform.DOKill(true);
        transform.DOPunchScale(Vector3.one * punchStrength, punchDuration, 0);
    }

    private void OnDestroy()
    {
        if (Metronome.Instance != null)
            Metronome.Instance.OnTick -= Metronome_OnTick;
    }
}
