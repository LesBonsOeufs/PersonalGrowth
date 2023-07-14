using System;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle {
    [RequireComponent(typeof(Rigidbody))]
    public class DragShoot : MonoBehaviour
    {
        [SerializeField] private float dragPowerPerPixel = 5f;

        private new Rigidbody rigidbody;
        private Vector2 startDragPosition;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void OnMouseDown()
        {
            Debug.Log("start");
            startDragPosition = Input.mousePosition;
        }

        private void OnMouseUp()
        {
            Debug.Log("end");
            Vector2 lDeltaDrag = (Vector2)Input.mousePosition - startDragPosition;
            Debug.Log(lDeltaDrag);
            rigidbody.AddForce(-new Vector3(lDeltaDrag.x, 0f, lDeltaDrag.y) * dragPowerPerPixel, ForceMode.Impulse);
        }
    }
}
