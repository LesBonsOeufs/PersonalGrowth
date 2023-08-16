using NaughtyAttributes;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.MainMenu.UI.Map {
    [RequireComponent(typeof(RectTransform))]
    public class MapHero : MonoBehaviour
    {
        private RectTransform rectTransform;

        private MapSpot LastSpot
        {
            get
            {
                return _lastSpot;
            }

            set
            {
                _lastSpot = value;
                rectTransform.anchoredPosition = LastSpot.RectTransform.anchoredPosition;
            }
        }
        private MapSpot _lastSpot;

        private int pathStepsProgress = 0;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            Map.Instance.OnMapGenerated += Map_OnMapGenerated;
        }

        private void Map_OnMapGenerated()
        {
            LastSpot = Map.Instance.GetSpot(0);
            Map.Instance.OnMapGenerated -= Map_OnMapGenerated;
        }

        [Button("move")]
        public void MoveForward()
        {
            MapPath lPathToNextSpot = LastSpot.PathToNextSpot;
            pathStepsProgress += 100;
            rectTransform.anchoredPosition = lPathToNextSpot.GetAnchoredPositionFromStepCoins(pathStepsProgress);

            //Completed path
            if (lPathToNextSpot.IsPathCompleted(pathStepsProgress))
            {
                pathStepsProgress = 0;
                LastSpot = LastSpot.NextSpot;

                if (LastSpot.PathToNextSpot != null)
                    Debug.Log("New spot!");
                else
                    Debug.Log("Completed map!");
            }
        }
    }
}