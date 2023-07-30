using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.BilliardBattle {
    [CreateAssetMenu(fileName = "Scriptables", menuName = "ScriptableObjects/UnitInfos", order = 1)]
    public class UnitInfos : ScriptableObject
    {
        [SerializeField] private int _maxHealth = 2;
        [SerializeField] private CharacterInfos _characterInfos;

        public int MaxHealth => _maxHealth;
        public CharacterInfos CharacterInfos => _characterInfos;
    }
}
