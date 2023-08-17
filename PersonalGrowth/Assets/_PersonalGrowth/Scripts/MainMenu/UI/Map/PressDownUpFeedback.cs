using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.GabrielBernabeu.PersonalGrowth.MainMenu.UI.Map 
{
    public abstract class PressDownUpFeedback : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
    {
        public List<PressDownUpFeedback> echo = new List<PressDownUpFeedback>();
        public bool isPressable = true;

        public bool IsPressed => isPressed;
        private bool isPressed = true;

        public void OnPointerDown(PointerEventData eventData)
        {
            PressDown();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            PressUp();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PressUp();
        }

        private void PressDown()
        {
            if (!isPressable)
                return;

            OnPressDown();

            foreach (PressDownUpFeedback item in echo)
                item.OnPressDown();

            isPressed = true;
        }

        private void PressUp()
        {
            if (!isPressed)
                return;

            OnPressUp();

            foreach (PressDownUpFeedback item in echo)
                item.OnPressUp();

            isPressed = false;
        }

        protected abstract void OnPressDown();
        protected abstract void OnPressUp();
    }
}