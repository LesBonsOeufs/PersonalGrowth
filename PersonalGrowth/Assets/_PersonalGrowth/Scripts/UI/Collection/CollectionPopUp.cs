using Com.GabrielBernabeu.PersonalGrowth;
using Com.GabrielBernabeu.PersonalGrowth.Battle;
using Com.GabrielBernabeu.PersonalGrowth.PodometerSystem;
using Com.GabrielBernabeu.PersonalGrowth.UI.Collection;
using UnityEngine;
using UnityEngine.UI;

public class CollectionPopUp : MonoBehaviour
{
    [SerializeField] private Button buttonYes;
    [SerializeField] private Button buttonNo;
    [SerializeField] private Drawer_CollectionWeapon weaponDrawer;
    [SerializeField] private CollectionInventory inventory;

    private WeaponInfo selectedWeapon;

    private void Awake()
    {
        CollectionWeapon.OnChosen += CollectionWeapon_OnChosen;
        buttonYes.onClick.AddListener(ButtonYes_OnClick);
        buttonNo.onClick.AddListener(ButtonNo_OnClick);

        gameObject.SetActive(false);
    }

    private void CollectionWeapon_OnChosen(WeaponInfo info)
    {
        selectedWeapon = info;
        weaponDrawer.SetInfo(selectedWeapon);
        gameObject.SetActive(true);
    }

    private void ButtonYes_OnClick()
    {
        if (inventory.IsFull)
        {
            GeneralTextFeedback.Instance.MakeText("Inventory is already full!");
            return;
        }
        else if (StepCoinsManager.Instance.Count < selectedWeapon.Price)
        {
            GeneralTextFeedback.Instance.MakeText("You do not have enough step-coins!");
            return;
        }

        //Save new equipped weapon
        LocalDataSaver<LocalData>.CurrentData.inventory.weaponInfoAddresses.Add(selectedWeapon.name);
        LocalDataSaver<LocalData>.SaveCurrentData();
        inventory.AddWeapon(selectedWeapon);

        StepCoinsManager.Instance.Consume(selectedWeapon.Price);
        gameObject.SetActive(false);
    }

    private void ButtonNo_OnClick() 
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        CollectionWeapon.OnChosen -= CollectionWeapon_OnChosen;
    }
}