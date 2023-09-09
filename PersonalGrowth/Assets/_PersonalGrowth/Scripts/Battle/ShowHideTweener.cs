using DG.Tweening;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle {
    public class ShowHideTweener : MonoBehaviour
    {
        [SerializeField] private float showDuration = 0.5f;
        [SerializeField] private float hideDuration = 0.2f;

        private Tween showTween;

        public bool IsShown { get; private set; } = false;

        private void Start()
        {
            transform.localScale = Vector3.zero;
        }

        public void Show()
        {
            IsShown = true;
            showTween?.Kill();
            showTween = transform.DOScale(1f, showDuration).SetEase(Ease.OutBack);
        }

        public void Hide()
        {
            IsShown = false;
            showTween?.Kill();
            showTween = transform.DOScale(0f, hideDuration).SetEase(Ease.InSine);
        }
    }
}