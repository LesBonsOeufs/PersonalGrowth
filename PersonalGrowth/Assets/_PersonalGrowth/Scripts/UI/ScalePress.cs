using DG.Tweening;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.UI {
    public class ScalePress : PressFeedback
    {
        [SerializeField] private Vector3 pressedAddedScale;

        private Vector3 initScale;

        private void Awake()
        {
            initScale = transform.localScale;
        }

        protected override void PressedDown()
        {
            transform.DOKill();
            transform.DOScale(initScale + pressedAddedScale, 0.6f).SetEase(Ease.OutBounce);
        }

        protected override void PressedUp()
        {
            transform.DOKill();
            transform.DOScale(initScale, 0.25f).SetEase(Ease.OutCubic);
        }
    }
}
