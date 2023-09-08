
using UnityEngine.UI;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle 
{
    public delegate void BattleInventoryEventHandler(WeaponInfo weaponInfo);
    public class BattleInventory : Inventory
    {
        public static event BattleInventoryEventHandler OnEquipWeapon;

        public override void AddWeapon(WeaponInfo info)
        {
            //Equip first weapon instantly
            if (weapons.Count == 0)
                OnEquipWeapon?.Invoke(info);

            base.AddWeapon(info);

            weapons[^1].GetComponent<Button>().onClick.AddListener(() => { OnEquipWeapon?.Invoke(info); });
        }
    }
}