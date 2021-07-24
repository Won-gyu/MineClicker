using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;

namespace Mine
{
    public class OreManager : MonoSingleton<OreManager>
    {
        public const string EVENT_EXEC_STORE_ORE = "Game_ExecStoreOre";
        public const string EVENT_ORE_STORED = "Game_OreStored";

        [SerializeField]
        private OreDataSO oreData;
        public OreDataSO OreData
        {
            get
            {
                return oreData;
            }
        }

        [SerializeField]
        private OrePilePoolGroups orePilePoolGroups;

        public int TotalOreCount
        {
            get
            {
                return oreData.prefabOres.Count;
            }
        }

        public PooledGameObject CreateOrePile(int oreId, OrePileSize size)
        {
            return orePilePoolGroups.GetOrePileObject(oreId, size);
        }
    }
}