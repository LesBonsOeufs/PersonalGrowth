using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.UI.Map {
    [RequireComponent(typeof(RectTransform))]
    public class Trail : MonoBehaviour
    {
        [SerializeField, Range(0f, 1000f)] private float thickness = 150f;

        protected Vector2 start;
        protected Vector2 end;

        public float Thickness => thickness;

        public void SetThickness(float thickness)
        {
            this.thickness = thickness;
        }

        /// <summary>
        /// Use anchored positions
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public virtual void SetExtents(Vector2 start, Vector2 end)
        {
            this.start = start;
            this.end = end;

            RectTransform lRectTransform = GetComponent<RectTransform>();
            Vector2 lStartToEnd = end - start;
            float lAngle = Vector2.SignedAngle(Vector2.right, lStartToEnd);

            lRectTransform.anchoredPosition = start + lStartToEnd * 0.5f;
            lRectTransform.rotation = Quaternion.Euler(0f, 0f, lAngle);
            lRectTransform.sizeDelta = new Vector2(lStartToEnd.magnitude, thickness);
        }

        private void OnValidate()
        {
            RectTransform lRectTransform = GetComponent<RectTransform>();
            lRectTransform.sizeDelta = new Vector2(lRectTransform.sizeDelta.x, thickness);
        }
    }
}