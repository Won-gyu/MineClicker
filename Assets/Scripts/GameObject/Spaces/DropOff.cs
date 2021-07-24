using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Helper;
using System;

namespace Mine
{
    public enum OrePileSize
    {
        VeryLarge,
        Large,
        Med,
        Small
    }

    public class PileSet
    {
        public int oreId;
        public int orePileCount;
        public PooledGameObject pile;
    }

    public class DropOff : MonoBehaviour
    {
        public const int MAX_VISIBLE_PILES = 4;

        [SerializeField]
        private GameObject pileArea;

        private List<PileSet> pileSets;
        private List<PileSet> prevPileSets; // for calculation

        private void Awake()
        {
            InitFeilds();
            
            MessageDispatcher.Subscribe(OreManager.EVENT_ORE_PILE_STORED, OnOrePileStored);
            MessageDispatcher.Subscribe(Trader.EVENT_EXEC_PICK_ORE_PILE, OnPickOrePile);
        }

        private void OnDestroy()
        {
            MessageDispatcher.UnSubscribe(OreManager.EVENT_ORE_PILE_STORED, OnOrePileStored);
            MessageDispatcher.UnSubscribe(Trader.EVENT_EXEC_PICK_ORE_PILE, OnPickOrePile);
        }

        private void InitFeilds()
        {
            prevPileSets = new List<PileSet>();
            for (int i = 0; i < MAX_VISIBLE_PILES; i++)
                prevPileSets.Add(new PileSet());
            RefreshPileSets();
        }

        private void RefreshPileSets()
        {
            pileSets = new List<PileSet>();
            for (int i = 0; i < OreManager.Instance.TotalOreCount; i++)
            {
                pileSets.Add(new PileSet
                {
                    oreId = i,
                    orePileCount = 0,
                });
            }
        }

        [Button]
        private void SortPileSets()
        {
            pileSets.Sort((a, b) =>
            {
                return b.orePileCount - a.orePileCount;
            });

            bool changeVisuals = false;
            for (int i = 0; i < MAX_VISIBLE_PILES; i++)
            {
                if (prevPileSets[i].oreId != pileSets[i].oreId ||
                    OreManager.Instance.GetOrePileSize(prevPileSets[i].orePileCount) != OreManager.Instance.GetOrePileSize(pileSets[i].orePileCount))
                {
                    Debug.Log("@@@ TEST " + (OreManager.Instance.GetOrePileSize(prevPileSets[i].orePileCount) + " vs " + OreManager.Instance.GetOrePileSize(pileSets[i].orePileCount)));
                    changeVisuals = true;
                    break;
                }
            }

            if (changeVisuals)
            {
                for (int i = 0; i < pileSets.Count; i++)
                {
                    if (pileSets[i].pile != null)
                        pileSets[i].pile.ReturnToPool();

                    if (i < MAX_VISIBLE_PILES)
                    {
                        if (pileSets[i].orePileCount > 0)
                        {
                            pileSets[i].pile = OreManager.Instance.CreateOrePile(pileSets[i].oreId, OreManager.Instance.GetOrePileSize(pileSets[i].orePileCount));
                            pileSets[i].pile.transform.SetParent(pileArea.transform, false);
                            pileSets[i].pile.transform.localPosition = Vector3.zero;
                        }
                    }
                }
            }
            
            for (int i = 0; i < MAX_VISIBLE_PILES; i++)
            {
                prevPileSets[i].oreId = pileSets[i].oreId;
                prevPileSets[i].orePileCount = pileSets[i].orePileCount;
            }
        }

        private PileSet GetPileSet(int oreId)
        {
            // todo: refactoring
            for (int i = 0; i < pileSets.Count; i++)
            {
                if (pileSets[i].oreId == oreId)
                {
                    return pileSets[i];
                }
            }
            return null;
        }

        public void DropCarryOre(CarryOre carryOre)
        {
            GetPileSet(carryOre.OreId).orePileCount++;
            SortPileSets();
        }

        private void OnOrePileStored(EventData eventData)
        {
            DropCarryOre((CarryOre)eventData.value);
        }

        private void OnPickOrePile(EventData eventData)
        {
            Action<List<PileSet>> onPick = (Action<List<PileSet>>)eventData.value;
            onPick(pileSets);
            RefreshPileSets();
        }
    }
}