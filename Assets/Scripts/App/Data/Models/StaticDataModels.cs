using System;
using System.Collections.Generic;

namespace Mine
{
    class StaticData
    {
        public const int MAX_FLOOR = 10;
        public const int FLOOR_MINER_LIMIT_COUNT = 10;
    }
    
    [Serializable]
    public class SpreadSheetFloorTierDataInfo
    {
        public int tier;
        public int limitCount;
        public double requiredCredit;
    }
}