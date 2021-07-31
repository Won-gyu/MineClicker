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

    [Serializable]
    public class PileSet
    {
        public int oreId;
        public int orePileCount;
        public PooledGameObject pile;
        public OrePileSize PileSize
        {
            get
            {
                return OreManager.Instance.GetOrePileSize(orePileCount);
            }
        }
    }

    public class DropOff : MonoBehaviour
    {
        public const int MAX_VISIBLE_PILES = 4;

        [SerializeField]
        private GameObject pileArea;
        [SerializeField]
        private float widthPileRandomPos;
        [SerializeField]
        private WidePlace goal;


        public List<PileSet> pileSets;
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
            for (int i = 0; i < MAX_VISIBLE_PILES; i++)
            {
                prevPileSets[i].oreId = -1;
                prevPileSets[i].orePileCount = 0;
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
                if (prevPileSets[i].oreId != pileSets[i].oreId || prevPileSets[i].PileSize != pileSets[i].PileSize ||
                    (pileSets[i].orePileCount > 0 && pileSets[i].pile == null))
                {
                    changeVisuals = true;
                    break;
                }
            }

            if (changeVisuals)
            {
                for (int i = 0; i < pileSets.Count; i++)
                {
                    if (i < MAX_VISIBLE_PILES)
                    {
                        if (pileSets[i].orePileCount > 0)
                        {
                            CreateOrPile(pileSets[i]);
                        }
                    }
                    else if (pileSets[i].pile != null)
                    {
                        pileSets[i].pile.ReturnToPool();
                    }
                }
            
                for (int i = 0; i < MAX_VISIBLE_PILES; i++)
                {
                    prevPileSets[i].oreId = pileSets[i].oreId;
                    prevPileSets[i].orePileCount = pileSets[i].orePileCount;
                }
            }
        }

        private void CreateOrPile(PileSet pileSet)
        {
            if (pileSet.PileSize == OrePileSize.Small && pileSet.pile != null)
                return;

            PooledGameObject pile = OreManager.Instance.CreateOrePile(pileSet.oreId, pileSet.PileSize);
            pile.transform.SetParent(pileArea.transform, false);
            if (pileSet.PileSize == OrePileSize.Small)
            {
                pile.transform.localPosition = new Vector3(GetRandomPileAddPosition().x, 0f);
            }
            else
            {
                pile.transform.localPosition = pileSet.pile.transform.localPosition;
                pileSet.pile.ReturnToPool();
            }

            pileSet.pile = pile;
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

        public Vector2 GetRandomPileAddPosition()
        {
            return new Vector2(-widthPileRandomPos * 0.5f + UnityEngine.Random.Range(0, widthPileRandomPos), 0f);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Vector2 left = transform.position + new Vector3(-widthPileRandomPos * 0.5f, 0.2f);
            Vector2 right = transform.position + new Vector3(widthPileRandomPos * 0.5f, 0.2f);
            Gizmos.DrawLine(left, right);
        }
    }
}