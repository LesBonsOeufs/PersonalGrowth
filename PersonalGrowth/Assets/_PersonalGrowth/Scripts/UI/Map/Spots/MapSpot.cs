using Com.GabrielBernabeu.Common;
using Com.GabrielBernabeu.PersonalGrowth.UI.PressFeedbacks;
using DG.Tweening;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.UI.Map.Spots {
    public delegate void MapSpotEventHandler (MapSpot sender, int chosenNextSpotIndex);

    [RequireComponent(typeof(RectTransform))]
    public abstract class MapSpot : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private PressFeedback _pressFeedback;
        [SerializeField] private PassiveScale _passiveScale;

        [Header("Spots")]
        [SerializeField] private List<MapSpot> _nextSpots = new List<MapSpot>();
        [SerializeField, ReadOnly] private List<MapTrail> _trailsToNextSpots;

        public PressFeedback PressFeedback => _pressFeedback;
        public PassiveScale PassiveScale => _passiveScale;
        public List<MapSpot> NextSpots => _nextSpots;
        public List<MapTrail> TrailsToNextSpots => _trailsToNextSpots;
        public RectTransform RectTransform { get; private set; }

        public bool IsMultipath => _nextSpots.Count > 1;

        public event MapSpotEventHandler OnActionCompleted;

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }

        public abstract void StartAction();

        public void SetProperties(List<MapTrail> trailsToNextSpots)
        {
            _trailsToNextSpots = trailsToNextSpots == null ? null : new List<MapTrail>(trailsToNextSpots);
        }

        /// <summary>
        /// Always call this when custom spot Action is completed
        /// </summary>
        protected void StartChoosingNextSpot()
        {
            if (IsMultipath)
            {
                foreach (MapSpot nextSpot in _nextSpots)
                {
                    nextSpot.PressFeedback.OnClick += ChooseNextSpot;

                }
            }
            else
                OnActionCompleted?.Invoke(this, 0);
        }

        private void ChooseNextSpot(PressFeedback sender)
        {
            foreach (MapSpot nextSpot in _nextSpots)
                nextSpot.PressFeedback.OnClick -= ChooseNextSpot;

            int lSpotIndex = _nextSpots.FindIndex(nextSpot => nextSpot.PressFeedback == sender);
            OnActionCompleted?.Invoke(this, lSpotIndex);
        }

        private void OnValidate()
        {
            RectTransform = GetComponent<RectTransform>();
        }
    }
}