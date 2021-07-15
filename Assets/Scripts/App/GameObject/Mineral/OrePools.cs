using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;

namespace Mine
{
    public class OrePools : MonoSingleton<OrePools>
    {
        [SerializeField]
        private List<GameObjectPool> pools;

        public PooledGameObject GetPoolObject(int oreId)
        {
            if (oreId > pools.Count)
            {
                Debug.LogError("pools are set wrong");
                return null;
            }

            return pools[oreId].GetObject().GetComponent<PooledGameObject>();
        }
    }
}