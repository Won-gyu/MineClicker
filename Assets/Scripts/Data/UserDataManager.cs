using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;

namespace Mine
{
    public class UserDataManager : MonoSingleton<UserDataManager>
    {
        public const string EVENT_EXEC_ADD_CREDIT = "Game_ExecAddCredit";
        public const string EVENT_CREDIT_ADDED = "Game_CreditAdded";

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
            MessageDispatcher.Subscribe(OreManager.EVENT_EXEC_STORE_ORE_PILE, OnStoreOrePile);
            MessageDispatcher.Subscribe(EVENT_EXEC_ADD_CREDIT, OnAddCredit);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            MessageDispatcher.UnSubscribe(OreManager.EVENT_EXEC_STORE_ORE_PILE, OnStoreOrePile);
            MessageDispatcher.UnSubscribe(EVENT_EXEC_ADD_CREDIT, OnAddCredit);
        }

        private void OnStoreOrePile(EventData eventData)
        {
            MessageDispatcher.Dispatch<CarryOre>(OreManager.EVENT_ORE_PILE_STORED, (CarryOre)eventData.value);
        }

        private void OnAddCredit(EventData eventData)
        {
            UserData.credit += (double)eventData.value;
            MessageDispatcher.Dispatch(EVENT_CREDIT_ADDED);
        }
    }
}