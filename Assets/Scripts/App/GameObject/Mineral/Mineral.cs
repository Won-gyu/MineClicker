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
        private List<int> oreIds;
        public int GetRandomOreId()
        {
            return oreIds[Random.Range(0, oreIds.Count)];
        }
    }
}