using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;

namespace Mine
{
    public class CarryOrePoolGroup : MonoBehaviour
    {
        [SerializeField]
        private List<GameObjectPool> pools;

        public PooledGameObject GetPoolObject(int groupIndex)
        {
            if (groupIndex > pools.Count)
            {
                Debug.LogError("pools are set wrong");
                return null;
            }

            return pools[groupIndex].GetObject().GetComponent<PooledGameObject>();
        }
    }
}