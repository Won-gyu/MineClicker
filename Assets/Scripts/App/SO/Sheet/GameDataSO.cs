using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Sirenix.OdinInspector;
using Helper;

namespace Mine
{
    [Serializable]
    public class SpreadSheetGameDataInfos
    {
        public List<SpreadSheetFloorTierDataInfo> FloorTierData;
    }
#if UNITY_5_3_OR_NEWER
    [CreateAssetMenu(fileName = "GameData", menuName = "Mine/ScriptableObject/GameData", order = 0)]
#endif
    public class GameDataSO : SheetDataBasedSO
    {
        public SpreadSheetGameDataInfos sheetGameDataInfos;

        [Button]
        protected override void ApplySheetInfos()
        {
            string path = string.Format("{0}/{1}/{2}", Application.dataPath, shteetTableObjectDirectory, "GameDataSheet.json");
            sheetGameDataInfos = FileUtils.FromJson<SpreadSheetGameDataInfos>(path);
        }
    }
}