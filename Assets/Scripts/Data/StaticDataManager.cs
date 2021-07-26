using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;

namespace Mine
{
    public class StaticDataManager : MonoSingleton<StaticDataManager>
    {
        [SerializeField]
        private GameDataSO gameDataSO;

        [SerializeField]
        private MineAudioDataSO mineAudioData;
        public MineAudioDataSO MineAudioData
        {
            get
            {
                return mineAudioData;
            }
        }

        private List<SpreadSheetFloorTierDataInfo> FloorTierData
        {
            get
            {
                return gameDataSO.sheetGameDataInfos.FloorTierData;
            }
        }

        public SpreadSheetFloorTierDataInfo GetFloorTierData(int tier)
        {
            return FloorTierData[tier - 1];
        }
    }
}