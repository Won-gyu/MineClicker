using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace Mine
{
#if UNITY_5_3_OR_NEWER
    [CreateAssetMenu(fileName = "MinerDataSO", menuName = "App/ScriptableObject/MinerData", order = 0)]
#endif
    public class MinerDataSO : ScriptableObject
    {
        [BoxGroup("costume")]
        public List<GameObject> prefabHeads;
        [BoxGroup("costume")]
        public List<GameObject> prefabFaces;
        [BoxGroup("costume")]
        public List<Color> colorSkins;
    }
}
