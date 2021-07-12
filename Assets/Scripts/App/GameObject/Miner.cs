using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public enum MinerState
    {
        FindMineral,
        Dig,
        Deliver
    }

    public class Miner : GameObject2D
    {
        public Floor floor;
        public Basement basement;
        public ElevatorArea elevator;
        public Mineral mineral;
        public MinerState state;
        public Vector2 direction;
        public float speed;
        public float delayDig;

        [SerializeField]
        private PathController pathController;

        private Vector2 Speed
        {
            get
            {
                return direction * speed  * Time.deltaTime;
            }
        }

        public GoalOnFloor goal;
        public Vector2 positionGoal;
        private Coroutine coroutineState;

        private void Start()
        {
            ChangeState(MinerState.FindMineral);
        }

        public void Init(Floor floor)
        {
            this.floor = floor;
            this.basement = floor as Basement;
        }

        private void ChangeState(MinerState state)
        {
            this.state = state;
            if (coroutineState != null)
            {
                StopCoroutine(coroutineState);
                coroutineState = null;
            }
            switch (state)
            {
                case MinerState.FindMineral:
                    coroutineState = StartCoroutine(StateCoroutineFindingMineral());
                    break;
                case MinerState.Dig:
                    coroutineState = StartCoroutine(StateCoroutineDig());
                    break;
                case MinerState.Deliver:
                    coroutineState = StartCoroutine(StateCoroutineDeliver());
                    break;
            }
        }

        // State
        private IEnumerator StateCoroutineFindingMineral()
        {
            mineral = basement.GetRandomMineral();
            yield return StartCoroutine(CoroutineWalkTo(mineral.GoalOnFloor));
            ChangeState(MinerState.Dig);
        }

        private IEnumerator StateCoroutineDig()
        {
            yield return new WaitForSecondsRealtime(delayDig);
            ChangeState(MinerState.Deliver);
        }

        private IEnumerator StateCoroutineDeliver()
        {
            yield return StartCoroutine(CoroutineWalkTo(basement.GoalElevator));
            yield return StartCoroutine(CoroutineMoveToFloor(0));
            ChangeState(MinerState.FindMineral);
        }


        // Action
        private IEnumerator CoroutineWalkTo(GoalOnFloor goal)
        {
            this.goal = goal;
            positionGoal = goal.GetRandomPositionGoal();
            direction = GetDirectionX(positionGoal);
            while (GetDistanceX(positionGoal) > 0.1f)
            {
                Position += Speed;
                yield return null;
            }
        }

        private IEnumerator CoroutineWaitForElevator(int spaceIdElevator, int floorLevel)
        {
            elevator = SpaceManager.Instance.GetSpace(spaceIdElevator).GetComponent<ElevatorArea>();
            elevator.AddMinerWaiting(this, floorLevel);
            while (elevator != null)
            {
                yield return null;
            }
        }

        public void OnArriveAtGoalFloor()
        {
            elevator = null;
        }

        private IEnumerator CoroutineMoveToFloor(int floorLevel)
        {
            pathController.SetPathes(SpaceManager.Instance.GetPathClone(
                SpaceManager.Instance.GetSpaceIdFromFloorLevel(floor.FloorLevel),
                SpaceManager.Instance.GetSpaceIdFromFloorLevel(floorLevel)));
            yield return StartCoroutine(CoroutineWalkTo(floor.GoalElevator));

            int spaceIdElevator = pathController.PopPath();
            int spaceIdGoal = pathController.PopPath();
            yield return StartCoroutine(CoroutineWaitForElevator(spaceIdElevator, spaceIdGoal));
        }
    }
}