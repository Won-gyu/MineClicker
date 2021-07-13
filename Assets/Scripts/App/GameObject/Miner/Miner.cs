using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public enum MinerActionState
    {
        FindMineral,
        Dig,
        Deliver
    }

    public enum MinerBodyState
    {
        Stand,
        Walk,
        Hit,
        Carry
    }

    public class Miner : MovableObject
    {
        public const string ANI_PARAM_STATE = "State";

        [SerializeField]
        private Animator animator;
        [SerializeField]
        private MinerBody body;
        public Floor currentFloor;
        public Basement basementWorkPlace;
        public ElevatorArea elevator;
        public Mineral mineral;
        public MinerActionState actionState;
        public MinerBodyState bodyState;
        public float delayDig;

        [SerializeField]
        private PathController pathController;

        public GoalOnFloor goal;
        public Vector2 positionGoal;
        private Coroutine coroutineActionState;
        private bool carry;

        private void Start()
        {
            body.SetRandomCostume();
            ChangeActionState(MinerActionState.FindMineral);
        }

        public void Init(Floor floorWorkPlace)
        {
            this.currentFloor = floorWorkPlace;
            this.basementWorkPlace = floorWorkPlace as Basement;
            ChangeBodyState(MinerBodyState.Stand);
        }

        public void ChangeBodyState(MinerBodyState bodyState)
        {
            this.bodyState = bodyState;
            animator.SetInteger(ANI_PARAM_STATE, (int)bodyState);
        }

        private void ChangeActionState(MinerActionState actionState)
        {
            this.actionState = actionState;
            if (coroutineActionState != null)
            {
                StopCoroutine(coroutineActionState);
                coroutineActionState = null;
            }
            switch (actionState)
            {
                case MinerActionState.FindMineral:
                    coroutineActionState = StartCoroutine(StateCoroutineFindingMineral());
                    break;
                case MinerActionState.Dig:
                    coroutineActionState = StartCoroutine(StateCoroutineDig());
                    break;
                case MinerActionState.Deliver:
                    coroutineActionState = StartCoroutine(StateCoroutineDeliver());
                    break;
            }
        }

        // State
        private IEnumerator StateCoroutineFindingMineral()
        {
            mineral = basementWorkPlace.GetRandomMineral();
            yield return StartCoroutine(CoroutineWalkTo(mineral.GoalOnFloor));
            ChangeActionState(MinerActionState.Dig);
        }

        private IEnumerator StateCoroutineDig()
        {
            ChangeBodyState(MinerBodyState.Hit);
            yield return new WaitForSecondsRealtime(delayDig);
            ChangeActionState(MinerActionState.Deliver);
        }

        private IEnumerator StateCoroutineDeliver()
        {
            StartCarry();
            yield return StartCoroutine(CoroutineWalkTo(basementWorkPlace.GoalElevator));
            yield return StartCoroutine(CoroutineMoveToFloor(SpaceManager.Instance.Surface.FloorLevel));
            yield return StartCoroutine(CoroutineWalkTo(SpaceManager.Instance.Surface.DropOff));
            EndCarry();

            ChangeBodyState(MinerBodyState.Stand);
            yield return StartCoroutine(CoroutineWalkTo(SpaceManager.Instance.Surface.GoalElevator));
            yield return StartCoroutine(CoroutineMoveToFloor(basementWorkPlace.FloorLevel));

            ChangeActionState(MinerActionState.FindMineral);
        }

        private void StartCarry()
        {
            carry = true;
        }

        private void EndCarry()
        {
            carry = false;
        }


        // Action
        private IEnumerator CoroutineWalkTo(GoalOnFloor goal)
        {
            ChangeBodyState(carry ? MinerBodyState.Carry : MinerBodyState.Walk);
            this.goal = goal;
            positionGoal = goal.GetRandomPositionGoal();
            Direction = GetDirectionX(positionGoal);
            while (GetDistanceX(positionGoal) > Speed.magnitude)
            {
                Position += Speed;
                yield return null;
            }
            ChangeBodyState(MinerBodyState.Stand);
        }

        private IEnumerator CoroutineWaitForElevator(int spaceIdElevator, int floorLevelWaiting, int floorLevelGoal)
        {
            ChangeBodyState(carry ? MinerBodyState.Carry : MinerBodyState.Stand);
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