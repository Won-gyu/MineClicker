using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public class GoalOnFloor : GameObject2D
    {
            [SerializeField]
            private float widthDigable;

            public Vector2 GetRandomPositionGoal()
            {
                return Position + new Vector2(-widthDigable * 0.5f + Random.Range(0, widthDigable), 0f);
            }

            private void OnDrawGizmosSelected()
            {
                Gizmos.color = Color.blue;
                Vector2 left = transform.position - new Vector3(widthDigable, 0f);
                Vector2 right = transform.position + new Vector3(widthDigable, 0f);
                Gizmos.DrawLine(left, right);
            }
    }
}