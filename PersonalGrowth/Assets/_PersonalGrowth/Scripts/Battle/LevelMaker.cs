using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle {
    public class LevelMaker : MonoBehaviour
    {
        public static LevelInfos currentLevel = null;

        [SerializeField] private LevelInfos debugCurrentLevel = default;
        [SerializeField] private BallUnit enemyUnitPrefab = default;

        private void Start()
        {
            Make(debugCurrentLevel);
        }

        private void Make(LevelInfos level)
        {
            foreach (LevelInfos.LevelUnit levelUnit in level.Units)
            {
                Instantiate(enemyUnitPrefab, levelUnit.position, Quaternion.identity, null);
            }
        }
    }
}
