using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.PodometerSystem.UI 
{
    [CreateAssetMenu(menuName = "ScreensSwiper/SwipableScreenInfo")]
    public class SwipableScreenInfo : ScriptableObject
    {
        [SerializeField] private RectTransform _prefab = default;
        [SerializeField] private Sprite _menuIcon = default;

        public RectTransform Prefab => _prefab;
        public Sprite ScreensBandLogo => _menuIcon;
    }
}