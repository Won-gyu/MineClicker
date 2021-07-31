using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;

namespace Mine
{
    public class CarryOrePoolGroups : MonoSingleton<CarryOrePoolGroups>
    {
        [SerializeField]
        private List<CarryOrePoolGroup> groups;

        public PooledGameObject GetCarryOreObject(int oreId)
        {
            return groups[oreId].GetPoolObject(Random.Range(0, groups.Count));
        }
    }
}