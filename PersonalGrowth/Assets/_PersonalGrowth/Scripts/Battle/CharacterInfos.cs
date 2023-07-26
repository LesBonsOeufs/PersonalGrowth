using NaughtyAttributes;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle {
    [CreateAssetMenu(fileName = "Scriptables", menuName = "ScriptableObjects/CharacterInfos", order = 1)]
    public class CharacterInfos : ScriptableObject
    {
        [SerializeField, ShowAssetPreview] private Material _material;
        [SerializeField] private string _name;

        public Material Material => _material;
        public string Name => _name;
    }
}
