using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Mine
{
    public class Floor : GameObject2D
    {
        [SerializeField]
        private int floor;
        public int floor
        {
            get
            {
                return floor;
            }
        }
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
        private GoalOnFloor goalElevator;
        public GoalOnFloor GoalElevator
        {
            get
            {
                return goalElevator;
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
