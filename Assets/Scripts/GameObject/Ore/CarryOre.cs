using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarryOre
{
    public class CarryOre : MonoBehaviour
    {
        private int oreId;

        public void Init(int oreId)
        {
            this.oreId = oreId;
        }
    }
}
