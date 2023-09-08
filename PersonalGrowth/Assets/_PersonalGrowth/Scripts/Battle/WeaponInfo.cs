using NaughtyAttributes;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle {
    [CreateAssetMenu(fileName = "Scriptables", menuName = "ScriptableObjects/WeaponInfo", order = 1)]
    public class WeaponInfo : ScriptableObject
    {
        [SerializeField] private string _name = "Weapon";
        [SerializeField] private int _price = 100;
        [SerializeField, ShowAssetPreview(1024, 1024)] private Sprite _sprite = default;

        [Foldout("Gameplay"), SerializeField] private int _power = 1;
        [Foldout("Gameplay"), SerializeField] private float _chargeDuration;
        [Foldout("Gameplay"), SerializeField] private float _cooldownDuration;

        public string Name => _name;
        public int Price => _price;
        public Sprite Sprite => _sprite;
        public int Power => _power;
        public float ChargeDuration => _chargeDuration;
        public float CooldownDuration => _cooldownDuration;
    }
}