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

        [SerializeField]
        private float widthDigable;
        public float WidthDigable
        {
            get
            {
                return widthDigable;
            }
        }

        void OnDrawGizmosSelected()
        {
            if (goal != null)
            {
                Gizmos.color = Color.red;
                Vector2 left = goal.position - new Vector3(widthDigable, 0f);
                Vector2 right = goal.position + new Vector3(widthDigable, 0f);
                Gizmos.DrawLine(left, right);
            }
        }

        private void Awake()
        {
            minerals = mineralArea.GetComponentsInChildren<Mineral>();
        }
    }
}
