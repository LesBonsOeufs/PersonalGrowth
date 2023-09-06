using Com.GabrielBernabeu.PersonalGrowth.Battle;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.UI.Collection {
    public class CollectionInventory : Inventory
    {
        [SerializeField] private int nMaxWeapons = 3;

        public bool IsFull => weapons.Count >= nMaxWeapons;

        public override void AddWeapon(WeaponInfo info)
        {
            if (weapons.Count >= nMaxWeapons)
                return;

            base.AddWeapon(info);
        }
    }
}