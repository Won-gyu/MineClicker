using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public class Space : MonoBehaviour
    {
        public int id;
        public List<Space> spacesLeft;
        public List<Space> spacesRight;
        public List<Space> spacesTop;
        public List<Space> spacesBottom;
    }
}