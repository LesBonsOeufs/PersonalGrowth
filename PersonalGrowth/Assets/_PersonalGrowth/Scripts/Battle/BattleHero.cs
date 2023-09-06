using NaughtyAttributes;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle {
    public class BattleHero : MonoBehaviour
    {
        [SerializeField] private float strikeCooldown = 0.66f;

        [SerializeField, ReadOnly] private float castCounter = 0f;
        [SerializeField, ReadOnly] private float strikeCooldownCounter = 0f;

        private bool isCasting = false;

        public EScreenSide GetTouchSide
        {
            get
            {
                bool lIsLeftSide = Input.mousePosition.x < Screen.width * 0.5f;
                return lIsLeftSide ? EScreenSide.LEFT : EScreenSide.RIGHT;
            }
        }

        private void Update()
        {
            if (strikeCooldownCounter > 0f)
            {
                strikeCooldownCounter -= Time.deltaTime;
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                isCasting = true;
                Debug.Log(GetTouchSide);
            }
            if (Input.GetMouseButtonUp(0))
            {
                isCasting = false;

                if (castCounter >= 1f)
                    Strike();
            }
            
            if (isCasting)
                castCounter += Time.deltaTime;
            else
                castCounter -= Time.deltaTime;

            castCounter = Mathf.Clamp(castCounter, 0f, 1f);
        }

        private void Strike()
        {
            castCounter = 0f;
            Debug.Log("Strike");
            //Striking stuff
            strikeCooldownCounter = strikeCooldown;
        }
    }
}
