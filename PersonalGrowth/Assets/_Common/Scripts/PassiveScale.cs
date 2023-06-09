using DG.Tweening;
using UnityEngine;

namespace Com.GabrielBernabeu.Common {
    public class PassiveScale : MonoBehaviour
    {
        [SerializeField]
        private Vector3 maxAddedScale = Vector3.zero;
        private Vector3 initScale;

        [SerializeField]
        private float loopDuration = 2f;

        private void Awake()
        {
            initScale = transform.localScale;
        }

        private void OnEnable()
        {
            RectTransform thisdude = GetComponent<RectTransform>();

            thisdude.DOScale(initScale + maxAddedScale, loopDuration * 2);

            DOTween.Sequence(transform).SetLoops(-1)
                .Append(transform.DOScale(initScale + maxAddedScale, loopDuration * 0.5f).SetEase(Ease.InOutSine))
                .Append(transform.DOScale(initScale, loopDuration * 0.5f).SetEase(Ease.InOutSine)).SetUpdate(true);
        }

        private void OnDisable()
        {
            DOTween.Kill(transform);
        }
    }
}
