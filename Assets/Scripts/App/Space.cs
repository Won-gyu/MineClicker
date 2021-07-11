using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public class Space : GameObject2D
    {
        public int id;
        public Space spaceLeft;
        public Space spaceRight;
        public Space spaceTop;
        public Space spaceBottom;
    }
}