using Com.GabrielBernabeu.PersonalGrowth.UI.Map.Spots;
using MoreMountains.Tools;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.GabrielBernabeu.PersonalGrowth.UI.Map {
    public class Map : Singleton<Map>
    {
        [SerializeField] private Button _fightBtn = default;

        [Header("Path generation")]
        [SerializeField, Tooltip("All spots must be in the same parent")] private List<MapSpot> spotsOrder;
        [SerializeField] private MapTrail pathPrefab;
        [SerializeField] private Transform pathsContainer;

        public Button FightBtn => _fightBtn;

        public event Action OnMapGenerated;

        public MapSpot GetSpot(int index)
        {
            return spotsOrder[index];
        }

        private void Start()
        {
            GenerateMap();
        }

        ///Call from editor for having a preview
        [Button("GenerateMap"), Tooltip("Place spots in spotsOrder first")]
        private void GenerateMap()
        {
            pathsContainer.MMDestroyAllChildren();
            int lSpotsCount = spotsOrder.Count;
            int lPreviousIndex;
            int lNextIndex;
            MapSpot lPreviousSpot;
            MapSpot lCurrentSpot;
            MapSpot lNextSpot;
            MapTrail lPathToNextSpot;

            for (int i = 0; i < lSpotsCount; i++)
            {
                lPreviousIndex = i - 1;
                lNextIndex = i + 1;
                lPreviousSpot = lPreviousIndex < 0 ? null : spotsOrder[lPreviousIndex];
                lCurrentSpot = spotsOrder[i];
                lNextSpot = lNextIndex >= lSpotsCount ? null : spotsOrder[lNextIndex];

                if (lNextSpot != null)
                {
                    lPathToNextSpot = Instantiate(pathPrefab, pathsContainer);
                    lPathToNextSpot.SetExtents(lCurrentSpot.RectTransform.anchoredPosition, lNextSpot.RectTransform.anchoredPosition);
                }
                else
                    lPathToNextSpot = null;

                lCurrentSpot.SetProperties(lPreviousSpot, lPathToNextSpot, lNextSpot);
            }

            OnMapGenerated?.Invoke();
        }
    }
}
