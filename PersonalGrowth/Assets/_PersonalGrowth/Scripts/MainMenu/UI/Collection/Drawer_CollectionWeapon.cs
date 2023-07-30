using Com.GabrielBernabeu.PersonalGrowth.ColumnsBattle;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Com.GabrielBernabeu.PersonalGrowth.MainMenu
{
    public class Drawer_CollectionWeapon : MonoBehaviour
    {
        [SerializeField] private WeaponInfo info = default;

        [Header("Content")]
        [SerializeField] private Image weaponImage = default;
        [SerializeField] private TextMeshProUGUI priceTmp = default;

        private void OnValidate()
        {
            weaponImage.sprite = info.Sprite;
            priceTmp.text = info.Price.ToString();
        }
    }
}
