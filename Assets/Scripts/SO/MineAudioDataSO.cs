using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace Mine
{
#if UNITY_5_3_OR_NEWER
    [CreateAssetMenu(fileName = "MineAudioDataSO", menuName = "Mine/ScriptableObject/MineAudioDataSO", order = 0)]
#endif
    public class MineAudioDataSO : ScriptableObject
    {
        [BoxGroup("Miner")]
        public List<string> audioIdsArrowHit;

        public string GetRandomAudioIdArrowHit()
        {
            return audioIdsArrowHit[UnityEngine.Random.Range(0, audioIdsArrowHit.Count)];
        }
    }
}
