using Com.GabrielBernabeu.PersonalGrowth.ColumnsBattle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.MainMenu.UI.Collection {
    public class CollectionInventory : MonoBehaviour
    {
        [SerializeField] private Drawer_InventoryWeapon inventoryWeaponPrefab = default;
        [SerializeField] private int nMaxWeapons = 3;

        public bool IsFull => weapons.Count >= nMaxWeapons;

        private List<Drawer_InventoryWeapon> weapons = new List<Drawer_InventoryWeapon>();

        public void AddWeapon(WeaponInfo info)
        {
            if (weapons.Count >= nMaxWeapons)
                return;

            Drawer_InventoryWeapon lWeapon = Instantiate(inventoryWeaponPrefab);
            lWeapon.SetInfos(info);
            weapons.Add(lWeapon);
        }
    }
}