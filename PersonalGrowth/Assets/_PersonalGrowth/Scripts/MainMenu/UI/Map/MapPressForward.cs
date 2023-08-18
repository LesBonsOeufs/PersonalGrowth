using Com.GabrielBernabeu.PersonalGrowth.MainMenu.UI;
using System;
using UnityEngine;

namespace Com.GabrielBernabeu
{
    public class MapPressForward : MonoBehaviour
    {
        [SerializeField, Tooltip("Time between each OnForward call")] private float forwardCallCooldown = 0.1f;

        private float counter = 0f;

        public PressFeedback ForwardTrail
        {
            get
            {
                return _forwardTrail;
            }

            set
            {
                if (_forwardTrail != null)
                {
                    _forwardTrail.OnPressDown -= PressFeedback_OnPressDown;
                    _forwardTrail.OnPressUp -= PressFeedback_OnPressUp;
                }

                _forwardTrail = value;
                _forwardTrail.OnPressDown += PressFeedback_OnPressDown;
                _forwardTrail.OnPressUp += PressFeedback_OnPressUp;
            }
        }
        private PressFeedback _forwardTrail;

        public PressFeedback NextSpot
        {
            get
            {
                return _nextSpot;
            }

            set
            {
                if (_nextSpot != null)
                {
                    _nextSpot.OnPressDown -= PressFeedback_OnPressDown;
                    _nextSpot.OnPressUp -= PressFeedback_OnPressUp;
                }

                _nextSpot = value;
                _nextSpot.OnPressDown += PressFeedback_OnPressDown;
                _nextSpot.OnPressUp += PressFeedback_OnPressUp;
            }
        }
        private PressFeedback _nextSpot;

        public bool IsPressed => _forwardTrail.IsPressed || NextSpot.IsPressed;

        public event Action OnForward;

        private void PressFeedback_OnPressDown(PressFeedback sender)
        {
            if (sender == NextSpot)
                ForwardTrail.ForcePressDown();
            else if (sender == ForwardTrail)
                NextSpot.ForcePressDown();
        }

        private void PressFeedback_OnPressUp(PressFeedback sender)
        {
            if (sender == NextSpot)
                ForwardTrail.ForcePressUp();
            else if (sender == ForwardTrail)
                NextSpot.ForcePressUp();
        }

        private void Update()
        {
            if (!IsPressed)
            {
                counter = 0f;
                return;
            }

            counter += Time.deltaTime;

            if (counter > forwardCallCooldown)
            {
                counter = 0f;
                OnForward?.Invoke();
            }
        }
    }
}