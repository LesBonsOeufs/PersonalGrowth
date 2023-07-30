using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.BilliardBattle {
    public delegate void DragShootEventHandler(DragShoot sender);

    [RequireComponent(typeof(Rigidbody))]
    public class DragShoot : MonoBehaviour
    {
        [SerializeField] private float dragPowerPerPixel = 5f;

        private Vector2 startDragPosition;

        public event DragShootEventHandler OnDrag;
        public event DragShootEventHandler OnShoot;

        private void OnMouseDown()
        {
            if (!enabled)
                return;

            startDragPosition = Input.mousePosition;
            OnDrag?.Invoke(this);
        }

        private void OnMouseUp()
        {
            if (!enabled)
                return;

            OnShoot?.Invoke(this);
            Vector2 lDeltaDrag = (Vector2)Input.mousePosition - startDragPosition;
            GetComponent<Rigidbody>().AddForce(-new Vector3(lDeltaDrag.x, 0f, lDeltaDrag.y) * dragPowerPerPixel, ForceMode.Impulse);
        }

        private void OnDestroy()
        {
            OnDrag = null;
            OnShoot = null;
        }
    }
}
