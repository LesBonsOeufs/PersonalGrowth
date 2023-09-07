
using UnityEngine.UI;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle 
{
    public delegate void BattleInventoryEventHandler(WeaponInfo weaponInfo);
    public class BattleInventory : Inventory
    {
        public static event BattleInventoryEventHandler OnEquipWeapon;

        public override void AddWeapon(WeaponInfo info)
        {
            base.AddWeapon(info);
            weapons[^1].GetComponent<Button>().onClick.AddListener(() => { OnEquipWeapon?.Invoke(info); });
        }
    }
}