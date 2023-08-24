using Com.GabrielBernabeu.PersonalGrowth.UI.Map.Spots;
using Com.GabrielBernabeu.PersonalGrowth.UI.PressFeedbacks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.UI.Map {
    [RequireComponent(typeof(RectTransform))]
    public class MapHero : MonoBehaviour
    {
        [SerializeField] private Trail forwardTrail;
        [SerializeField] private MapPressForward pressForward;

        [Header("Steps bubble")]
        [SerializeField] private RectTransform stepsBubbleRectTransform;
        [SerializeField] private TextMeshProUGUI stepsBubbleTmp;

        private RectTransform rectTransform;

        private int lastChosenNextSpotIndex;
        private int pathStepsProgress = 0;

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

        private void Awake()
        {
            lastChosenNextSpotIndex = 0;
            pressForward.ForwardTrail = forwardTrail.GetComponent<PressFeedback>();
            rectTransform = GetComponent<RectTransform>();
            Map.Instance.OnMapGenerated += Map_OnMapGenerated;
        }

        private void Start()
        {
            StartTravel();
        }

        private void Map_OnMapGenerated()
        {
            LastSpot = Map.Instance.GetSpot(0);
            Map.Instance.OnMapGenerated -= Map_OnMapGenerated;
        }

        private void PressForward_OnForward()
        {
            MoveForward(1);
        }

        private void MapSpot_OnActionCompleted(MapSpot sender, int chosenNextSpotIndex)
        {
            sender.OnActionCompleted -= MapSpot_OnActionCompleted;
            lastChosenNextSpotIndex = chosenNextSpotIndex;
            StartTravel();
        }

        private void StartTravel()
        {
            forwardTrail.gameObject.SetActive(true);
            stepsBubbleRectTransform.gameObject.SetActive(true);
            UpdateForwardTrail();
            UpdatePressForward();
            pressForward.OnForward += PressForward_OnForward;
        }

        private void StopTravel()
        {
            forwardTrail.gameObject.SetActive(false);
            stepsBubbleRectTransform.gameObject.SetActive(false);
            pressForward.OnForward -= PressForward_OnForward;
        }

        private void StopForSpot(MapSpot spot)
        {
            Debug.Log("New spot!");
            LastSpot = spot;
            pathStepsProgress = 0;
            StopTravel();

            LastSpot.OnActionCompleted += MapSpot_OnActionCompleted;
            LastSpot.StartAction();
        }

        public void MoveForward(int nSteps)
        {
            StepCoinsManager lStepCoinsInstance = StepCoinsManager.Instance;

            if (lStepCoinsInstance.Count < nSteps)
            {
                GeneralTextFeedback.Instance.MakeText("Not enough step coins for moving forward!");
                return;
            }

            MapTrail lPathToNextSpot = LastSpot.TrailsToNextSpots[lastChosenNextSpotIndex];

            lStepCoinsInstance.Consume(nSteps);
            pathStepsProgress += nSteps;

            rectTransform.DOAnchorPos(lPathToNextSpot.GetAnchoredPositionFromStepCoins(pathStepsProgress), pressForward.ForwardCallCoolDown)
                .SetEase(Ease.InOutQuart);

            //Completed path
            if (lPathToNextSpot.IsPathCompleted(pathStepsProgress))
            {
                StopForSpot(LastSpot.NextSpots[lastChosenNextSpotIndex]);
                return;
            }
            else
                stepsBubbleTmp.text = (lPathToNextSpot.StepsDistance - pathStepsProgress).ToString();

            UpdateForwardTrail();
        }

        private void UpdateForwardTrail()
        {
            Vector2 lNextSpotAnchoredPos = LastSpot.NextSpots[lastChosenNextSpotIndex].RectTransform.anchoredPosition;
            forwardTrail.SetThickness(LastSpot.TrailsToNextSpots[lastChosenNextSpotIndex].Thickness);
            forwardTrail.SetExtents(rectTransform.anchoredPosition, lNextSpotAnchoredPos);

            Vector2 lForwardCenter = rectTransform.anchoredPosition + (lNextSpotAnchoredPos - rectTransform.anchoredPosition) * 0.5f;
            stepsBubbleRectTransform.anchoredPosition = lForwardCenter;
        }
        
        private void UpdatePressForward()
        {
            pressForward.NextSpot = LastSpot.NextSpots[lastChosenNextSpotIndex].PressFeedback;
            stepsBubbleTmp.text = LastSpot.TrailsToNextSpots[lastChosenNextSpotIndex].StepsDistance.ToString();
        }

        private void OnDestroy()
        {
            pressForward.OnForward -= PressForward_OnForward;
        }
    }
}