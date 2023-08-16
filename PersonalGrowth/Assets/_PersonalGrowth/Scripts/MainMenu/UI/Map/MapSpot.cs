using NaughtyAttributes;
using System.ComponentModel;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.MainMenu.UI.Map {
    [RequireComponent(typeof(RectTransform))]
    public class MapSpot : MonoBehaviour
    {
        [ShowNativeProperty] public MapSpot PreviousSpot { get; private set; }
        [ShowNativeProperty] public MapPath PathToNextSpot { get; private set; }
        [ShowNativeProperty] public MapSpot NextSpot { get; private set; }
        public RectTransform RectTransform { get; private set; }

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }

        public void SetProperties(MapSpot previousSpot, MapPath pathToNextSpot, MapSpot nextSpot)
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