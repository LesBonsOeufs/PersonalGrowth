using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle 
{
    public class EnemyBall : MonoBehaviour
    {
        [SerializeField] private UnitInfos infos = default;
        [SerializeField] private HealthDisplayer healthDisplayer = default;
        [SerializeField] private float minForceForDamage = 400f;
        [SerializeField] private new Renderer renderer = default;

        public int Health
        {
            get
            {
                return _health;
            }

            set
            {
                _health = value;
                healthDisplayer.SetHealth(_health);

                if (_health <= 0)
                    Destroy(gameObject);
            }
        }
        private int _health;

        public void SetInfos(UnitInfos infos)
        {
            this.infos = infos;
            Health = infos.MaxHealth;
            renderer.material = infos.CharacterInfos.Material;
        }

        private void OnCollisionEnter(Collision collision)
        {
            float lCollisionForce = collision.impulse.magnitude / Time.fixedDeltaTime;

            if (lCollisionForce >= minForceForDamage)
                Health--;
        }

        private void OnValidate()
        {
            SetInfos(infos);
        }
    }
}