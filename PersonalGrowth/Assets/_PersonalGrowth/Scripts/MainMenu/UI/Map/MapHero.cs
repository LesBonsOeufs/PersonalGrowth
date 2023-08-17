using NaughtyAttributes;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.MainMenu.UI.Map {
    [RequireComponent(typeof(RectTransform))]
    public class MapHero : MonoBehaviour
    {
        [SerializeField] private Trail forwardTrail;

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
                UpdateForwardTrail();
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
            MapTrail lPathToNextSpot = LastSpot.PathToNextSpot;

            StepCoinsManager.Instance.Consume(1);
            pathStepsProgress ++;
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

            UpdateForwardTrail();
        }

        private void UpdateForwardTrail()
        {
            forwardTrail.SetThickness(LastSpot.PathToNextSpot.Thickness);
            forwardTrail.SetExtents(rectTransform.anchoredPosition, LastSpot.NextSpot.RectTransform.anchoredPosition);
        }
    }
}