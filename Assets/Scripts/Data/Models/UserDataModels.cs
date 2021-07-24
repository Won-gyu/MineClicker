using System;
using System.Collections.Generic;

namespace Mine
{

    [Serializable]
    public class UserData
    {
        public void CheckAlloc()
        {
            if (floorData == null) floorData = new List<FloorData>();
        }

        public void FillEmptyData()
        {
            CheckAlloc();

            for (int i = floorData.Count; i < StaticData.MAX_FLOOR; i++)
            {
                floorData.Add(new FloorData());
            }
        }

        public void PolishData()
        {
            for (int i = 0; i < floorData.Count; i++)
            {
                if (floorData[i].tier < 1)
                    floorData[i].tier = 1;
            }
        }
        public double credit;
        public List<FloorData> floorData;
    }

    public class FloorData
    {
        public int tier;
    }
}