using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;
using System;

namespace Mine
{
    public class SpaceManager : MonoSingleton<SpaceManager>
    {
        [SerializeField]
        private  Space[] totalSpaces;
        public Floor[] totalFloors;
        public Surface surface;
        [SerializeField]
        private PathFinder pathFinder;

        private void Awake()
        {
            totalSpaces = GetComponentsInChildren<Space>();
            totalFloors = GetComponentsInChildren<Floor>();
            Array.Sort(totalFloors, (a, b) =>
            {
                return a.FloorLevel - b.FloorLevel;
            });
            surface = GetComponentInChildren<Surface>();

            pathFinder.Init(totalSpaces);
        }

        public List<int> GetPathClone(int fromId, int toId)
        {
            return new List<int>(pathFinder.totalPathes[fromId][toId]);
        }

        public Space GetSpace(int spaceId)
        {
            return totalSpaces[spaceId];
        }

        public int GetSpaceIdFromFloorLevel(int floorLevel)
        {
            return totalFloors[floorLevel].GetComponent<Space>().id;
        }
    }
}