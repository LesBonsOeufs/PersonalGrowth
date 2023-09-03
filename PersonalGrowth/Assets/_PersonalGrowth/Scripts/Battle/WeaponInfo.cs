using NaughtyAttributes;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle {
    [CreateAssetMenu(fileName = "Scriptables", menuName = "ScriptableObjects/WeaponInfo", order = 1)]
    public class WeaponInfo : ScriptableObject
    {
        [SerializeField] private string _name = "Weapon";
        [SerializeField] private int _price = 100;
        [SerializeField, ShowAssetPreview(1024, 1024)] private Sprite _sprite = default;

        [Foldout("Gameplay"), SerializeField] private int _shape; //TO CHANGE, MUST BE A 3x3 GRID
        [Foldout("Gameplay"), SerializeField] private int _power = 1;
        [Foldout("Gameplay"), SerializeField] private int _durability = 3;

        public string Name => _name;
        public int Price => _price;
        public Sprite Sprite => _sprite;
        public int Shape => _shape;
        public int Power => _power;
        public int Durability => _durability;
    }
}