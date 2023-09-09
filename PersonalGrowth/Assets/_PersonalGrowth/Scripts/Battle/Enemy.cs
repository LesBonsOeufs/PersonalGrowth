using DG.Tweening;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle {
    [RequireComponent(typeof(Collider2D))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyDefense defense;
        [SerializeField] private Transform leftDefensePos;
        [SerializeField] private Transform rightDefensePos;
        [SerializeField, Range(0.3f, 1.3f)] private float minDefenseMoveDuration = 0.4f;
        [SerializeField, Range(0.6f, 1.6f)] private float maxDefenseMoveDuration = 1f;

        public float RandomDefenseMoveDuration => Random.Range(minDefenseMoveDuration, maxDefenseMoveDuration);

        private void Awake()
        {
            DefenseLoop();
        }

        private async void DefenseLoop()
        {
            await defense.transform.DOMove(leftDefensePos.position, RandomDefenseMoveDuration).AsyncWaitForCompletion();
            await defense.transform.DOMove(rightDefensePos.position, RandomDefenseMoveDuration).AsyncWaitForCompletion();

            DefenseLoop();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Hit!");
        }
    }
}