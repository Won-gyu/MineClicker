using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;
using Sirenix.OdinInspector;

namespace Mine
{
    public class SpaceManager : MonoSingleton<SpaceManager>
    {
        public Space[] spaces;
        public int[][] costMap;

        // all costs are 1
        public const int COST = 1;
        public const int MAX_COST = 10000000;

        private int SpaceCount
        {
            get
            {
                return spaces.Length;
            }
        }

        private void Awake()
        {
            spaces = GetComponentsInChildren<Space>();
            costMap = new int[SpaceCount][];
            for (int id = 0; id < SpaceCount; id++)
            {
                costMap[id] = new int[SpaceCount];
                spaces[id].id = id;
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
            List<int>[] pathes = FindPathesFromOneSpace(1);
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

        private List<int>[] FindPathesFromOneSpace(int id)
        {
            bool[] visited = new bool[SpaceCount];
            List<int>[] pathes = new List<int>[SpaceCount];
            for (int i = 0; i < SpaceCount; i++)
                pathes[i] = new List<int>();

            visited[id] = true;
            int[] costs = new int[SpaceCount];
            for (int i = 0; i < SpaceCount; i++)
                costs[i] = costMap[id][i];

            for (int i = 0; i < SpaceCount - 1; i++)
            {
                int smallestId = GetSmallestId(costs, visited);
                Debug.Log("@@@1: " + smallestId);
                if (smallestId == -1) break;

                visited[smallestId] = true;
                pathes[smallestId].Add(smallestId);
                for (int neighbor = 0; neighbor < SpaceCount; neighbor++)
                {
                    if (!visited[neighbor] && costs[neighbor] > costMap[smallestId][neighbor] + costs[smallestId])
                    {
                        costs[neighbor] = costMap[smallestId][neighbor] + costs[smallestId];
                        pathes[neighbor] = new List<int>(pathes[smallestId]);
                        Debug.Log("@@@2: smallestId:" + smallestId + " neighbor:" + neighbor);
                        Debug.Log("@@@3: " + costs[neighbor] + " vs " +  costMap[smallestId][neighbor] + " " + costs[smallestId]);
                    }
                }
            }

            return pathes;
        }

        private int GetSmallestId(int[] costs, bool[] visited)
        {
            int min = MAX_COST;
            int minId = -1;
            for (int i = 0; i < costs.Length; i++)
            {
                if (!visited[i] && costs[i] < min)
                {
                    min = costs[i];
                    minId = i;
                }
            }
            return minId;
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
                        costMap[fromId][toId] = MAX_COST;
                    }
                }

                // check bound spaces
                if (spaces[fromId].spaceLeft != null)
                {
                    int toId = spaces[fromId].spaceLeft.id;
                    costMap[fromId][toId] = COST;
                }
                if (spaces[fromId].spaceRight != null)
                {
                    int toId = spaces[fromId].spaceRight.id;
                    costMap[fromId][toId] = COST;
                }
                if (spaces[fromId].spaceTop != null)
                {
                    int toId = spaces[fromId].spaceTop.id;
                    costMap[fromId][toId] = COST;
                }
                if (spaces[fromId].spaceBottom != null)
                {
                    int toId = spaces[fromId].spaceBottom.id;
                    costMap[fromId][toId] = COST;
                }
            }
        }
    }
}