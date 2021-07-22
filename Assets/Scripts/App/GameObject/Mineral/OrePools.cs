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

        public PooledGameObject GetPoolObject(int carryOreId)
        {
            if (carryOreId > pools.Count)
            {
                Debug.LogError("pools are set wrong");
                return null;
            }

            return pools[carryOreId].GetObject().GetComponent<PooledGameObject>();
        }
    }
}