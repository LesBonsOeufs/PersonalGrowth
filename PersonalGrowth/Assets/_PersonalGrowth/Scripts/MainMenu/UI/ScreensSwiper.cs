using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine.EventSystems;
using System;

namespace Com.GabrielBernabeu.PersonalGrowth.MainMenu.UI {
    public class ScreensSwiper : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Foldout("Objects"), SerializeField] private RectTransform screensContainer = default;
        [Foldout("Objects"), SerializeField] private LayoutGroup screensIconsContainer = default;
        [Foldout("Objects"), SerializeField] private Button screensBandIconPrefab = default;
        [Foldout("Objects"), SerializeField] private RectTransform screensBandSelector = default;

        [Foldout("Screens"), SerializeField] private List<SwipableScreenInfo> leftScreens = default;
        [Foldout("Screens"), SerializeField] private SwipableScreenInfo centerScreen = default;
        [Foldout("Screens"), SerializeField] private List<SwipableScreenInfo> rightScreens = default;

        [Foldout("Screens icons settings"), SerializeField, Range(1f, 300f)] private float buttonsBaseSize = 100f;
        [Foldout("Screens icons settings"), SerializeField, Range(1f, 300f)] private float currentButtonAddedSize = 30f;
        [Foldout("Screens icons settings"), SerializeField, Range(0.1f, 5f)] private float timeToGoToScreen = 1f;
        [Foldout("Screens icons settings"), SerializeField, Range(0.1f, 5f)] private float buttonTimeToExpand = 1f;

        [SerializeField, Range(0f, 1f)] private float swipePercentThreshold = 0.2f;

        private CanvasScaler canvasScaler;
        private List<Button> screensButtons = new List<Button>();
        private int screensButtonsCenterIndex;

        private Vector2 beginDragAnchoredPos;

        private int IndexPosition
        {
            get
            {
                return _indexPosition;
            }

            set
            {
                ChangeCurrentButton(_indexPosition, value);

                _indexPosition = value;

                screensContainer.DOAnchorPos(GetAnchoredPosFromIndex(_indexPosition), timeToGoToScreen)
                    .SetUpdate(true).OnUpdate(UpdateSelectorPos);
            }
        }
        private int _indexPosition = 0;

        protected void Start()
        {
            canvasScaler = GetComponentInParent<CanvasScaler>();
            InitScreens();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            screensContainer.DOKill(false);
            beginDragAnchoredPos = screensContainer.anchoredPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            float lDifference = eventData.pressPosition.x - eventData.position.x;
            screensContainer.anchoredPosition = beginDragAnchoredPos - new Vector2(lDifference, 0f);
            UpdateSelectorPos();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            float lPercentage = (eventData.position.x - eventData.pressPosition.x) / Screen.width;
            int lNewIndex = IndexPosition;

            if (Mathf.Abs(lPercentage) >= swipePercentThreshold)
            {
                if (lPercentage > 0f && screensButtonsCenterIndex + lNewIndex > 0)
                    lNewIndex--;
                else if (lPercentage < 0f && screensButtonsCenterIndex + lNewIndex < screensButtons.Count - 1)
                    lNewIndex++;
            }

            IndexPosition = lNewIndex;
        }

        private void Button_OnClick(int index)
        {
            if (index == IndexPosition)
                return;

            IndexPosition = index;
        }

        private void InitScreens()
        {
            int lLeftCount = leftScreens.Count;
            int lRightCount = rightScreens.Count;

            if (lLeftCount == 0 && lRightCount == 0)
            {
                InstantiateScreen(centerScreen, 0f, 0);
                return;
            }

            SwipableScreenInfo lCurrentScreen;
            float lScreenWidth = canvasScaler.referenceResolution.x;


            for (int i = 0; i < lLeftCount; i++)
            {
                lCurrentScreen = leftScreens[i];
                InstantiateScreen(lCurrentScreen, (-lLeftCount + i) * lScreenWidth, -lLeftCount + i);
            }

            InstantiateScreen(centerScreen, 0f, 0);

            screensButtonsCenterIndex = screensButtons.Count - 1;

            RectTransform lMainButtonTransform = screensButtons[screensButtonsCenterIndex].GetComponent<RectTransform>();
            lMainButtonTransform.sizeDelta = Vector2.one * (currentButtonAddedSize + buttonsBaseSize);

            for (int i = 0; i < lRightCount; i++)
            {
                lCurrentScreen = rightScreens[i];
                InstantiateScreen(lCurrentScreen, (i + 1) * lScreenWidth, i + 1);
            }

            screensBandSelector.sizeDelta = new Vector2(lScreenWidth / screensButtons.Count, screensBandSelector.sizeDelta.y);
        }

        private void InstantiateScreen(SwipableScreenInfo screen, float positionX, int index)
        {
            RectTransform lCurrentScreen = Instantiate(screen.Prefab, screensContainer);
            lCurrentScreen.anchoredPosition += Vector2.right * positionX;

            Button lCurrentScreenButton = InstantiateIcon(screen.ScreensBandLogo, index);
            screensButtons.Add(lCurrentScreenButton);
        }

        private Button InstantiateIcon(Sprite icon, int index)
        {
            Button lButton = Instantiate(screensBandIconPrefab, screensIconsContainer.transform);
            lButton.GetComponent<RectTransform>().sizeDelta = Vector2.one * buttonsBaseSize;

            lButton.GetComponent<Image>().sprite = icon;
            lButton.onClick.AddListener(delegate { Button_OnClick(index); });

            return lButton;
        }

        private void ChangeCurrentButton(int lastIndex, int newIndex)
        {
            RectTransform lLastMainButtonTransform = screensButtons[screensButtonsCenterIndex + lastIndex].GetComponent<RectTransform>();
            lLastMainButtonTransform.DOSizeDelta(Vector2.one * buttonsBaseSize, buttonTimeToExpand).SetUpdate(true);

            RectTransform lMainButtonTransform = screensButtons[screensButtonsCenterIndex + newIndex].GetComponent<RectTransform>();
            lMainButtonTransform.DOSizeDelta(Vector2.one * (currentButtonAddedSize + buttonsBaseSize), buttonTimeToExpand).SetUpdate(true);
        }

        private void UpdateSelectorPos()
        {
            float lScreenHalfWidth = canvasScaler.referenceResolution.x * 0.5f;
            float lSelectorHalfWidth = screensBandSelector.sizeDelta.x * 0.5f;
            float lScreensButtonsRatio = (GetIndexFromAnchoredXPos(screensContainer.anchoredPosition.x) + screensButtonsCenterIndex) / (screensButtons.Count - 1);

            screensBandSelector.anchoredPosition = new Vector2(
                Mathf.LerpUnclamped(-lScreenHalfWidth + lSelectorHalfWidth, lScreenHalfWidth - lSelectorHalfWidth, lScreensButtonsRatio), 
                screensBandSelector.anchoredPosition.y
                );
        }

        private Vector2 GetAnchoredPosFromIndex(int index)
        {
            return new Vector2(-canvasScaler.referenceResolution.x * index, 0f);
        }

        private float GetIndexFromAnchoredXPos(float anchoredPosX)
        {
            return anchoredPosX / -canvasScaler.referenceResolution.x;
        }

        private void OnDestroy()
        {
            int lButtonsCount = screensButtons.Count;

            for (int i = 0; i < lButtonsCount; i++)
            {
                screensButtons[i].onClick.RemoveAllListeners();
            }
        }
    }
}