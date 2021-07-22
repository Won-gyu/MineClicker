using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public class Mineral : MonoBehaviour
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
        public int GetRandomCarryOreId()
        {
            return carryOreIds[Random.Range(0, carryOreIds.Count)];
        }
    }
}