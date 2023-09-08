using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle {
    public class EquippedWeapon : MonoBehaviour
    {
        [SerializeField] private Collider2D strikeCollider = default;

        [Foldout("Visuals"), SerializeField] private float sideChangeDuration = 0.5f;
        [Foldout("Visuals"), SerializeField, Range(-1f, 0f)] private float sideChangeYShift = -0.5f;
        [Foldout("Visuals"), SerializeField] private float strikeDuration = 0.3f;
        [Foldout("Visuals"), SerializeField, Range(0f, 1f)] private float strikeYShift = 0.3f;
        [Foldout("Visuals"), SerializeField] private int restOpeningAngle = 80;
        [Foldout("Visuals"), SerializeField] private int strikeOpeningAngle = 20;

        [ReadOnly] public WeaponInfo info;

        private Tween sideMoveTween;
        private Tween sideRotateTween;
        private Tween strikeTween;

        public float RestAngle => Side == EScreenSide.LEFT ? restOpeningAngle : -restOpeningAngle;
        public float StrikeAngle => Side == EScreenSide.LEFT ? strikeOpeningAngle : -strikeOpeningAngle;

        public EScreenSide Side
        {
            get
            {
                return _side;
            }

            set
            {
                if (_side == value)
                    return;

                _side = value;
                SideAnim();
            }
        }
        private EScreenSide _side;

        private void Awake()
        {
            strikeCollider.enabled = false;
        }

        public async void Strike()
        {
            sideMoveTween.Kill(true);
            sideRotateTween.Kill(true);
            strikeCollider.enabled = true;
            strikeTween = DOTween.Sequence(transform)
                .Append(transform.DOLocalMove(Vector3.up * strikeYShift, strikeDuration))
                .Join(transform.DORotate(new Vector3(0f, 0f, StrikeAngle), strikeDuration))
                .SetEase(Ease.OutBack)
                .SetUpdate(UpdateType.Fixed);

            await strikeTween.AsyncWaitForCompletion();

            strikeCollider.enabled = false;
            SideAnim();
        }

        private void SideAnim()
        {
            float lHalfTweenDuration = sideChangeDuration * 0.5f;

            sideMoveTween?.Kill();
            sideRotateTween?.Kill();

            sideRotateTween = transform.DORotate(new Vector3(0f, 0f, RestAngle), sideChangeDuration).SetEase(Ease.InOutSine);
            sideMoveTween = DOTween.Sequence(transform)
                .Append(transform.DOLocalMove(Vector3.up * sideChangeYShift, lHalfTweenDuration).SetEase(Ease.InOutSine))
                .Append(transform.DOLocalMove(Vector3.zero, lHalfTweenDuration).SetEase(Ease.InOutSine));
        }

        //Strike landed
        private void OnCollisionEnter2D(Collision2D collision)
        {
            //Prevent striking multiple times
            strikeCollider.enabled = false;
            strikeTween.Kill();
        }
    }
}