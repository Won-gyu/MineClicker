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

        private UserData userData;
        public int OreStored
        {
            get
            {
                return userData.oreStored;
            }
        }

        private void Awake()
        {
            userData = new UserData();
            MessageDispatcher.Subscribe(EVENT_EXEC_STORE_ORE, OnStoreOre);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            MessageDispatcher.UnSubscribe(EVENT_EXEC_STORE_ORE, OnStoreOre);
        }

        private void OnStoreOre(EventData eventData)
        {
            userData.oreStored++;
            MessageDispatcher.Dispatch(EVENT_ORE_STORED);
        }
    }
}
