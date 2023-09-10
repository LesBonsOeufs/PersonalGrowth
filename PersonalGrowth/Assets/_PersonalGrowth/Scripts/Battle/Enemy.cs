using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle {
    [RequireComponent(typeof(Collider2D))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyDefense defense;
        [SerializeField] private Transform leftDefensePos;
        [SerializeField] private Transform rightDefensePos;
        [SerializeField] private int maxHealth = 6;
        [SerializeField] private Image healthBar;
        [SerializeField, Range(0.3f, 1.3f)] private float minDefenseMoveDuration = 0.4f;
        [SerializeField, Range(0.6f, 1.6f)] private float maxDefenseMoveDuration = 1f;

        public int Health
        {
            get
            {
                return _health;
            }

            set
            {
                _health = value;
                healthBar.DOKill();
                healthBar.DOFillAmount((float)_health / maxHealth, 0.6f).SetEase(Ease.OutCubic);

                //DEBUG!!! WIN
                if (_health <= 0)
                {
                    transform.DOScale(0f, 1.3f).SetEase(Ease.OutCubic)
                        .OnComplete(() =>
                        {
                            SceneManager.LoadScene(0);
                        });
                }
                //DEBUG!!!
            }
        }
        private int _health;

        private float RandomDefenseMoveDuration => Random.Range(minDefenseMoveDuration, maxDefenseMoveDuration);

        private void Awake()
        {
            Health = maxHealth;
            DefenseLoop();
        }

        private async void DefenseLoop()
        {
            await defense.transform.DOMove(leftDefensePos.position, RandomDefenseMoveDuration).AsyncWaitForCompletion();
            await defense.transform.DOMove(rightDefensePos.position, RandomDefenseMoveDuration).AsyncWaitForCompletion();

            DefenseLoop();
        }
    }
}