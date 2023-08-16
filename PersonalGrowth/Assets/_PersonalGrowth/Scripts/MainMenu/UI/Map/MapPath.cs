using UnityEngine;

namespace Com.GabrielBernabeu
{
    [RequireComponent(typeof(RectTransform))]
    public class MapPath : MonoBehaviour
    {
        public const int STEPS_PER_1000_UNIT = 1000;

        [SerializeField, Range(0f, 1000f)] private float thickness = 150f;

        private Vector2 start;
        private Vector2 end;

        public int StepsDistance { get; private set; }

        public Vector2 GetAnchoredPositionFromStepCoins(int nStepCoins)
        {
            Vector2 lStartToEnd = end - start;
            return start + lStartToEnd * ((float)nStepCoins / StepsDistance);
        }

        public bool IsPathCompleted(int nStepCoins)
        {
            return nStepCoins >= StepsDistance;
        }

        /// <summary>
        /// Use anchored positions
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void SetExtents(Vector2 start, Vector2 end)
        {
            this.start = start;
            this.end = end;

            RectTransform lRectTransform = GetComponent<RectTransform>();
            Vector2 lStartToEnd = end - start;
            float lExtentsDistance = lStartToEnd.magnitude;
            float lAngle = Vector2.SignedAngle(Vector2.right, lStartToEnd);

            lRectTransform.anchoredPosition = start + lStartToEnd * 0.5f;
            lRectTransform.rotation = Quaternion.Euler(0f, 0f, lAngle);
            lRectTransform.sizeDelta = new Vector2(lExtentsDistance, thickness);

            StepsDistance = Mathf.FloorToInt(lExtentsDistance * (STEPS_PER_1000_UNIT / 1000f));
            Debug.Log("Steps distance: " + StepsDistance);
        }

        private void OnValidate()
        {
            RectTransform lRectTransform = GetComponent<RectTransform>();
            lRectTransform.sizeDelta = new Vector2(lRectTransform.sizeDelta.x, thickness);
        }
    }
}