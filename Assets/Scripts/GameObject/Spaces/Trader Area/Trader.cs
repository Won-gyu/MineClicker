using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;
using System;

namespace Mine
{
    public class Trader : MovableObject
    {
        public const string EVENT_EXEC_PICK_ORE_PILE = "Game_ExecPickOrePile";

        private TraderArea traderArea;
        private List<PileSet> pileSets;

        public void Init(TraderArea traderArea)
        {
            this.traderArea = traderArea;
            flipX = true;
            StartCoroutine(UpdateCoroutine());
        }

        private void PickOrePiles()
        {
            MessageDispatcher.Dispatch<Action<List<PileSet>>>(EVENT_EXEC_PICK_ORE_PILE, OnPickPileSets);
        }

        private IEnumerator UpdateCoroutine()
        {
            while (true)
            {
                Position = traderArea.StartOnFloor.Position;
                yield return StartCoroutine(CoroutineWalkToX(traderArea.DropOff));
                PickOrePiles();
                yield return new WaitForSeconds(0.5f);
                yield return StartCoroutine(CoroutineWalkToX(traderArea.GoalOnFloor));
                ChangeOrePilesToCredit();
                yield return new WaitForSeconds(5f);
            }
        }

        private void OnPickPileSets(List<PileSet> pileSets)
        {
            this.pileSets = pileSets;
            for (int i = 0; i < pileSets.Count; i++)
            {
                if (pileSets[i].orePileCount > 0)
                    pileSets[i].pile.transform.SetParent(transform);
            }

        }

        private void ChangeOrePilesToCredit()
        {
            for (int i = 0; i < pileSets.Count; i++)
            {
                if (pileSets[i].orePileCount > 0)
                {
                    MessageDispatcher.Dispatch<double>(UserDataManager.EVENT_EXEC_ADD_CREDIT, (double)pileSets[i].orePileCount);
                    pileSets[i].pile.ReturnToPool();
                }
            }
            pileSets = null;
        }
    }
}