using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public enum OrePileSize
    {
        Small,
        Med,
        Large,
        VeryLarge
    }

    public class PileSet
    {
        public int orePileCount;
        public int orePileId;
    }

    public class DropOff : MonoBehaviour
    {
        public const int MAX_VISIBLE_PILES = 4;

        [SerializeField]
        private GameObject pileArea;
        private List<GameObject> orePiles;
        private List<PileSet> pileSet;

        private void Awake()
        {
            pileSet = new List<PileSet>();
            // for (int i = 0; i < )
        }

        public void DropCarryOre(CarryOre carryOre)
        {

        }
    }
}