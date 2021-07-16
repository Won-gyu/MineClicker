using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;

namespace Mine
{
    public class StageManager : MonoSingleton<StageManager>
    {
        public const string EVENT_EXEC_STORE_ORE = "Game_ExecStoreOre";
        public const string EVENT_ORE_STORED = "Game_OreStored";

        private int oreStored;
        public int OreStored
        {
            get
            {
                return oreStored;
            }
        }

        private void Awake()
        {
            MessageDispatcher.Subscribe(EVENT_EXEC_STORE_ORE, OnStoreOre);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            MessageDispatcher.UnSubscribe(EVENT_EXEC_STORE_ORE, OnStoreOre);
        }

        private void OnStoreOre(EventData eventData)
        {
            oreStored++;
            MessageDispatcher.Dispatch(EVENT_ORE_STORED);
        }
    }
}
