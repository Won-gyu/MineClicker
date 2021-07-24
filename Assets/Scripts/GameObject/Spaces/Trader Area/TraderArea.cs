using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public class TraderArea : MonoBehaviour
    {
        [SerializeField]
        private Trader trader;

        [SerializeField]
        private GoalOnFloor startOnFloor;
        public GoalOnFloor StartOnFloor
        {
            get
            {
                return startOnFloor;
            }
        }
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
        private GoalOnFloor dropOff;
        public GoalOnFloor DropOff
        {
            get
            {
                return dropOff;
            }
        }

        private void Awake()
        {
            trader.Init(this);
        }
    }
}