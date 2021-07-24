using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public class Surface : Floor
    {
        [SerializeField]
        private GoalOnFloor dropOff;
        public GoalOnFloor DropOff
        {
            get
            {
                return dropOff;
            }
        }
    }
}