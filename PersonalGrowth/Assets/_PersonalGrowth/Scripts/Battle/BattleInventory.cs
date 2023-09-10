
using Com.GabrielBernabeu.PersonalGrowth.PodometerSystem;
using Com.GabrielBernabeu.PersonalGrowth.UI.Collection;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle 
{
    public delegate void BattleInventoryEventHandler(WeaponInfo info, int inventoryIndex);
    public class BattleInventory : Inventory
    {
        public event BattleInventoryEventHandler OnEquipWeapon;
        public event BattleInventoryEventHandler OnRemoveWeapon;

        protected virtual void Awake()
        {
            EnemyDefense.OnBlock += EnemyDefense_OnBlock;
        }

        private void EnemyDefense_OnBlock()
        {
            RemoveRandomWeapon();
        }

        public override void AddWeapon(WeaponInfo info)
        {
            base.AddWeapon(info);

            int lLastIndex = weapons.Count - 1;
            Drawer_InventoryWeapon lInventoryWeaponDrawer = weapons[lLastIndex];

            //Equip first weapon instantly
            if (weapons.Count == 1)
                OnEquipWeapon?.Invoke(info, lLastIndex);

            lInventoryWeaponDrawer.GetComponent<Button>().onClick.AddListener(() => 
            {
                OnEquipWeapon?.Invoke(info, lLastIndex);
            });
        }

        public void EquipRandomWeapon()
        {
            int lRandomIndex = Random.Range(0, weapons.Count);
            WeaponInfo lInfo = weapons[lRandomIndex].Info;
            OnEquipWeapon?.Invoke(lInfo, lRandomIndex);
        }

        private void RemoveRandomWeapon()
        {
            int lRandomIndex = Random.Range(0, weapons.Count);
            Drawer_InventoryWeapon lWeapon = weapons[lRandomIndex];
            weapons.RemoveAt(lRandomIndex);
            lWeapon.GetComponent<Button>().onClick.RemoveAllListeners();

            //Save removed weapon
            LocalDataSaver<LocalData>.CurrentData.inventory.weaponInfoAddresses.Remove(lWeapon.Info.name);
            LocalDataSaver<LocalData>.SaveCurrentData();

            OnRemoveWeapon?.Invoke(lWeapon.Info, lRandomIndex);

            lWeapon.transform.DOScale(0f, 1f).SetEase(Ease.InSine)
                .OnComplete(() =>
                {
                    Destroy(lWeapon.gameObject);

                    //DEBUG!!! GAMEOVER
                    if (weapons.Count == 0)
                        SceneManager.LoadScene(0);
                    //DEBUG!!!
                });
        }

        public void StartCooldowns()
        {
            foreach (Drawer_InventoryWeapon weapon in weapons)
                StartCooldown(weapon);
        }

        public void StartCooldown(int index) => StartCooldown(weapons[index]);

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

        private void OnDestroy()
        {
            EnemyDefense.OnBlock -= EnemyDefense_OnBlock;
        }
    }
}