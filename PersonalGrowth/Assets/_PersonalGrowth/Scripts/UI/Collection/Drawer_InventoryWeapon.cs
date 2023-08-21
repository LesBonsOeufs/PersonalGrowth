using Com.GabrielBernabeu.PersonalGrowth.ColumnsBattle;
using UnityEngine;
using UnityEngine.UI;

namespace Com.GabrielBernabeu.PersonalGrowth.UI.Collection {
    public class Drawer_InventoryWeapon : MonoBehaviour
    {
        [SerializeField] private Image weaponImage = default;

        public virtual void SetInfos(WeaponInfo info)
        {
            weaponImage.sprite = info.Sprite;
        }
    }
}