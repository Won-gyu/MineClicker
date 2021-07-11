using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public class Surface : GameObject2D
    {
        [SerializeField]
        private Transform dropOff;
        public Vector2 PositionDropOff
        {
            get
            {
                return dropOff.position;
            }
        }
    }
}