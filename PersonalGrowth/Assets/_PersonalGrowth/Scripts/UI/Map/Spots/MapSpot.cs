using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.UI.Map.Spots {
    public delegate void MapSpotEventHandler (MapSpot sender);

    [RequireComponent(typeof(RectTransform))]
    public abstract class MapSpot : MonoBehaviour
    {
        public MapSpot PreviousSpot => _previousSpot;
        [SerializeField, ReadOnly] private MapSpot _previousSpot;
        public MapTrail PathToNextSpot => _pathToNextSpot;
        [SerializeField, ReadOnly] private MapTrail _pathToNextSpot;
        public MapSpot NextSpot => _nextSpot;
        [SerializeField, ReadOnly] private MapSpot _nextSpot;
        public List<MapSpot> AdditionalNextSpots => _additionalNextSpots;
        [SerializeField, ReadOnly] private List<MapSpot> _additionalNextSpots = new List<MapSpot>();
        public RectTransform RectTransform { get; private set; }

        public bool IsMultipath => _additionalNextSpots.Count > 0;

        public abstract event MapSpotEventHandler OnActionCompleted;

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }

        public abstract void StartAction();

        public void SetProperties(MapSpot previousSpot, MapTrail pathToNextSpot, MapSpot nextSpot)
        {
            _previousSpot = previousSpot;
            _pathToNextSpot = pathToNextSpot;
            _nextSpot = nextSpot;
        }

        private void OnValidate()
        {
            RectTransform = GetComponent<RectTransform>();
        }
    }
}