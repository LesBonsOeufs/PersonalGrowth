using DG.Tweening;
using MoreMountains.Feedbacks;
using NaughtyAttributes;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.BilliardBattle {
    public class EnemyBall : Ball
    {
        [SerializeField] private UnitInfos infos = default;
        [SerializeField] private HealthDisplayer healthDisplayer = default;
        [SerializeField] private float minForceForDamage = 400f;
        [SerializeField] private new Renderer renderer = default;

        [Header("Tags")]
        [SerializeField, Tag] private string gameBoundsTag = default;
        [SerializeField, Tag] private string groundTag = default;

        [Header("Feedbacks")]
        [SerializeField] private MMF_Player hurtFeedbacks = default;
        [SerializeField] private MMF_Player deathFeedbacks = default;

        public int Health
        {
            get
            {
                return _health;
            }

            set
            {
                if (value < _health)
                    hurtFeedbacks.PlayFeedbacks();

                _health = value;
                healthDisplayer.SetHealth(_health);

                if (_health <= 0)
                    deathFeedbacks.PlayFeedbacks();
            }
        }
        private int _health;

        private void Start()
        {
            Vector3 lInitScale = transform.localScale;
            transform.localScale = Vector3.zero;
            transform.DOScale(lInitScale, 0.6f).SetEase(Ease.OutBack);
        }

        public void SetInfos(UnitInfos infos)
        {
            this.infos = infos;
            Health = infos.MaxHealth;
            renderer.material = infos.CharacterInfos.Material;
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag(groundTag))
                return;

            base.OnCollisionEnter(collision);

            if (Health <= 0)
                return;

            float lCollisionForce = collision.impulse.magnitude / Time.fixedDeltaTime;

            if (lCollisionForce >= minForceForDamage)
                Health--;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(gameBoundsTag))
                Destroy(gameObject);
        }

        private void OnValidate()
        {
            SetInfos(infos);
        }
    }
}