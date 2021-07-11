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

        public void CreateMiner(Floor floor)
        {
            var go = Instantiate(prefabMiner) as GameObject;
            go.name = prefabMiner.name;
            go.transform.SetParent(transform, false);
            go.transform.position = floor.PositionSpawner;
            go.GetComponent<Miner>().Init(floor);
        }
    }
}