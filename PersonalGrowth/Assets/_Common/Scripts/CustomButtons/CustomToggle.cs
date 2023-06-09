using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.GabrielBernabeu.Common.CustomButtons {
    public delegate void CustomToggleEventHandler(bool isActive);
    public class CustomToggle : MonoBehaviour, IPointerUpHandler
    {
        public bool IsSwitchedOn
        {
            get
            {
                return _isSwitchedOn;
            }

            protected set
            {
                if (_isSwitchedOn == value)
                    return;

                _isSwitchedOn = value;
                OnValueChangedInvocation();
            }
        }
        private bool _isSwitchedOn = false;

        public event CustomToggleEventHandler OnValueChanged;

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            IsSwitchedOn = !IsSwitchedOn;
        }

        protected virtual void OnValueChangedInvocation()
        {
            OnValueChanged?.Invoke(IsSwitchedOn);
        }

        private void OnDestroy()
        {
            OnValueChanged = null;
        }
    }
}
