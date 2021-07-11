using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Mine
{
    public class Floor : Space
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
        private GoalOnFloor goal;
        public GoalOnFloor Goal
        {
            get
            {
                return goal;
            }
        }
        [SerializeField]
        private GameObject mineralArea;

        private Mineral[] minerals;

        private void Awake()
        {
            minerals = mineralArea.GetComponentsInChildren<Mineral>();
        }

        public Mineral GetRandomMineral()
        {
            return minerals[Random.Range(0, minerals.Length)];
        }
        
        [Button]
        public void CreateMiner()
        {
            MinerManager.Instance.CreateMiner(this);
        }
    }
}
