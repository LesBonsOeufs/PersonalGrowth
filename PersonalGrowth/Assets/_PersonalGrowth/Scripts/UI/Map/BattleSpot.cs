using UnityEngine;
using UnityEngine.UI;

namespace Com.GabrielBernabeu.PersonalGrowth.UI.Map {
    [RequireComponent(typeof(RectTransform))]
    public class BattleSpot : MapSpot
    {
        [SerializeField] private Button fightBtn;

        public override event MapSpotEventHandler OnActionCompleted;

        public override void StartAction()
        {
            fightBtn.interactable = true;
            fightBtn.onClick.AddListener(FightBtn_OnClick);
        }

        private void FightBtn_OnClick()
        {
            fightBtn.interactable = false;
            fightBtn.onClick.RemoveListener(FightBtn_OnClick);
            Debug.Log("Fight!");
            OnActionCompleted?.Invoke(this);
        }
    }
}