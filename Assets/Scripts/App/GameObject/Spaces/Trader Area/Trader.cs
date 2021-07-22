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
        }

        private IEnumerator UpdateCoroutine()
        {
            while (true)
            {
                // transform.position = traderArea.StartOnFloor.Position;
                // yield return StartCoroutine(CoroutineMoveTo(elevatorArea.EntranceFloorLevels[i]));
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}