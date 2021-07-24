using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;

namespace Mine
{
    public class OreManager : MonoSingleton<OreManager>
    {
        [SerializeField]
        private OreDataSO oreData;
        public OreDataSO OreData
        {
            get
            {
                return oreData;
            }
        }

        public int TotalOreCount
        {
            get
            {
                return oreData.prefabOres.Count;
            }
        }
    }
}