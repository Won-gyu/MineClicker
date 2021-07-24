using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public class Ore : MonoBehaviour
    {
        [SerializeField]
        private GoalOnFloor goalOnFloor;
        public GoalOnFloor GoalOnFloor
        {
            get
            {
                return goalOnFloor;
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
        }
#endif
        public int GetRandomCarryOreId()
        {
            return carryOreIds[Random.Range(0, carryOreIds.Count)];
        }
    }
}