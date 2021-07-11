using System.Collections;
using System.Collections.Generic;
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
        private Transform goal;
        public Vector2 PositionGoal
        {
            get
            {
                return goal.position;
            }
        }
        [SerializeField]
        private GameObject mineralArea;

        private Mineral[] minerals;

        private void Awake()
        {
            minerals = mineralArea.GetComponentsInChildren<Mineral>();
        }
    }
}
