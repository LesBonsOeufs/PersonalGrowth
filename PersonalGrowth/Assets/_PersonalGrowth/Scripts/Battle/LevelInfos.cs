using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle {
    [CreateAssetMenu(fileName = "Scriptables", menuName = "ScriptableObjects/LevelInfos", order = 1)]
    public class LevelInfos : ScriptableObject
    {
        [SerializeField] private List<LevelUnit> _units = default;

        public List<LevelUnit> Units => _units;

        [Serializable]
        public struct LevelUnit
        {
            public UnitInfos unitInfos;
            public Vector3 position;
        }
    }
}
