using NaughtyAttributes;
using System.Collections.Generic;

namespace Com.GabrielBernabeu.PersonalGrowth.UI.Map.Spots
{
    public class MultiTrailsSpot : MapSpot
    {
        [ShowNativeProperty] public List<MapSpot> AdditionalNextSpots { get; private set; }

        public override event MapSpotEventHandler OnActionCompleted;

        public override void StartAction()
        {
            //START TRAIL CHOICE
        }
    }
}