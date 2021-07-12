using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Mine
{
    public class Elevator : GameObject2D
    {
        [SerializeField]
        private Transform minerArea;

        public Vector2 direction;
        public float speed;
        private Vector2 Speed
        {
            get
            {
                return direction * speed  * Time.deltaTime;
            }
        }

        public int currentFloor = -1;
        // [floorLevelGoal]
        public Dictionary<int, List<Miner>> minersOn;

        private ElevatorArea elevatorArea;
        public int debug;

        private void Awake()
        {
            minersOn = new Dictionary<int, List<Miner>>();
        }

        public void Init(ElevatorArea elevatorArea, int floor)
        {
            this.elevatorArea = elevatorArea;
            currentFloor = floor;
            Position = elevatorArea.GetEntrance(floor).Position;
            for (int i = 0; i < elevatorArea.EntranceFloorLevels.Count; i++)
            {
                minersOn.Add(elevatorArea.EntranceFloorLevels[i], new List<Miner>());
            }
        }
        
        // Action
        private IEnumerator CoroutineMoveTo(int floorLevel)
        {
            GameObject2D goal = elevatorArea.GetEntrance(floorLevel);
            direction = GetDirectionY(goal.Position);
            while (GetDistanceY(goal.Position) > 0.1f)
            {
                Position += Speed;
                yield return null;
            }
            ArriveOnFloor(floorLevel);
        }

        public void ArriveOnFloor(int floor)
        {
            for (int i = 0; i < minersOn[floor].Count; i++)
            {
                GetOff(minersOn[floor][i]);
                Debug.Log("Miner get off the elevator on " + floor);
            }
            minersOn[floor].Clear();

            for (int i = 0; i < elevatorArea.GetWaitInfos(floor).Count; i++)
            {
                GetOn(elevatorArea.GetWaitInfos(floor)[i]);
                Debug.Log("Miner get on the elevator on " + floor);
            }
            elevatorArea.GetWaitInfos(floor).Clear();
        }

        private void GetOn(ElevatorWaitInfo waitInfo)
        {
            List<Miner> miners;
            if (!minersOn.TryGetValue(waitInfo.goal, out miners))
            {
                Debug.LogError("something went wrong");
            }
            miners.Add(waitInfo.minerWaiting);
            waitInfo.minerWaiting.transform.parent = minerArea;
        }

        public void GetOff(Miner miner)
        {
            miner.OnArriveAtGoalFloor();
            miner.transform.parent = MinerManager.Instance.transform;
        }

        [Button]
        public void Test()
        {
            StartCoroutine(CoroutineMoveTo(debug));
        }
    }
}