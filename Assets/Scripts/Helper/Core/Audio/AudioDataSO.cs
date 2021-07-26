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
    public class AudioDataInfo
    {
        public string id;
        public AudioClip clip;
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
        [BoxGroup("Interpreted Data")]
        public List<AudioDataInfo> audioDataInfos;

        private Dictionary<string, AudioDataInfo> audioDataDict;

        public void Initialize()
        {
            audioDataDict = new Dictionary<string, AudioDataInfo>();
            for (int i = 0; i < audioDataInfos.Count; i++)
            {
                audioDataDict.Add(audioDataInfos[i].id, audioDataInfos[i]);
            }
        }

        public AudioDataInfo GetAudioData(string id)
        {
            return audioDataDict[id];
        }

#if UNITY_EDITOR
        [Button]
        protected override void ApplySheetInfos()
        {
            string path = string.Format("{0}/{1}/{2}", Application.dataPath, shteetTableObjectDirectory, "AudioDataSheet.json");
            sheetAudioDataInfos = FileUtils.FromJson<SpreadSheetAudioDataInfos>(path);

            audioDataInfos = new List<AudioDataInfo>();
            for (int i = 0; i < sheetAudioDataInfos.AudioData.Count; i++)
            {
                audioDataInfos.Add(new AudioDataInfo
                {
                    id = sheetAudioDataInfos.AudioData[i].id,
                    clip = AssetBundleManager.LoadAssetForEditor<AudioClip>("audio", sheetAudioDataInfos.AudioData[i].clip)
                });
            }
        }
#endif
    }
}
