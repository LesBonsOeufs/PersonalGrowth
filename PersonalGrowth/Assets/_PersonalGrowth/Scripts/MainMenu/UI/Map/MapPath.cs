using UnityEngine;

namespace Com.GabrielBernabeu
{
    [RequireComponent(typeof(RectTransform))]
    public class MapPath : MonoBehaviour
    {
        public const int STEPS_PER_UNIT = 1;

        [SerializeField, Range(0f, 1000f)] private float thickness = 150f;

        /// <summary>
        /// Use anchored positions
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void SetExtents(Vector2 start, Vector2 end)
        {
            RectTransform lRectTransform = GetComponent<RectTransform>();
            Vector2 lStartToEnd = end - start;
            float lExtentsDistance = lStartToEnd.magnitude;
            float lAngle = Vector2.SignedAngle(Vector2.right, lStartToEnd);

            lRectTransform.anchoredPosition = start + lStartToEnd * 0.5f;
            lRectTransform.rotation = Quaternion.Euler(0f, 0f, lAngle);
            lRectTransform.sizeDelta = new Vector2(lExtentsDistance, thickness);

            Debug.Log("Steps distance: " + lExtentsDistance * STEPS_PER_UNIT);
        }

        private void OnValidate()
        {
            RectTransform lRectTransform = GetComponent<RectTransform>();
            lRectTransform.sizeDelta = new Vector2(lRectTransform.sizeDelta.x, thickness);
        }
    }
}