using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public class OreHolder : MonoBehaviour
    {
        private PooledGameObject ore;

        public void ReturnOre()
        {
            if (ore != null)
            {
                ore.ReturnToPool();
                ore = null;
            }
        }

        public void HoldOre(int oreId)
        {
            if (ore == null)
            {
                ore = OrePools.Instance.GetPoolObject(oreId);
                ore.transform.SetParent(transform, false);
                // ore.transform.localPosition = Vector2.zero;
            }
        }
    }
}