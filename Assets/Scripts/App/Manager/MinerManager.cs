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
        [SerializeField]
        private MinerDataSO minerData;
        public MinerDataSO MinerData
        {
            get
            {
                return minerData;
            }
        }

        public void CreateMiner(Basement basement)
        {
            var go = Instantiate(prefabMiner) as GameObject;
            go.name = prefabMiner.name;
            go.transform.SetParent(transform, false);
            go.transform.position = basement.PositionSpawner;
            go.GetComponent<Miner>().Init(basement);
        }
    }
}