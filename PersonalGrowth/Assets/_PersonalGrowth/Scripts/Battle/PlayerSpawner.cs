using DG.Tweening;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle {
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private Transform spawnPos = default;
        [SerializeField] private GameObject aura = default;
        [SerializeField] private PlayerBall playerPrefab = default;
        [SerializeField] private ParticleSystem spawnParticles = default;

        private void Start()
        {
            Spawn();
        }

        private void Spawn()
        {
            spawnParticles.Play();
            aura.SetActive(true);
            PlayerBall lPlayerBall = Instantiate(playerPrefab, spawnPos.position, Quaternion.identity, null);
            lPlayerBall.DragShoot.OnShoot += DragShoot_OnShoot;
            lPlayerBall.OnDeath += PlayerBall_OnDeath;
        }

        private void DragShoot_OnShoot(DragShoot sender)
        {
            sender.OnShoot -= DragShoot_OnShoot;
            aura.SetActive(false);
        }

        private void PlayerBall_OnDeath(PlayerBall sender)
        {
            sender.OnDeath -= PlayerBall_OnDeath;
            Spawn();
        }
    }
}
