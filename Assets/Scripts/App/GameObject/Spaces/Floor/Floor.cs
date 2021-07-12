using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Mine
{
    public class Floor : GameObject2D
    {
        [SerializeField]
        private int floorLevel;
        public int FloorLevel
        {
            get
            {
                return floorLevel;
            }
        }
    }
}
