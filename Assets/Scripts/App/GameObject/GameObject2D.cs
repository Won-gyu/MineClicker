using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public class GameObject2D : MonoBehaviour
    {
        public Vector2 Position
        {
            get
            {
                return transform.position;
            }
            set
            {
                transform.position = new Vector3(value.x, value.y);
            }
        }

        public Vector2 GetDistanceX(Vector2 position)
        {
            return new Vector2(transform.position.x - position.x, 0f);
        }

        public Vector2 GetDirection(GameObject2D target)
        {
            return GetDirection(target.Position);
        }

        public Vector2 GetDirection(Vector2 target)
        {
            if (target.x >= Position.x)
            {
                return new Vector2(1f, 0f);
            }
            else if (target.x < Position.x)
            {
                return new Vector2(-1f, 0f);
            }
            return Vector2.zero;
        }
    }
}