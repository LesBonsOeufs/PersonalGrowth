using System;
using System.Collections.Generic;

namespace Com.GabrielBernabeu.PersonalGrowth.PodometerSystem {
    [Serializable]
    public class LocalData
    {
        public PodometerData podometer;
        public InventoryData inventory;
        public MapPositionData mapPosition;

        public LocalData()
        {
            inventory.weaponInfoAddresses = new List<string>();
        }

        [Serializable]
        public struct PodometerData
        {
            public DateTime lastUseDay;
            public int nLastTodaySteps;
            public int stepCoinsCount;
        }

        [Serializable]
        public struct InventoryData
        {
            public List<string> weaponInfoAddresses;
        }
    }
}