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
        public Mineral mineral;
        public MinerState state;
        public Vector2 direction;
        public float speed;

        private Vector2 Speed
        {
            get
            {
                return direction * speed;
            }
        }

        private Vector2 positionGoal;
        private Coroutine coroutineState;

        private void Start()
        {
            ChangeState(MinerState.FindMineral);
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
                    coroutineState = StartCoroutine(CoroutineFindingMineral());
                    break;
                case MinerState.Dig:
                    coroutineState = StartCoroutine(CoroutineDig());
                    break;
                case MinerState.Deliver:
                    coroutineState = StartCoroutine(CoroutineDeliver());
                    break;
            }
        }

        private IEnumerator CoroutineFindingMineral()
        {
            mineral = floor.GetRandomMineral();
            direction = GetDirection(mineral) * Time.fixedDeltaTime;
            while (GetDistanceX(mineral.Position).magnitude > mineral.WidthDigable)
            {
                Position += Speed;
                yield return null;
            }
            ChangeState(MinerState.Dig);
        }

        private IEnumerator CoroutineDig()
        {
            yield return new WaitForSecondsRealtime(5f);
            ChangeState(MinerState.Deliver);
        }

        private IEnumerator CoroutineDeliver()
        {
            direction = GetDirection(floor.PositionGoal) * Time.fixedDeltaTime;
            while (GetDistanceX(floor.PositionGoal).magnitude > floor.WidthDigable)
            {
                Position += Speed;
                yield return null;
            }
            ChangeState(MinerState.FindMineral);
        }
    }
}