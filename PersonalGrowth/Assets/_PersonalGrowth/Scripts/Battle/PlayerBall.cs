using DG.Tweening;
using System.Xml.Schema;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle 
{
    public delegate void PlayerBallEventHandler(PlayerBall sender);

    [RequireComponent(typeof(DragShoot))]
    public class PlayerBall : MonoBehaviour
    {
        [SerializeField, Tooltip("Once the ball is slower than this, it dies")] 
        private float maxDeathSpeed = 1f;

        private bool shot = false;
        private new Rigidbody rigidbody;

        public event PlayerBallEventHandler OnDeath;

        private void Start()
        {
            GetComponent<DragShoot>().OnShoot += DragShoot_OnShoot;
            rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (!shot)
                return;

            if (rigidbody.velocity.magnitude <= maxDeathSpeed)
                Die();
        }

        private void DragShoot_OnShoot(DragShoot sender)
        {
            sender.enabled = false;
            shot = true;
        }

        private void Die()
        {
            enabled = false;
            transform.DOScale(0f, 0.4f).SetEase(Ease.OutCubic)
                .OnComplete(() =>
                {
                    Destroy(gameObject);
                    OnDeath?.Invoke(this);
                });
        }

        private void OnDestroy()
        {
            OnDeath = null;
        }
    }
}
