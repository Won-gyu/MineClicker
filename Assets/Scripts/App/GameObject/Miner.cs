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
        public Elevator elevator;
        public Mineral mineral;
        public MinerState state;
        public Vector2 direction;
        public float speed;
        public float delayDig;

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
            mineral = floor.GetRandomMineral();
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
            yield return StartCoroutine(CoroutineWalkTo(floor.GoalElevator));
            yield return StartCoroutine(CoroutineWaitForElevator());
            ChangeState(MinerState.FindMineral);
        }


        // Action
        private IEnumerator CoroutineWalkTo(GoalOnFloor goal)
        {
            this.goal = goal;
            positionGoal = goal.GetRandomPositionGoal();
            direction = GetDirection(positionGoal);
            while (GetDistanceX(positionGoal).magnitude > 0.1f)
            {
                Position += Speed;
                yield return null;
            }
        }

        private IEnumerator CoroutineWaitForElevator()
        {
            while (true)
            {

            }
        }
    }
}