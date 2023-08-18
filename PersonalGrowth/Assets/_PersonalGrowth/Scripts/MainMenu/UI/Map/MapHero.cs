using NaughtyAttributes;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.MainMenu.UI.Map {
    [RequireComponent(typeof(RectTransform))]
    public class MapHero : MonoBehaviour
    {
        [SerializeField] private Trail forwardTrail;
        [SerializeField] private MapPressForward pressForward;

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

                pressForward.NextSpot = LastSpot.NextSpot.GetComponent<PressFeedback>();
            }
        }
        private MapSpot _lastSpot;

        private int pathStepsProgress = 0;

        private void Awake()
        {
            pressForward.ForwardTrail = forwardTrail.GetComponent<PressFeedback>();
            rectTransform = GetComponent<RectTransform>();
            Map.Instance.OnMapGenerated += Map_OnMapGenerated;
            pressForward.OnForward += PressForward_OnForward;
        }

        private void Map_OnMapGenerated()
        {
            LastSpot = Map.Instance.GetSpot(0);
            Map.Instance.OnMapGenerated -= Map_OnMapGenerated;
        }

        private void PressForward_OnForward()
        {
            MoveForward(15);
        }

        public void MoveForward(int nSteps)
        {
            StepCoinsManager lStepCoinsInstance = StepCoinsManager.Instance;

            if (lStepCoinsInstance.Count < nSteps)
                return;

            MapTrail lPathToNextSpot = LastSpot.PathToNextSpot;

            lStepCoinsInstance.Consume(nSteps);
            pathStepsProgress += nSteps;
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

        private void OnDestroy()
        {
            pressForward.OnForward -= PressForward_OnForward;
        }
    }
}