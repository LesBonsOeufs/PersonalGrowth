using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.UI.Map {
    [RequireComponent(typeof(RectTransform))]
    public class BattleSpot : MapSpot
    {
        public override void StartAction()
        {
            Debug.Log("Fight!");
        }
    }
}