using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Mine
{
    public enum ElevatorState
    {
        Idle,
        Up,
        Down
    }
    public class Elevator : MovableObject
    {
        public const string ANI_PARAM_STATE = "State";

        [SerializeField]
        private Transform minerArea;
        [SerializeField]
        private Animator animator;
        protected override Vector2 Direction
        {
            set
            {
                base.Direction = value;

                if (Direction.y > 0)
                    animator.SetInteger(ANI_PARAM_STATE, (int)ElevatorState.Up);
                else if (Direction.y < 0)
                    animator.SetInteger(ANI_PARAM_STATE, (int)ElevatorState.Down);
                else
                    animator.SetInteger(ANI_PARAM_STATE, (int)ElevatorState.Idle);
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
            StartCoroutine(UpdateCoroutine());
        }

        private IEnumerator UpdateCoroutine()
        {
            while (true)
            {
                for (int i = 0; i < elevatorArea.EntranceFloorLevels.Count; i++)
                {
                    yield return StartCoroutine(CoroutineMoveTo(elevatorArea.EntranceFloorLevels[i]));
                    Direction = Vector2.zero;
                    yield return new WaitForSeconds(0.2f);
                }
                for (int i = elevatorArea.EntranceFloorLevels.Count - 1; i >= 0; i--)
                {
                    yield return StartCoroutine(CoroutineMoveTo(elevatorArea.EntranceFloorLevels[i]));
                    Direction = Vector2.zero;
                    yield return new WaitForSeconds(0.2f);
                }
            }
        }
        
        // Action
        private IEnumerator CoroutineMoveTo(int floorLevelGoal)
        {
            bool isElevatorGoingUp = floorLevelGoal < currentFloor;
            ArriveOnFloor(currentFloor, isElevatorGoingUp);
            GameObject2D goal = elevatorArea.GetEntrance(floorLevelGoal);
            yield return StartCoroutine(CoroutineWalkToY(goal));
            Position = goal.Position;
            ArriveOnFloor(floorLevelGoal, isElevatorGoingUp);
        }

        public void ArriveOnFloor(int floor, bool isElevatorGoingUp)
        {
            currentFloor = floor;
            for (int i = 0; i < minersOn[floor].Count; i++)
            {
                GetOff(minersOn[floor][i], floor);
            }
            minersOn[floor].Clear();

            for (int i = 0; i < elevatorArea.GetWaitInfos(floor).Count;)
            {
                bool isMinerGoingUp = elevatorArea.GetWaitInfos(floor)[i].goal < floor;
                if (isMinerGoingUp == isElevatorGoingUp)
                {
                    GetOn(elevatorArea.GetWaitInfos(floor)[i]);
                    elevatorArea.GetWaitInfos(floor).RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
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

        public void GetOff(Miner miner, int floor)
        {
            miner.OnArriveAtGoalFloor();
            miner.transform.parent = MinerManager.Instance.transform;
            miner.Position = new Vector2(miner.Position.x, elevatorArea.GetEntrance(floor).Position.y);
        }

        [Button]
        public void Test()
        {
            StartCoroutine(CoroutineMoveTo(debug));
        }
    }
}