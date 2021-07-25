using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace Helper
{
    [Serializable]
    public class SpreadSheetAudioDataInfo
    {
        public string id;
        public string clip;
    }
    [Serializable]
    public class SpreadSheetAudioDataInfos
    {
        public List<SpreadSheetAudioDataInfo> AudioData;
    }
#if UNITY_5_3_OR_NEWER
    [CreateAssetMenu(fileName = "AudioDataSO", menuName = "Helper/ScriptableObject/AudioData", order = 0)]
#endif
    public class AudioDataSO : SheetDataBasedSO
    {
        public SpreadSheetAudioDataInfos sheetAudioDataInfos;

#if UNITY_EDITOR
        [Button]
        protected override void ApplySheetInfos()
        {
            string path = string.Format("{0}/{1}/{2}", Application.dataPath, shteetTableObjectDirectory, "AudioDataSheet.json");
            sheetAudioDataInfos = FileUtils.FromJson<SpreadSheetAudioDataInfos>(path);
        }
#endif
    }
}
