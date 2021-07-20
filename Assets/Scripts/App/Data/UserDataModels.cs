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
        public int oreStored;
        public List<FloorData> floorData;
    }

    public class FloorData
    {
        public int accommodationLevel;
    }
}