using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;

namespace Mine
{
    public class OrePilePoolGroup : MonoBehaviour
    {
        [SerializeField]
        private List<GameObjectPool> pools;

        public PooledGameObject GetPoolObject(OrePileSize size)
        {
            int sizeIndex = (int)size;
            if (sizeIndex > pools.Count)
            {
                Debug.LogError("pools are set wrong");
                return null;
            }

            return pools[sizeIndex].GetObject().GetComponent<PooledGameObject>();
        }
    }
}