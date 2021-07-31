using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;

namespace Mine
{
    public class OreHolder : MonoBehaviour
    {
        private PooledGameObject objCarryOre;
        private CarryOre carryOre;

        public void ReturnOre()
        {
            if (objCarryOre != null)
            {
                MessageDispatcher.Dispatch<CarryOre>(OreManager.EVENT_EXEC_STORE_ORE_PILE, carryOre);
                objCarryOre.ReturnToPool();
                objCarryOre = null;
                carryOre = null;
            }
        }

        public void HoldOre(Ore ore)
        {
            if (objCarryOre == null)
            {
                objCarryOre = CarryOrePoolGroups.Instance.GetCarryOreObject(ore.OreId);
                objCarryOre.transform.SetParent(transform, false);
                carryOre = objCarryOre.GetComponent<CarryOre>();
                carryOre.Init(ore.OreId);
            }
        }
    }
}