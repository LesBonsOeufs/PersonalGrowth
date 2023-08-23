using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.UI.Map.Spots {
    public delegate void MapSpotEventHandler (MapSpot sender);

    [RequireComponent(typeof(RectTransform))]
    public abstract class MapSpot : MonoBehaviour
    {
        public List<MapTrail> TrailsToNextSpots => _trailsToNextSpots;
        [SerializeField, ReadOnly] private List<MapTrail> _trailsToNextSpots;

        public List<MapSpot> NextSpots => _nextSpots;
        [Header("Editable")]
        [SerializeField] private List<MapSpot> _nextSpots = new List<MapSpot>();
        public RectTransform RectTransform { get; private set; }

        public bool IsMultipath => _nextSpots.Count > 0;

        public abstract event MapSpotEventHandler OnActionCompleted;

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }

        public abstract void StartAction();

        public void SetProperties(List<MapTrail> trailsToNextSpots)
        {
            _trailsToNextSpots = trailsToNextSpots == null ? null : new List<MapTrail>(trailsToNextSpots);
        }

        private void OnValidate()
        {
            RectTransform = GetComponent<RectTransform>();
        }
    }
}