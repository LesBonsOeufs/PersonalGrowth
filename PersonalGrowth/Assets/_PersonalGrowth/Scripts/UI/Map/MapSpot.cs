using NaughtyAttributes;
using System;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.UI.Map {
    public delegate void MapSpotEventHandler (MapSpot sender);

    [RequireComponent(typeof(RectTransform))]
    public abstract class MapSpot : MonoBehaviour
    {
        [ShowNativeProperty] public MapSpot PreviousSpot { get; private set; }
        [ShowNativeProperty] public MapTrail PathToNextSpot { get; private set; }
        [ShowNativeProperty] public MapSpot NextSpot { get; private set; }
        public RectTransform RectTransform { get; private set; }

        public abstract event MapSpotEventHandler OnActionCompleted;

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }

        public abstract void StartAction();

        public void SetProperties(MapSpot previousSpot, MapTrail pathToNextSpot, MapSpot nextSpot)
        {
            PreviousSpot = previousSpot;
            PathToNextSpot = pathToNextSpot;
            NextSpot = nextSpot;
        }

        private void OnValidate()
        {
            RectTransform = GetComponent<RectTransform>();
        }
    }
}