using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace Mine
{
#if UNITY_5_3_OR_NEWER
    [CreateAssetMenu(fileName = "OreDataSO", menuName = "Mine/ScriptableObject/OreData", order = 1)]
#endif
    public class OreDataSO : ScriptableObject
    {
        [BoxGroup("Pile")]
        public List<GameObject> prefabPiles;
    }
}
