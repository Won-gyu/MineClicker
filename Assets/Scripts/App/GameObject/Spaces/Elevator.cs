using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Mine
{
    public class Elevator : GameObject2D
    {
        public int currentFloor = -1;
        // [floorLevelWaiting]
        public List<List<Miner>> minersWaiting;
        // [floorLevelGoal]
        public List<List<Miner>> minersOn;

        private void Awake()
        {
            minersWaiting = new List<List<Miner>>();
            minersOn = new List<List<Miner>>();
        }

        public void AddMinerWaiting(Miner miner, int floorLevelWaiting)
        {
            while (minersWaiting.Count < floorLevelWaiting + 1)
            {
                minersWaiting.Add(new List<Miner>());
            }

            minersWaiting[floorLevelWaiting].Add(miner);
        }

        public void AddMinerOn(Miner miner, int floorLevelGoal)
        {
            while (minersOn.Count < floorLevelGoal + 1)
            {
                minersOn.Add(new List<Miner>());
            }

            minersOn[floorLevelGoal].Add(miner);
        }

        public void ArriveOnFloor(int floor)
        {
            for (int i = 0; i < minersOn[floor].Count; i++)
            {
                minersOn[floor][i].OnArriveAtGoalFloor();
            }
            minersOn[floor].Clear();

            for (int i = 0; i < minersWaiting[floor].Count; i++)
            {
                minersOn[floor].Add(minersWaiting[floor][i]);
            }
            minersWaiting[floor].Clear();
        }
    }
}