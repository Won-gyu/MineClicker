using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public class Trader : MovableObject
    {
        private TraderArea traderArea;

        private void Awake()
        {
            flipX = true;
        }

        public void Init(TraderArea traderArea)
        {
            this.traderArea = traderArea;
            StartCoroutine(UpdateCoroutine());
        }

        private IEnumerator UpdateCoroutine()
        {
            while (true)
            {
                Position = traderArea.StartOnFloor.Position;
                yield return StartCoroutine(CoroutineWalkToX(traderArea.DropOff));
                yield return new WaitForSeconds(0.5f);
                yield return StartCoroutine(CoroutineWalkToX(traderArea.GoalOnFloor));
                yield return new WaitForSeconds(5f);
            }
        }
    }
}