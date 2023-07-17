using DG.Tweening;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle {
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private Transform spawnPos = default;
        [SerializeField] private PlayerBall playerPrefab = default;

        private void Start()
        {
            Spawn();
        }

        private void Spawn()
        {
            PlayerBall lPlayerBall = Instantiate(playerPrefab, spawnPos.position, Quaternion.identity, null);
            lPlayerBall.OnDeath += PlayerBall_OnDeath;

            Vector3 lInitScale = lPlayerBall.transform.localScale;
            lPlayerBall.transform.localScale = Vector3.zero;
            lPlayerBall.transform.DOScale(lInitScale, 0.6f).SetEase(Ease.OutBack);
        }

        private void PlayerBall_OnDeath(PlayerBall sender)
        {
            sender.OnDeath -= PlayerBall_OnDeath;
            Spawn();
        }
    }
}
