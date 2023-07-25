using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle {
    public class Ball : MonoBehaviour
    {
        [SerializeField] private ParticleSystem hitParticles = default;

        protected virtual void OnCollisionEnter(Collision collision)
        {
            Vector3 lAveragePosition = (transform.position + collision.gameObject.transform.position) * 0.5f;
            hitParticles.transform.position = lAveragePosition;
            hitParticles.Play();
        }
    }
}