using Com.GabrielBernabeu.PersonalGrowth.ColumnsBattle;
using TMPro;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.MainMenu.UI.Collection {
    public class Drawer_CollectionWeapon : Drawer_InventoryWeapon
    {
        [SerializeField] private TextMeshProUGUI priceTmp = default;

        public override void SetInfos(WeaponInfo info)
        {
            base.SetInfos(info);
            priceTmp.text = info.Price.ToString();
        }
    }
}