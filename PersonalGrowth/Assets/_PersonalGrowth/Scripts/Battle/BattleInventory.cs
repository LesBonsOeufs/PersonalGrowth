
using Com.GabrielBernabeu.PersonalGrowth.UI.Collection;
using DG.Tweening;
using UnityEngine.UI;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle 
{
    public delegate void BattleInventoryEventHandler(WeaponInfo weaponInfo);
    public class BattleInventory : Inventory
    {
        private Drawer_InventoryWeapon lastEquipped;
        
        public event BattleInventoryEventHandler OnEquipWeapon;

        public override void AddWeapon(WeaponInfo info)
        {
            base.AddWeapon(info);

            Drawer_InventoryWeapon lInventoryWeaponDrawer = weapons[^1];

            //Equip first weapon instantly
            if (weapons.Count == 1)
                EquipWeapon(lInventoryWeaponDrawer);

            lInventoryWeaponDrawer.GetComponent<Button>().onClick.AddListener(() => 
            {
                EquipWeapon(lInventoryWeaponDrawer);

                foreach (Drawer_InventoryWeapon weapon in weapons)
                    StartCooldown(weapon);
            });
        }

        private void EquipWeapon(Drawer_InventoryWeapon inventoryWeapon)
        {
            OnEquipWeapon?.Invoke(inventoryWeapon.Info);
            lastEquipped = inventoryWeapon;
        }

        public void StartCooldownOnLastEquipped() => StartCooldown(lastEquipped);

        private void StartCooldown(Drawer_InventoryWeapon weapon)
        {
            ChargingSign lChargingSign = weapon.GetComponentInChildren<ChargingSign>();
            ShowHideTweener lShowHideTweener = weapon.GetComponentInChildren<ShowHideTweener>();
            lChargingSign.Status = EChargeStatus.BLOCKED;
            lChargingSign.Value = 1f;
            lChargingSign.DOKill();
            lShowHideTweener.Show();

            DOVirtual.Float(1f, 0f, weapon.Info.CooldownDuration,
                (float value) =>
                {
                    lChargingSign.Value = value;
                }).OnComplete(() =>
                {
                    lShowHideTweener.Hide();
                })
                .SetEase(Ease.Linear)
                .SetTarget(lChargingSign);
        }
    }
}