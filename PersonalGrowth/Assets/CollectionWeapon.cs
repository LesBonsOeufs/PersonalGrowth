using Com.GabrielBernabeu.PersonalGrowth.ColumnsBattle;
using Com.GabrielBernabeu.PersonalGrowth.MainMenu;
using Com.GabrielBernabeu.PersonalGrowth.MainMenu.UI.Collection;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Com.GabrielBernabeu
{
    public delegate void CollectionWeaponEventHandler (WeaponInfo info);
    [RequireComponent(typeof(Button))]
    public class CollectionWeapon : MonoBehaviour
    {
        public static event CollectionWeaponEventHandler OnPaid;

        [SerializeField] private WeaponInfo info = null;
        [SerializeField] private Drawer_CollectionWeapon drawer = default;

        [Header("PopUp")]
        [SerializeField] private BinaryQuestion popUp = default;
        [SerializeField] private Drawer_CollectionWeapon popUpDrawer = default;

        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(Button_OnClick);
        }

        private async void Button_OnClick()
        {
            popUpDrawer.SetInfos(info);
            bool lResponse = await popUp.AskBinaryQuestion();

            if (!lResponse)
                return;

            //Check if Inventory can take more weapon
            //Check if player has enough Coins
            //Add MMF Player for losing coins
            StepCoinsManager.Instance.Consume(info.Price);
            OnPaid?.Invoke(info);
        }

        private void OnValidate()
        {
            drawer.SetInfos(info);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(Button_OnClick);
        }
    }
}