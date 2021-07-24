using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Helper;

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
        private List<int> prevOreIds; // for calculation

        private void Awake()
        {
            InitFeilds();
            
            MessageDispatcher.Subscribe(OreManager.EVENT_ORE_PILE_STORED, OnOrePileStored);
        }

        private void OnDestroy()
        {
            MessageDispatcher.UnSubscribe(OreManager.EVENT_ORE_PILE_STORED, OnOrePileStored);
        }

        private void InitFeilds()
        {
            prevOreIds = new List<int>();
            for (int i = 0; i < MAX_VISIBLE_PILES; i++)
                prevOreIds.Add(-1);

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
            for (int i = 0; i < MAX_VISIBLE_PILES; i++)
                prevOreIds[i] = pileSets[i].oreId;

            pileSets.Sort((a, b) =>
            {
                return a.orePileCount - b.orePileCount;
            });

            bool isResorted = false;
            for (int i = 0; i < MAX_VISIBLE_PILES; i++)
            {
                if (prevOreIds[i] != pileSets[i].oreId)
                {
                    isResorted = true;
                }
            }

            if (isResorted)
            {
                for (int i = 0; i < pileSets.Count; i++)
                {
                    if (i < MAX_VISIBLE_PILES)
                    {
                        pileSets[i].pile = OreManager.Instance.CreateOrePile(pileSets[i].oreId, (OrePileSize)i);
                        pileSets[i].pile.transform.SetParent(pileArea.transform);
                    }
                    else
                    {
                        pileSets[i].pile.ReturnToPool();
                    }
                }
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
    }
}