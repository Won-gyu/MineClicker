using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Mine
{
    public class Floor : GameObject2D
    {
        [SerializeField]
        private int floorLevel;
        public int FloorLevel
        {
            get
            {
                return floorLevel;
            }
        }
        [SerializeField]
        protected GoalOnFloor goalElevator;
        public GoalOnFloor GoalElevator
        {
            get
            {
                return goalElevator;
            }
        }

        public FloorData FloorData
        {
            get
            {
                return UserDataManager.Instance.UserData.floorData[FloorLevel];
            }
        }

        public SpreadSheetFloorTierDataInfo FloorTierData
        {
            get
            {
                return StaticDataManager.Instance.GetFloorTierData(FloorData.tier);
            }
        }
    }
}
