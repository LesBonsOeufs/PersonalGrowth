using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine;

///-----------------------------------------------------------------
/// Author : Gabriel Bernabeu
/// Date : 20/02/2022 17:59
///-----------------------------------------------------------------
namespace Com.GabrielBernabeu.Common.CustomButtons {
    public class CustomBouncyButton : CustomBasicButton
    {
        [SerializeField] private float bounceForce = 1.35f;
        [SerializeField] private float bounceDuration = 0.4f;
        [SerializeField] private float resetDuration = 0.2f;

        private Vector3 initScale;

        private void Awake()
        {
            initScale = transform.localScale;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            DOTween.Kill(transform, true);
            transform.DOScale(initScale * bounceForce, bounceDuration).SetEase(Ease.OutBounce);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            DOTween.Kill(transform);
            transform.DOScale(initScale, resetDuration);
        }
    }
}
