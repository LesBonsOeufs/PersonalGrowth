using Com.GabrielBernabeu.PersonalGrowth.Battle;
using UnityEngine;
using UnityEngine.UI;

namespace Com.GabrielBernabeu.PersonalGrowth.UI.Collection {
    public class Drawer_InventoryWeapon : MonoBehaviour
    {
        [SerializeField] private Image weaponImage = default;

        public virtual void SetInfo(WeaponInfo info)
        {
            weaponImage.sprite = info.Sprite;
        }
    }
}