using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.MainMenu.UI.Map {
    [RequireComponent(typeof(RectTransform))]
    public class MapTrail : Trail
    {
        public const int STEPS_PER_1000_UNIT = 1000;

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

        public override void SetExtents(Vector2 start, Vector2 end)
        {
            base.SetExtents(start, end);

            StepsDistance = Mathf.FloorToInt((end - start).magnitude * (STEPS_PER_1000_UNIT / 1000f));
            Debug.Log("Steps distance: " + StepsDistance);
        }
    }
}