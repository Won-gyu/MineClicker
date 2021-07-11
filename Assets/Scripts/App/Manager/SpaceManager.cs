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


        }

        [Button]
        private void Test()
        {
            FindPathesFromOneSpace(0);
        }

        private void FindPathesFromOneSpace(int id)
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
                int smallestId = GetSmallestId(costMap[id], visited);
                if (smallestId == -1) break;

                visited[smallestId] = true;
                for (int neighbor = 0; neighbor < SpaceCount; neighbor++)
                {
                    if (!visited[neighbor] && costs[neighbor] > costMap[smallestId][neighbor] + costs[smallestId])
                    {
                        costs[neighbor] = costMap[smallestId][neighbor] + costs[smallestId];

                        pathes[smallestId].Add(smallestId);
                        if (smallestId != neighbor)
                        {
                            pathes[smallestId].Add(neighbor);
                        }
                    }
                }
            }
        }

        private int GetSmallestId(int[] costs, bool[] visited)
        {
            int min = int.MaxValue;
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
                        costMap[fromId][toId] = int.MaxValue;
                    }
                }

                // check bound spaces
                if (spaces[fromId].spaceLeft != null)
                {
                    int toId = spaces[fromId].spaceLeft.id;
                    costMap[fromId][toId] = COST;
                }
                else if (spaces[fromId].spaceRight != null)
                {
                    int toId = spaces[fromId].spaceRight.id;
                    costMap[fromId][toId] = COST;
                }
                else if (spaces[fromId].spaceTop != null)
                {
                    int toId = spaces[fromId].spaceTop.id;
                    costMap[fromId][toId] = COST;
                }
                else if (spaces[fromId].spaceBottom != null)
                {
                    int toId = spaces[fromId].spaceBottom.id;
                    costMap[fromId][toId] = COST;
                }
            }
        }
    }
}