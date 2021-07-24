using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;

namespace Mine
{
    public class OreManager : MonoSingleton<OreManager>
    {
        public const string EVENT_EXEC_STORE_ORE_PILE = "Game_ExecStoreOrePile";
        public const string EVENT_ORE_PILE_STORED = "Game_OrePileStored";

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

        private List<int> OrePileRange
        {
            get
            {
                return oreData.orePileRange;
            }
        }

        private int totalSizeLength;

        private void Awake()
        {
            totalSizeLength = System.Enum.GetValues(typeof(OrePileSize)).Length;
        }

        public int GetOrePileRange(OrePileSize size)
        {
            return OrePileRange[(int)size];
        }

        public OrePileSize GetOrePileSize(int count)
        {
            for (int i = totalSizeLength - 1; i >= 0; i--)
            {
                if (i == 0 || count < GetOrePileRange((OrePileSize)i))
                {
                    return (OrePileSize)i;
                }
            }
            Debug.LogError("Shoudln't reach here");
            return OrePileSize.Small;
        }

        public PooledGameObject CreateOrePile(int oreId, OrePileSize size)
        {
            return orePilePoolGroups.GetOrePileObject(oreId, size);
        }
    }
}