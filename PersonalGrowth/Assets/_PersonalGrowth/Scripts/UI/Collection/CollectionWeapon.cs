using Com.GabrielBernabeu.PersonalGrowth.ColumnsBattle;
using UnityEngine;
using UnityEngine.UI;

namespace Com.GabrielBernabeu.PersonalGrowth.UI.Collection {
    public delegate void CollectionWeaponEventHandler (WeaponInfo info);
    [RequireComponent(typeof(Button))]
    public class CollectionWeapon : MonoBehaviour
    {
        public static event CollectionWeaponEventHandler OnChosen;

        [SerializeField] private WeaponInfo info = null;
        [SerializeField] private Drawer_CollectionWeapon drawer = default;

        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(Button_OnClick);
        }

        private void Button_OnClick()
        {
            OnChosen?.Invoke(info);
        }

        private void OnValidate()
        {
            drawer.SetInfos(info);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(Button_OnClick);
        }
    }
}