using NaughtyAttributes;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.UI {
    [CreateAssetMenu(menuName = "ScreensSwiper/SwipableScreenInfo")]
    public class SwipableScreenInfo : ScriptableObject
    {
        [SerializeField] private RectTransform _prefab = default;
        [SerializeField, ShowAssetPreview(1024, 1024)] private Sprite _menuIcon = default;

        public RectTransform Prefab => _prefab;
        public Sprite ScreensBandLogo => _menuIcon;
    }
}