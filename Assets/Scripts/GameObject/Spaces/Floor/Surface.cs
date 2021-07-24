using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public class Surface : Floor
    {
        [SerializeField]
        private DropOff dropOff;
        public DropOff DropOff
        {
            get
            {
                return dropOff;
            }
        }
        private GoalOnFloor goalDropOff;
        public GoalOnFloor GoalDropOff
        {
            get
            {
                if (goalDropOff == null)
                {
                    goalDropOff = dropOff.GetComponent<GoalOnFloor>();
                }
                return goalDropOff;
            }
        }
    }
}