using Com.GabrielBernabeu.PersonalGrowth.PodometerSystem;
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
        private MapPositionData positionData;

        private MapSpot LastSpot
        {
            get
            {
                return _lastSpot;
            }

            set
            {
                _lastSpot = value;
                positionData.lastSpotIndex = Map.Instance.GetSpotIndex(_lastSpot);
                UpdateMapPosition(false);
            }
        }
        private MapSpot _lastSpot;

        private void Awake()
        {
            positionData = LocalDataSaver<LocalData>.CurrentData.mapPosition;
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
            LastSpot = Map.Instance.GetSpot(positionData.lastSpotIndex);
            Map.Instance.OnMapGenerated -= Map_OnMapGenerated;
        }

        private void PressForward_OnForward()
        {
            MoveForward(1);
        }

        private void MapSpot_OnActionCompleted(MapSpot sender, int chosenNextSpotIndex)
        {
            sender.OnActionCompleted -= MapSpot_OnActionCompleted;
            positionData.lastChosenNextSpotIndex = chosenNextSpotIndex;
            SaveMapPosition();
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
            positionData.pathStepsProgress = 0;
            SaveMapPosition();
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


            lStepCoinsInstance.Consume(nSteps);
            positionData.pathStepsProgress += nSteps;
            SaveMapPosition();
            UpdateMapPosition();
        }

        private void UpdateMapPosition(bool tweenPosition = true)
        {
            MapTrail lPathToNextSpot = LastSpot.TrailsToNextSpots[positionData.lastChosenNextSpotIndex];
            Vector2 lNewAnchorPos = lPathToNextSpot.GetAnchoredPositionFromSteps(positionData.pathStepsProgress);

            if (tweenPosition)
                rectTransform.DOAnchorPos(lNewAnchorPos, pressForward.ForwardCallCooldown)
                    .SetEase(Ease.InOutQuart);
            else
                rectTransform.anchoredPosition = lNewAnchorPos;

            //Completed path
            if (lPathToNextSpot.IsPathCompleted(positionData.pathStepsProgress))
            {
                StopForSpot(LastSpot.NextSpots[positionData.lastChosenNextSpotIndex]);
                return;
            }
            else
                stepsBubbleTmp.text = (lPathToNextSpot.StepsDistance - positionData.pathStepsProgress).ToString();

            UpdateForwardTrail();
        }

        private void UpdateForwardTrail()
        {
            Vector2 lNextSpotAnchoredPos = LastSpot.NextSpots[positionData.lastChosenNextSpotIndex].RectTransform.anchoredPosition;
            forwardTrail.SetThickness(LastSpot.TrailsToNextSpots[positionData.lastChosenNextSpotIndex].Thickness);
            forwardTrail.SetExtents(rectTransform.anchoredPosition, lNextSpotAnchoredPos);

            Vector2 lForwardCenter = rectTransform.anchoredPosition + (lNextSpotAnchoredPos - rectTransform.anchoredPosition) * 0.5f;
            stepsBubbleRectTransform.anchoredPosition = lForwardCenter;
        }
        
        private void UpdatePressForward()
        {
            pressForward.NextSpot = LastSpot.NextSpots[positionData.lastChosenNextSpotIndex].PressFeedback;
            stepsBubbleTmp.text = (LastSpot.TrailsToNextSpots[positionData.lastChosenNextSpotIndex].StepsDistance - positionData.pathStepsProgress).ToString();
        }

        private void SaveMapPosition()
        {
            LocalDataSaver<LocalData>.CurrentData.mapPosition = positionData;
            LocalDataSaver<LocalData>.SaveCurrentData();
        }

        private void OnDestroy()
        {
            pressForward.OnForward -= PressForward_OnForward;
        }
    }
}