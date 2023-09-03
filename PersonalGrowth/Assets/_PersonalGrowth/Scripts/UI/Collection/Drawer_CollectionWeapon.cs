using Com.GabrielBernabeu.PersonalGrowth.Battle;
using TMPro;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.UI.Collection {
    public class Drawer_CollectionWeapon : Drawer_InventoryWeapon
    {
        [SerializeField] private TextMeshProUGUI priceTmp = default;

        public override void SetInfo(WeaponInfo info)
        {
            base.SetInfo(info);
            priceTmp.text = info.Price.ToString();
        }
    }
}