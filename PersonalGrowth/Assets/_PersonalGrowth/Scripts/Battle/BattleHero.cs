using NaughtyAttributes;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle {
    public class BattleHero : MonoBehaviour
    {
        [SerializeField] private EquippedWeapon equippedWeapon;
        [SerializeField] private ChargingSign chargingSign;
        [SerializeField] private BattleInventory inventory;

        [SerializeField, ReadOnly] private float chargeCounter = 0f;
        [SerializeField, ReadOnly] private float cooldownCounter = 0f;

        public EScreenSide GetTouchSide
        {
            get
            {
                bool lIsLeftSide = Input.mousePosition.x < Screen.width * 0.5f;
                return lIsLeftSide ? EScreenSide.LEFT : EScreenSide.RIGHT;
            }
        }

        private void Awake()
        {
            inventory.OnEquipWeapon += BattleInventory_OnEquipWeapon;
        }

        private void BattleInventory_OnEquipWeapon(WeaponInfo weaponInfo)
        {
            equippedWeapon.info = weaponInfo;
            StartCooldown();
        }

        private void Update()
        {
            bool lTouchHold = Input.GetMouseButton(0);

            if (lTouchHold)
                equippedWeapon.Side = GetTouchSide;

            if (cooldownCounter > 0f)
            {
                cooldownCounter -= Time.deltaTime;

                if (cooldownCounter < 0f)
                    cooldownCounter = 0f;

                chargingSign.Value = cooldownCounter / equippedWeapon.info.CooldownDuration;
                return;
            }

            float lChargeDuration = equippedWeapon.info.ChargeDuration;

            if (lTouchHold)
            {
                if (chargeCounter != lChargeDuration)
                {
                    chargingSign.Status = EChargeStatus.GROW;
                    chargeCounter += Time.deltaTime;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (chargeCounter >= lChargeDuration)
                    Strike();
                else
                    chargingSign.Status = EChargeStatus.SHRINK;
            }
            else
                chargeCounter -= Time.deltaTime;

            chargeCounter = Mathf.Clamp(chargeCounter, 0f, lChargeDuration);

            if (chargeCounter == lChargeDuration)
                chargingSign.Status = EChargeStatus.FULL;

            chargingSign.Value = chargeCounter / lChargeDuration;
        }

        private void Strike()
        {
            equippedWeapon.Strike(
                () => 
                {
                    StartCooldown();
                    inventory.StartCooldownOnLastEquipped();
                });
        }

        private void StartCooldown()
        {
            chargeCounter = 0f;
            cooldownCounter = equippedWeapon.info.CooldownDuration;
            chargingSign.Status = EChargeStatus.BLOCKED;
        }

        private void OnDestroy()
        {
            inventory.OnEquipWeapon -= BattleInventory_OnEquipWeapon;
        }
    }
}