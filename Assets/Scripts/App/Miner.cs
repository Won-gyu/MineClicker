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
        public Vector2 speed;

        private Coroutine coroutineState;

        private void Awake()
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
            speed = GetDistanceX(mineral.Position).normalized * Time.deltaTime;
            while (GetDistanceX(mineral.Position).magnitude > mineral.WidthDigable)
            {
                Debug.Log(Vector2.Distance(mineral.Position, Position));
                Position += speed;
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
            speed = GetDistanceX(floor.Position).normalized;
            while (GetDistanceX(floor.PositionGoal).magnitude > 1f)
            {
                Position += speed;
                yield return null;
            }
            ChangeState(MinerState.FindMineral);
        }
    }
}