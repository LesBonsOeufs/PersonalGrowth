using Com.GabrielBernabeu.PersonalGrowth.UI.Map.Spots;
using System;

namespace Com.GabrielBernabeu
{
    [Serializable]
    public struct MapPositionData
    {
        /// <summary>
        /// Index from Map's spots list
        /// </summary>
        public int lastSpotIndex;

        /// <summary>
        /// Index from lastSpot's nextSpots list
        /// </summary>
        public int lastChosenNextSpotIndex;
        public int pathStepsProgress;
    }
}
