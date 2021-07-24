using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;

namespace Mine
{
    public class UserDataManager : MonoSingleton<UserDataManager>
    {
        private UserData userData;
        public UserData UserData
        {
            get
            {
                if (userData == null)
                {
                    userData = new UserData();
                    userData.FillEmptyData();
                    userData.PolishData();
                }
                return userData;
            }
        }

        private void Awake()
        {
            MessageDispatcher.Subscribe(OreManager.EVENT_EXEC_STORE_ORE, OnStoreOre);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            MessageDispatcher.UnSubscribe(OreManager.EVENT_EXEC_STORE_ORE, OnStoreOre);
        }

        private void OnStoreOre(EventData eventData)
        {
            UserData.oreStored++;
            MessageDispatcher.Dispatch(OreManager.EVENT_ORE_STORED);
        }
        
        public int OreStored
        {
            get
            {
                return userData.oreStored;
            }
        }
    }
}