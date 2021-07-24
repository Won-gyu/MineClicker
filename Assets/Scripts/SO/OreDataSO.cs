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
        [BoxGroup("Ore")]
        public List<int> orePileRange;
        [BoxGroup("Ore")]
        public List<Ore> prefabOres;
#if UNITY_EDITOR
        [Button]
        private void ApplyOres()
        {
            for (int i = 0; i < prefabOres.Count; i++)
            {
                prefabOres[i].SetOreId(i);
            }
        }
#endif
        [BoxGroup("Pile")]
        public List<GameObject> prefabPileGroups;
    }
}
