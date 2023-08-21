using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.GabrielBernabeu.PersonalGrowth.UI {
    public delegate void PressFeedbackEventHandler(PressFeedback sender);
    public abstract class PressFeedback : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
    {
        public bool isPressable = true;

        public bool IsPressed => _isPressed;
        private bool _isPressed = false;

        public event PressFeedbackEventHandler OnPressDown;
        public event PressFeedbackEventHandler OnPressUp;

        public void OnPointerDown(PointerEventData eventData)
        {
            PressDown(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            PressUp(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PressUp(true);
        }

        public void ForcePressDown() => PressDown();

        private void PressDown(bool fireEvent = false)
        {
            if (!isPressable)
                return;

            PressedDown();

            if (fireEvent)
                OnPressDown?.Invoke(this);

            _isPressed = true;
        }

        public void ForcePressUp() => PressUp();

        private void PressUp(bool fireEvent = false)
        {
            if (!_isPressed)
                return;

            PressedUp();

            if (fireEvent)
                OnPressUp?.Invoke(this);

            _isPressed = false;
        }

        protected abstract void PressedDown();
        protected abstract void PressedUp();
    }
}