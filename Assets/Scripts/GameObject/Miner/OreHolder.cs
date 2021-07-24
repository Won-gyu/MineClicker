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
                objCarryOre.ReturnToPool();
                objCarryOre = null;
                carryOre = null;
                MessageDispatcher.Dispatch("Game_ExecStoreOre");
            }
        }

        public void HoldOre(Ore ore)
        {
            if (objCarryOre == null)
            {
                objCarryOre = CarryOrePools.Instance.GetPoolObject(ore.GetRandomCarryOreId());
                objCarryOre.transform.SetParent(transform, false);
                carryOre = objCarryOre.GetComponent<CarryOre>();
                carryOre.Init(ore.OreId);
            }
        }
    }
}