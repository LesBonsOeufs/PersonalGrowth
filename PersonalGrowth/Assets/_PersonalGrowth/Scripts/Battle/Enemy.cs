using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle {
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Collider2D leftCollider;
        [SerializeField] private Collider2D rightCollider;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.otherCollider == leftCollider)
                Debug.Log("Left!");
            else if (collision.otherCollider == rightCollider)
                Debug.Log("Right!");
        }
    }
}
