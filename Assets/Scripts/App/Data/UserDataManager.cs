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
    }
}