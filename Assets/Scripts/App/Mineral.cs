using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public class Mineral : GameObject2D
    {
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
            Gizmos.color = Color.blue;
            Vector2 left = transform.position - new Vector3(widthDigable, 0f);
            Vector2 right = transform.position + new Vector3(widthDigable, 0f);
            Gizmos.DrawLine(left, right);
        }
    }
}