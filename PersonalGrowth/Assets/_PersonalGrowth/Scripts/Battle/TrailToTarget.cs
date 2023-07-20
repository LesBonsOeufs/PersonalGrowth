using System.Net;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle {
    [RequireComponent(typeof(RectTransform))]
    public class TrailToTarget : Singleton<TrailToTarget>
    {
        [SerializeField] private RectTransform trail = default;
        [SerializeField] private Transform tip = default;

        private RectTransform rectTransform;
        private Canvas canvas;
        private Transform target;

        protected override void Awake()
        {
            base.Awake();

            rectTransform = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
        }

        private void Update()
        {
            if (target == null)
                return;

            Camera lMainCamera = Camera.main;
            MoveToMouse(lMainCamera);

            Vector2 lTargetScreenPosition = lMainCamera.WorldToScreenPoint(target.position);
            Vector2 lTransformScreenPosition = lMainCamera.WorldToScreenPoint(transform.position);

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

        private void MoveToMouse(Camera camera)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle
                (rectTransform, Input.mousePosition, camera, out Vector3 lWorldMousePosition);

            transform.position = lWorldMousePosition;
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
