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
    }
}