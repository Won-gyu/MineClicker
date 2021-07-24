using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public class CarryOre : MonoBehaviour
    {
        private int oreId;
        public int OreId
        {
            get
            {
                return oreId;
            }
        }

        public void Init(int oreId)
        {
            this.oreId = oreId;
        }
    }
}
