using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public class OreArea : MonoBehaviour
    {
        public const int MAX_ORE = 3;
        private int oreId;
        [SerializeField]
        private WidePlace width;

        private List<Ore> ores;

        public void Init(int oreId)
        {
            this.oreId = oreId;
            ores = new List<Ore>();
            for (int i = 0; i < MAX_ORE; i++)
            {
                ores.Add(SpawnOre());
            }
        }

        private Ore SpawnOre()
        {
            GameObject prefab = OreManager.Instance.GetPrefabOre(oreId);
            var go = Instantiate(prefab) as GameObject;
            go.name = prefab.name;
            go.transform.SetParent(transform, false);
            go.transform.position = width.GetRandomPosition();
            return go.GetComponent<Ore>();
        }

        public Ore GetRandomOre()
        {
            return ores[Random.Range(0, ores.Count)];
        }
    }
}