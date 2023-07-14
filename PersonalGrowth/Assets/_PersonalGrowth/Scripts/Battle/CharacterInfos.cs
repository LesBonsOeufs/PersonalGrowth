using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle {
    [CreateAssetMenu(fileName = "Scriptables", menuName = "ScriptableObjects/CharacterInfos", order = 1)]
    public class CharacterInfos : ScriptableObject
    {
        [SerializeField] private Texture2D _texture;
        [SerializeField] private string _name;

        public Texture2D Texture => _texture;
        public string Name => _name;
    }
}
