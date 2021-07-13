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

    public class Miner : MovableObject
    {
        public Floor currentFloor;
        public Basement basementWorkPlace;
        public ElevatorArea elevator;
        public Mineral mineral;
        public MinerState state;
        public float delayDig;
        public Sprite sprite;

        [SerializeField]
        private PathController pathController;

        public GoalOnFloor goal;
        public Vector2 positionGoal;
        private Coroutine coroutineState;

        private void Start()
        {
            ChangeState(MinerState.FindMineral);
        }

        public void Init(Floor floorWorkPlace)
        {
            this.currentFloor = floorWorkPlace;
            this.basementWorkPlace = floorWorkPlace as Basement;
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
            mineral = basementWorkPlace.GetRandomMineral();
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
            yield return StartCoroutine(CoroutineWalkTo(basementWorkPlace.GoalElevator));
            yield return StartCoroutine(CoroutineMoveToFloor(SpaceManager.Instance.Surface.FloorLevel));
            yield return StartCoroutine(CoroutineWalkTo(SpaceManager.Instance.Surface.DropOff));
            yield return StartCoroutine(CoroutineWalkTo(SpaceManager.Instance.Surface.GoalElevator));
            yield return StartCoroutine(CoroutineMoveToFloor(basementWorkPlace.FloorLevel));

            ChangeState(MinerState.FindMineral);
        }


        // Action
        private IEnumerator CoroutineWalkTo(GoalOnFloor goal)
        {
            this.goal = goal;
            positionGoal = goal.GetRandomPositionGoal();
            Direction = GetDirectionX(positionGoal);
            while (GetDistanceX(positionGoal) > Speed.magnitude)
            {
                Position += Speed;
                yield return null;
            }
        }

        private IEnumerator CoroutineWaitForElevator(int spaceIdElevator, int floorLevelWaiting, int floorLevelGoal)
        {
            elevator = SpaceManager.Instance.GetSpace(spaceIdElevator).GetComponent<ElevatorArea>();
            elevator.AddMinerWaiting(this, floorLevelWaiting, floorLevelGoal);
            while (elevator != null)
            {
                yield return null;
            }
            // finish coroutine when arriving on goal floor
        }

        public void OnArriveAtGoalFloor()
        {
            elevator = null;
        }

        private IEnumerator CoroutineMoveToFloor(int floorLevel)
        {
            pathController.SetPathes(SpaceManager.Instance.GetPathClone(
                SpaceManager.Instance.GetSpaceIdFromFloorLevel(currentFloor.FloorLevel),
                SpaceManager.Instance.GetSpaceIdFromFloorLevel(floorLevel)));
            yield return StartCoroutine(CoroutineWalkTo(currentFloor.GoalElevator));

            int spaceIdElevator = pathController.PopPath();
            int spaceIdGoal = pathController.PopPath();
            int floorLevelGoal = SpaceManager.Instance.GetSpace(spaceIdGoal).GetComponent<Floor>().FloorLevel;
            yield return StartCoroutine(CoroutineWaitForElevator(spaceIdElevator, currentFloor.FloorLevel, floorLevelGoal));

            currentFloor = SpaceManager.Instance.GetFloor(floorLevelGoal);
        }
    }
}