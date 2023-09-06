using Com.GabrielBernabeu.PersonalGrowth.Battle;
using Com.GabrielBernabeu.PersonalGrowth.PodometerSystem;
using Com.GabrielBernabeu.PersonalGrowth.UI.Collection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Com.GabrielBernabeu
{
    public abstract class Inventory : MonoBehaviour
    {
        [SerializeField] private Drawer_InventoryWeapon inventoryWeaponPrefab = default;

        protected List<Drawer_InventoryWeapon> weapons = new List<Drawer_InventoryWeapon>();

        private void Start()
        {
            LoadWeapons();
        }

        private async void LoadWeapons()
        {
            //Load equipped weapons
            List<string> lWeaponInfoAddresses = LocalDataSaver<LocalData>.CurrentData.inventory.weaponInfoAddresses;

            foreach (string lAddress in lWeaponInfoAddresses)
            {
                AsyncOperationHandle<WeaponInfo> lHandle = Addressables.LoadAssetAsync<WeaponInfo>(lAddress);
                await lHandle.Task;

                if (lHandle.Status == AsyncOperationStatus.Succeeded)
                    AddWeapon(lHandle.Result);
            }
        }

        public virtual void AddWeapon(WeaponInfo info)
        {
            Drawer_InventoryWeapon lWeapon = Instantiate(inventoryWeaponPrefab, transform);
            lWeapon.SetInfo(info);
            weapons.Add(lWeapon);
        }
    }
}
