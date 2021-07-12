using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;
using Sirenix.OdinInspector;
using System;

namespace Mine
{
    public class PathFinder : MonoBehaviour
    {
        public Space[] totalSpaces;
        public Floor[] totalFloors;
        public int[][] costMap;
        public List<int>[][] totalPathes;

        private int SpaceCount
        {
            get
            {
                return totalSpaces.Length;
            }
        }

        private void Awake()
        {
            FindPathesAll();

            totalFloors = GetComponents<Floor>();
            Array.Sort(totalFloors, (a, b) =>
            {
                return a.FloorLevel - b.FloorLevel;
            });
        }

        private void FindPathesAll()
        {
            totalPathes = new List<int>[SpaceCount][];
            totalSpaces = GetComponentsInChildren<Space>();
            costMap = new int[SpaceCount][];
            for (int id = 0; id < SpaceCount; id++)
            {
                totalPathes[id] = new List<int>[SpaceCount];
                costMap[id] = new int[SpaceCount];
                totalSpaces[id].id = id;
            }

            // init
            InitCostMap();

            for (int i = 0; i < SpaceCount; i++)
            {
                totalPathes[i] = DijkstraUtils.FindPathesFromOneSpace(costMap, i);
            }
            // string stringCostMap = "cost map\n";
            // for (int i = 0; i < costMap.Length; i++)
            // {
            //     for (int j = 0; j < costMap[i].Length; j++)
            //     {
            //         stringCostMap += costMap[i][j] + " - ";
            //     }
            //     stringCostMap += "\n";
            // }
            // Debug.Log(stringCostMap);
        }

        [Button]
        private void Test()
        {
            List<int>[] pathes = DijkstraUtils.FindPathesFromOneSpace(costMap, 1);
            for (int i = 0; i < pathes.Length; i++)
            {
                string p = "path";
                for (int j = 0; j < pathes[i].Count; j++)
                {
                    p += "-" + pathes[i][j];
                }
                Debug.Log(p);
            }
        }

        private void InitCostMap()
        {
            for (int fromId = 0; fromId < SpaceCount; fromId++)
            {
                for (int toId = 0; toId < SpaceCount; toId++)
                {
                    if (fromId == toId)
                    {
                        costMap[fromId][toId] = 0;
                    }
                    else
                    {
                        costMap[fromId][toId] = DijkstraUtils.MAX_COST;
                    }
                }

                // check bound spaces
                InitCostMap(fromId, totalSpaces[fromId].spacesLeft);
                InitCostMap(fromId, totalSpaces[fromId].spacesRight);
                InitCostMap(fromId, totalSpaces[fromId].spacesTop);
                InitCostMap(fromId, totalSpaces[fromId].spacesBottom);
            }
        }

        private void InitCostMap(int fromId, List<Space> spaces)
        {
            if (spaces == null)
                return;

            for (int i = 0; i < spaces.Count; i++)
            {
                int toId = spaces[i].id;
                costMap[fromId][toId] = DijkstraUtils.COST;
            }
        }
    }
}