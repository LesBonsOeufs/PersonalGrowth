using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.ComponentModel;
using NaughtyAttributes;

namespace Com.GabrielBernabeu.PersonalGrowth.PodometerSystem.UI {
    public class ScreensSwiper : MonoBehaviour
    {
        [Foldout("Objects"), SerializeField] private RectTransform screensContainer = default;
        [Foldout("Objects"), SerializeField] private RectTransform screensBand = default;
        [Foldout("Objects"), SerializeField] private Button screensBandIconPrefab = default;

        [Foldout("Screens"), SerializeField] private List<SwipableScreenInfo> leftScreens = default;
        [Foldout("Screens"), SerializeField] private SwipableScreenInfo centerScreen = default;
        [Foldout("Screens"), SerializeField] private List<SwipableScreenInfo> rightScreens = default;

        [Foldout("Settings"), SerializeField, Range(0.1f, 3f)] private float timeToGoPosition = 1f;
        [Foldout("Settings"), SerializeField] private float buttonsSize = default;
        [Foldout("Settings"), SerializeField, Range(1f, 100f)] private float currentButtonAddSize = 20f;
        [Foldout("Settings"), SerializeField, Range(0.1f, 3)] private float timeToExpand = 0.5f;

        private List<Button> buttonScreens = new List<Button>();
        private CanvasScaler canvasScaler;

        private int indexPosition = 0;
        private int startIndex;

        private void Button_OnClick(int index)
        {
            if (index == indexPosition)
                return;

            ChangeCurrentButton(indexPosition, index);

            indexPosition = index;

            screensContainer.GetComponent<RectTransform>()
                .DOAnchorPos(new Vector2(canvasScaler.referenceResolution.x * indexPosition, 0f), timeToGoPosition)
                .SetUpdate(true);
        }

        protected void Start()
        {
            canvasScaler = GetComponentInParent<CanvasScaler>();
            InitScreens();
        }

        private void InitScreens()
        {
            float leftLength = leftScreens.Count;
            float rightLength = rightScreens.Count;

            RectTransform mainButtonTransform;

            if (leftLength == 0 && rightLength == 0)
            {
                InstantiateScreen(centerScreen, 0f, 0);
                return;
            }

            SwipableScreenInfo currentScreen;
            float screenSize = canvasScaler.referenceResolution.x;

            for (int i = 0; i < leftLength; i++)
            {
                currentScreen = leftScreens[i];
                InstantiateScreen(currentScreen, (leftLength - i) * -screenSize, (int)leftLength - i);
            }

            InstantiateScreen(centerScreen, 0f, 0);

            startIndex = buttonScreens.Count - 1;
            mainButtonTransform = buttonScreens[startIndex].GetComponent<RectTransform>();
            mainButtonTransform.sizeDelta = Vector2.one * (currentButtonAddSize + buttonsSize);

            for (int i = 0; i < rightLength; i++)
            {
                currentScreen = rightScreens[i];
                InstantiateScreen(currentScreen, (i + 1) * screenSize, -(i + 1));
            }
        }

        private void InstantiateScreen(SwipableScreenInfo screen, float positionX, int index)
        {
            RectTransform currentScreen = Instantiate(screen.Prefab, screensContainer).GetComponent<RectTransform>();
            currentScreen.anchoredPosition += Vector2.right * positionX;

            Button currentScreenButton = InstantiateLogo(screen.ScreensBandLogo, index);
            buttonScreens.Add(currentScreenButton);
        }

        private Button InstantiateLogo(Sprite logo, int index)
        {
            Button button = Instantiate(screensBandIconPrefab, screensBand);

            button.GetComponent<Image>().sprite = logo;
            button.onClick.AddListener(delegate { Button_OnClick(index); });

            return button;
        }

        private void ChangeCurrentButton(int currentIndex, int nextIndex)
        {
            RectTransform mainButtonTransform = buttonScreens[startIndex - currentIndex].GetComponent<RectTransform>();
            mainButtonTransform.DOSizeDelta(Vector2.one * buttonsSize, timeToExpand).SetUpdate(true);

            mainButtonTransform = buttonScreens[startIndex - nextIndex].GetComponent<RectTransform>();
            mainButtonTransform.DOSizeDelta(Vector2.one * (currentButtonAddSize + buttonsSize), timeToExpand).SetUpdate(true);
        }

        private void OnDestroy()
        {
            int length = buttonScreens.Count;

            for (int i = 0; i < length; i++)
            {
                buttonScreens[i].onClick.RemoveAllListeners();
            }
        }
    }
}