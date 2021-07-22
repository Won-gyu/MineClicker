using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public enum OrePileSize
    {
        Small,
        Mid,
        Big,
        VeryBig
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
        private List<GameObject> orePiles;
        private PileSet pileSet;

    }
}