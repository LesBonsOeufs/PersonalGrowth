using Com.GabrielBernabeu.PersonalGrowth.PodometerSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Com.GabrielBernabeu.PersonalGrowth.UI.Map.Spots {
    [RequireComponent(typeof(RectTransform))]
    public class BattleSpot : MapSpot
    {
        public override void StartAction()
        {
            Button lFightBtn = Map.Instance.FightBtn;

            lFightBtn.interactable = true;
            lFightBtn.onClick.AddListener(FightBtn_OnClick);
        }

        private void FightBtn_OnClick()
        {
            if (LocalDataSaver<LocalData>.CurrentData.inventory.weaponInfoAddresses.Count == 0)
            {
                GeneralTextFeedback.Instance.MakeText("Can't fight with no equipped weapons!");
                return;
            }

            Button lFightBtn = Map.Instance.FightBtn;

            lFightBtn.interactable = false;
            lFightBtn.onClick.RemoveListener(FightBtn_OnClick);
            Debug.Log("Fight!");

            //DEBUG!!!
            SceneManager.LoadScene(1);

            //On Win
            //StartChoosingNextSpot();
        }
    }
}