using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Com.GabrielBernabeu.Common.CustomButtons {
    public class CustomJuicyToggle : CustomToggle, IPointerDownHandler
    {
        [SerializeField] private Image imageRenderer = default;

        [Header("Pressed Animation")]
        [SerializeField] private Color pressedColor = Color.grey;
        [SerializeField] private float pressedAnimDuration = 0.1f;

        [Header("Activated Animation")]
        [SerializeField] private Vector3 activatedScale = new Vector3(1.1f, 1.1f, 1.1f);
        [SerializeField] private Color activatedColor = Color.green;
        [SerializeField] private float activatedAnimDuration = 0.35f;

        private Vector3 initScale;
        protected Color initColor;

        private void Awake()
        {
            initScale = transform.localScale;
            initColor = imageRenderer.color;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            imageRenderer.DOColor(Color.Lerp(pressedColor, imageRenderer.color, 0.5f), pressedAnimDuration);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            imageRenderer.DOColor(initColor, pressedAnimDuration);
            base.OnPointerUp(eventData);
        }

        protected override void OnValueChangedInvocation()
        {
            base.OnValueChangedInvocation();
            DOTween.Kill(transform, true);

            if (IsSwitchedOn)
            {
                DOTween.Sequence(transform)
                    .Append(transform.DOScale(activatedScale, activatedAnimDuration))
                    .Join(imageRenderer.DOColor(activatedColor, activatedAnimDuration));
            }
            else
            {
                DOTween.Sequence(transform)
                    .Append(transform.DOScale(initScale, activatedAnimDuration))
                    .Join(imageRenderer.DOColor(initColor, activatedAnimDuration));
            }
        }
    }
}
