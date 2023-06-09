///-----------------------------------------------------------------
/// Author : Gabriel Bernabeu
/// Date : 20/02/2022 17:59
///-----------------------------------------------------------------
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.GabrielBernabeu.Common.CustomButtons 
{
    public class CustomBasicButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool IsBeingPressed { get; private set; }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            IsBeingPressed = true;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            IsBeingPressed = false;
        }
    }
}
