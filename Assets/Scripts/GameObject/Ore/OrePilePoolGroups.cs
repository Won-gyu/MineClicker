using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;

namespace Mine
{
    public class OrePilePoolGroups : MonoSingleton<OrePilePoolGroups>
    {
        [SerializeField]
        private List<OrePilePoolGroup> groups;

        public PooledGameObject GetGroupObject(int orePileId, int sizeIndex)
        {
            return groups[orePileId].GetPoolObject(sizeIndex);
        }
    }
}