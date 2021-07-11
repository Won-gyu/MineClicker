using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;
using Sirenix.OdinInspector;

namespace Mine
{
    public class SpaceManager : MonoSingleton<SpaceManager>
    {
        public Space[] totalSpaces;
        public int[][] costMap;

        private int SpaceCount
        {
            get
            {
                return totalSpaces.Length;
            }
        }

        private void Awake()
        {
            totalSpaces = GetComponentsInChildren<Space>();
            costMap = new int[SpaceCount][];
            for (int id = 0; id < SpaceCount; id++)
            {
                costMap[id] = new int[SpaceCount];
                totalSpaces[id].id = id;
            }

            FindPathesAll();
        }

        private void FindPathesAll()
        {
            // init
            InitCosts();

            string stringCostMap = "cost map\n";
            for (int i = 0; i < costMap.Length; i++)
            {
                for (int j = 0; j < costMap[i].Length; j++)
                {
                    stringCostMap += costMap[i][j] + " - ";
                }
                stringCostMap += "\n";
            }
            Debug.Log(stringCostMap);
        }

        [Button]
        private void Test()
        {
            // List<int>[] pathes = FindPathesFromOneSpace(0);
            // for (int i = 0; i < pathes.Length; i++)
            // {
            //     string p = "path";
            //     for (int j = 0; j < pathes[i].Count; j++)
            //     {
            //         p += "-" + pathes[i][j];
            //     }
            //     Debug.Log(p);
            // }
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
            // pathes = FindPathesFromOneSpace(2);
            // for (int i = 0; i < pathes.Length; i++)
            // {
            //     string p = "path";
            //     for (int j = 0; j < pathes[i].Count; j++)
            //     {
            //         p += "-" + pathes[i][j];
            //     }
            //     Debug.Log(p);
            // }
        }

        private void InitCosts()
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
                InitCosts(fromId, totalSpaces[fromId].spacesLeft);
                InitCosts(fromId, totalSpaces[fromId].spacesRight);
                InitCosts(fromId, totalSpaces[fromId].spacesTop);
                InitCosts(fromId, totalSpaces[fromId].spacesBottom);
            }
        }

        private void InitCosts(int fromId, List<Space> spaces)
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