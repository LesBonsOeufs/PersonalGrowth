using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle {
    public delegate void EnemyDefenseEventHandler();
    [RequireComponent(typeof(Collider2D))]
    public class EnemyDefense : MonoBehaviour
    {
        //Change this logic in the EquippedWeapon script.
        public static event EnemyDefenseEventHandler OnBlock;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnBlock?.Invoke();
        }
    }
}