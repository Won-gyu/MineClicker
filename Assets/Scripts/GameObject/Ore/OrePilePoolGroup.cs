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

        public PooledGameObject GetPoolObject(int orePileId)
        {
            if (orePileId > pools.Count)
            {
                Debug.LogError("pools are set wrong");
                return null;
            }

            return pools[orePileId].GetObject().GetComponent<PooledGameObject>();
        }
    }
}