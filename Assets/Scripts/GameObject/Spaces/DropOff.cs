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
        public PileSet()
        {
            orePileCounts = new List<int>();
            orePileIds = new List<int>();
        }
        public List<int> orePileCounts;
        public List<int> orePileIds;
    }

    public class DropOff : MonoBehaviour
    {
        [SerializeField]
        private GameObject pileArea;
        private List<GameObject> orePiles;
        private PileSet pileSet;

        public void DropCarryOre(CarryOre carryOre)
        {

        }
    }
}