using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;

namespace Mine
{
    public class SpaceManager : MonoSingleton<SpaceManager>
    {
        [SerializeField]
        private PathFinder pathFinder;

        // public void CreateMiner(Basement basement)
        // {
        //     var go = Instantiate(prefabMiner) as GameObject;
        //     go.name = prefabMiner.name;
        //     go.transform.SetParent(transform, false);
        //     go.transform.position = basement.PositionSpawner;
        //     go.GetComponent<Miner>().Init(basement);
        // }
    }
}