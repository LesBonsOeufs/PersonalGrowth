using System.Net;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle {
    [RequireComponent(typeof(RectTransform))]
    public class TrailToTarget : MonoBehaviour
    {
        [SerializeField] private Transform target = default;
        [SerializeField] private RectTransform trail = default;
        [SerializeField] private Transform tip = default;

        private RectTransform rectTransform;
        private Canvas canvas;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
        }

        private void Update()
        {
            Camera lMainCamera = Camera.main;
            Vector2 lTargetScreenPosition = (Vector2)lMainCamera.WorldToScreenPoint(target.position);
            Vector2 lTransformScreenPosition = (Vector2)lMainCamera.WorldToScreenPoint(transform.position);

            RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, lTargetScreenPosition, lMainCamera,
                out Vector3 lProjectedTargetPosition);

            Vector3 lToProjectedTarget = lProjectedTargetPosition - transform.position;
            Vector3 lScreenSpaceToTarget = lTargetScreenPosition - lTransformScreenPosition;
            float lAngle = Vector3.SignedAngle(transform.right, lToProjectedTarget, transform.forward);

            tip.position = transform.position;
            trail.position = transform.position + lToProjectedTarget * 0.5f;
            trail.sizeDelta = new Vector2(lScreenSpaceToTarget.magnitude / canvas.scaleFactor, 50f);
            trail.rotation = transform.rotation * Quaternion.Euler(0f, 0f, lAngle);
        }
    }
}
