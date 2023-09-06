using NaughtyAttributes;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle {
    public class BattleHero : MonoBehaviour
    {
        [SerializeField, ReadOnly] private float currentCharge = 0f;

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (currentCharge >= 1f)
                    Strike();
            }
            
            if (Input.GetMouseButton(0))
                currentCharge += Time.deltaTime;
            else
                currentCharge -= Time.deltaTime;

            currentCharge = Mathf.Clamp(currentCharge, 0f, 1f);
        }

        private void Strike()
        {
            currentCharge = 0f;
            Debug.Log("Strike");
            //Striking stuff
        }
    }
}
