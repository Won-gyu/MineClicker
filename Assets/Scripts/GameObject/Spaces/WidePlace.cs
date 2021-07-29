using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public class WidePlace : GameObject2D
    {
        [SerializeField]
        private float width;

        public Vector2 GetRandomPosition()
        {
            return Position + new Vector2(-width * 0.5f + Random.Range(0, width), 0f);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Vector2 left = transform.position - new Vector3(width * 0.5f, 0f);
            Vector2 right = transform.position + new Vector3(width * 0.5f, 0f);
            Gizmos.DrawLine(left, right);
        }
    }
}