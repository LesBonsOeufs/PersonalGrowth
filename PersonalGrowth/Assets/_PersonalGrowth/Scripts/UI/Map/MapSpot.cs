using NaughtyAttributes;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.UI.Map {
    [RequireComponent(typeof(RectTransform))]
    public class MapSpot : MonoBehaviour
    {
        [ShowNativeProperty] public MapSpot PreviousSpot { get; private set; }
        [ShowNativeProperty] public MapTrail PathToNextSpot { get; private set; }
        [ShowNativeProperty] public MapSpot NextSpot { get; private set; }
        public RectTransform RectTransform { get; private set; }

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }

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