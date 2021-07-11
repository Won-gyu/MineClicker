using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Mine
{
    public static class DijkstraUtils
    {
        // all costs are 1
        public const int COST = 1;
        public const int MAX_COST = 10000000;

        public static List<int>[] FindPathesFromOneSpace(int[][] costMap, int id)
        {
            int spaceCount = costMap.Length;
            bool[] visited = new bool[spaceCount];
            List<int>[] pathes = new List<int>[spaceCount];
            for (int i = 0; i < spaceCount; i++)
                pathes[i] = new List<int>();

            visited[id] = true;
            int[] costs = new int[spaceCount];
            for (int i = 0; i < spaceCount; i++)
                costs[i] = costMap[id][i];

            for (int i = 0; i < spaceCount - 1; i++)
            {
                int smallestId = GetSmallestId(costs, visited);
                if (smallestId == -1) break;

                visited[smallestId] = true;
                pathes[smallestId].Add(smallestId);
                for (int neighbor = 0; neighbor < spaceCount; neighbor++)
                {
                    if (!visited[neighbor] && costs[neighbor] > costMap[smallestId][neighbor] + costs[smallestId])
                    {
                        costs[neighbor] = costMap[smallestId][neighbor] + costs[smallestId];
                        pathes[neighbor] = new List<int>(pathes[smallestId]);
                    }
                }
            }

            return pathes;
        }

        private static int GetSmallestId(int[] costs, bool[] visited)
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
    }
}
