using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.MainMenu.UI.Map {
    [RequireComponent(typeof(RectTransform))]
    public class MapSpot : MonoBehaviour
    {
        [SerializeField] private MapSpot _nextSpot;

        private MapPath path;

        public MapSpot NextSpot => _nextSpot;
        public RectTransform RectTransform { get; private set; }

        public void SetPath(MapPath path)
        {
            this.path = path;
        }

        private void OnValidate()
        {
            RectTransform = GetComponent<RectTransform>();
        }
    }
}