using DG.Tweening;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.UI.PressFeedbacks {
    public class ScalePress : PressFeedback
    {
        [SerializeField] private Vector3 pressedAddedScale;

        private Vector3 initScale;
        private Tween currentTween;

        private void Awake()
        {
            initScale = transform.localScale;
        }

        protected override void PressedDown()
        {
            currentTween?.Kill();
            currentTween = transform.DOScale(initScale + pressedAddedScale, 0.2f).SetEase(Ease.OutCubic);
        }

        protected override void PressedUp()
        {
            currentTween?.Kill();
            currentTween = transform.DOScale(initScale, 0.25f).SetEase(Ease.OutCubic);
        }
    }
}
