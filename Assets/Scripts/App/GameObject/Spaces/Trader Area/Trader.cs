using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public class Trader : MovableObject
    {
        private TraderArea traderArea;

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
                yield return StartCoroutine(CoroutineWalkToX(traderArea.GoalOnFloor));
                yield return new WaitForSeconds(5f);
            }
        }
    }
}