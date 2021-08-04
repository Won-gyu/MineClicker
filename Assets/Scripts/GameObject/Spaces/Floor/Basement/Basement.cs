using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;

namespace Mine
{
    public class Basement : Floor
    {
        [SerializeField]
        private Transform spawner;
        public Vector2 PositionSpawner
        {
            get
            {
                return spawner.position;
            }
        }
        [SerializeField]
        private OreArea oreArea;

        [Header("UI")]
        [SerializeField]
        private TextMeshPro textLimitMiner;

        private int countMiner;

        private void Awake()
        {
            goalElevator.Position = new Vector2(goalElevator.Position.x, spawner.position.y);
            oreArea.Init(StaticDataManager.Instance.GetFloorLevelData(FloorLevel).oreId);
            UpdateUI();
        }

        public Ore GetRandomOre()
        {
            return oreArea.GetRandomOre();
        }
        
        [Button]
        public void CreateMiner()
        {
            if (countMiner < FloorTierData.limitCount)
            {
                MinerManager.Instance.CreateMiner(this);
                countMiner++;
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            textLimitMiner.SetText(string.Format("{0}/{1}", countMiner, FloorTierData.limitCount));
        }
    }
}
