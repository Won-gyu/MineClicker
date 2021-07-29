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
        private WidePlace goalDropOff;
        public WidePlace GoalDropOff
        {
            get
            {
                if (goalDropOff == null)
                {
                    goalDropOff = dropOff.GetComponent<WidePlace>();
                }
                return goalDropOff;
            }
        }
    }
}