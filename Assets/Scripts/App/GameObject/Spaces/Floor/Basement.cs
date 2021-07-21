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
        private GameObject mineralArea;
        [Header("UI")]
        [SerializeField]
        private TextMeshPro textLimitMiner;

        private Mineral[] minerals;
        private int countMiner;

        private void Awake()
        {
            minerals = mineralArea.GetComponentsInChildren<Mineral>();
            goalElevator.Position = new Vector2(goalElevator.Position.x, spawner.position.y);
            UpdateUI();
        }

        public Mineral GetRandomMineral()
        {
            return minerals[Random.Range(0, minerals.Length)];
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
