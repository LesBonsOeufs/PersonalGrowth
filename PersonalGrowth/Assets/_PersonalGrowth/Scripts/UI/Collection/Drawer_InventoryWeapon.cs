using Com.GabrielBernabeu.PersonalGrowth.Battle;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Com.GabrielBernabeu.PersonalGrowth.UI.Collection {
    public class Drawer_InventoryWeapon : MonoBehaviour
    {
        [SerializeField] private Image weaponImage = default;

        [ShowNativeProperty] public WeaponInfo Info { get; private set; }

        public virtual void SetInfo(WeaponInfo info)
        {
            weaponImage.sprite = info.Sprite;
            Info = info;
        }
    }
}