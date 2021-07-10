using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;

namespace Mine
{
    public class MinerManager : MonoSingleton<MinerManager>
    {
        [SerializeField]
        private GameObject prefabMiner;

        public void CreateMiner(Transform spawner)
        {
            var go = Instantiate(prefabMiner) as GameObject;
            go.name = prefabMiner.name;
            go.transform.SetParent(transform, false);
            go.transform.position = spawner.position;
        }
    }
}