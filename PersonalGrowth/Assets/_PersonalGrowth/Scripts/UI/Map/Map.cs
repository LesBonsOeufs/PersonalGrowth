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
        [Header("Miscellaneous")]
        [SerializeField] private Button _fightBtn = default;

        [Header("Spots")]
        [InfoBox("Make sure to only have one Spot without any previousSpots. It will act as the map's start.", EInfoBoxType.Warning)]
        [SerializeField] private Transform spotsContainer;
        [InfoBox("This array will be used for generation & indexation. Changing its order might skew save files.")]
        [ReadOnly, SerializeField] private List<MapSpot> spotsCatalog = new List<MapSpot>();

        [Header("Path generation")]
        [SerializeField] private MapTrail pathPrefab;
        [SerializeField] private Transform pathsContainer;

        public Button FightBtn => _fightBtn;

        public event Action OnMapGenerated;

        public MapSpot GetSpot(int index)
        {
            return spotsCatalog[index];
        }

        private void Start()
        {
            GenerateMap();
        }

        //Call from editor for having a preview
        [Button("Generate Map"), Tooltip("Place spots in spotsOrder first")]
        /// <summary>
        /// Adds PreviousSpots && all Paths to the Map's spots, based on the entered NextSpots values.
        /// </summary>
        private void GenerateMap()
        {
            pathsContainer.MMDestroyAllChildren();

            foreach (MapSpot spot in spotsCatalog)
            {
                List<MapTrail> lTrailsToNextSpots = new List<MapTrail>();

                if (spot.NextSpots.Count > 0)
                {
                    MapTrail lTrailToSpot;

                    foreach (MapSpot nextSpot in spot.NextSpots)
                    {
                        lTrailToSpot = Instantiate(pathPrefab, pathsContainer);
                        lTrailsToNextSpots.Add(lTrailToSpot);
                        lTrailToSpot.SetExtents(spot.RectTransform.anchoredPosition, nextSpot.RectTransform.anchoredPosition);
                    }
                }
                else
                    lTrailsToNextSpots = null;

                spot.SetProperties(lTrailsToNextSpots);
            }

            OnMapGenerated?.Invoke();
        }

        private void OnValidate()
        {
            spotsCatalog.Clear();
            int lSpotsCount = spotsContainer.childCount;

            for (int i = 0; i < lSpotsCount; i++)
            {
                if (spotsContainer.GetChild(i).TryGetComponent(out MapSpot spot))
                    spotsCatalog.Add(spot);
            }
        }
    }
}