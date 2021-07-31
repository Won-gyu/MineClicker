using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Mine
{
    public class Ore : MonoBehaviour
    {
        [SerializeField]
        private WidePlace widePlace;
        public WidePlace WidePlace
        {
            get
            {
                return widePlace;
            }
        }

        [SerializeField]
        private List<int> carryOreIds;
        [SerializeField]
        private int oreId;
        public int OreId
        {
            get
            {
                return oreId;
            }
        }

#if UNITY_EDITOR
        public void SetOreId(int oreId)
        {
            this.oreId = oreId;
            PrefabUtility.RecordPrefabInstancePropertyModifications(this);
        }
#endif
        public int GetRandomCarryOreId()
        {
            return carryOreIds[Random.Range(0, carryOreIds.Count)];
        }
    }
}