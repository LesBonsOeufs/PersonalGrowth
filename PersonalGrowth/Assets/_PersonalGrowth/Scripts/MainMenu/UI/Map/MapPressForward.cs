using DG.Tweening;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.MainMenu.UI.Map 
{
    public class MapPressForward : PressDownUpFeedback
    {
        [SerializeField] private Vector3 pressedAddedScale;

        private Vector3 initScale;

        private void Awake()
        {
            initScale = transform.localScale;
        }

        protected override void OnPressDown()
        {
            transform.DOKill();
            transform.DOScale(initScale + pressedAddedScale, 0.6f).SetEase(Ease.OutBounce);
        }

        protected override void OnPressUp()
        {
            transform.DOKill();
            transform.DOScale(initScale, 0.25f).SetEase(Ease.OutCubic);
        }

        private void Update()
        {
            if (!IsPressed)
                return;

            Debug.Log("press");
        }
    }
}
