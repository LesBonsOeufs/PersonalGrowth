using UnityEngine;
using UnityEngine.UI;

namespace Com.GabrielBernabeu.PersonalGrowth.UI.Map.Spots {
    [RequireComponent(typeof(RectTransform))]
    public class BattleSpot : MapSpot
    {
        public override event MapSpotEventHandler OnActionCompleted;

        public override void StartAction()
        {
            Button lFightBtn = Map.Instance.FightBtn;

            lFightBtn.interactable = true;
            lFightBtn.onClick.AddListener(FightBtn_OnClick);
        }

        private void FightBtn_OnClick()
        {
            Button lFightBtn = Map.Instance.FightBtn;

            lFightBtn.interactable = false;
            lFightBtn.onClick.RemoveListener(FightBtn_OnClick);
            Debug.Log("Fight!");
            OnActionCompleted?.Invoke(this);
        }
    }
}