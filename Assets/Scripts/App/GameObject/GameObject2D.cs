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

        public float GetDistanceX(Vector2 position)
        {
            return Mathf.Abs(transform.position.x - position.x);
        }

        public float GetDistanceY(Vector2 position)
        {
            return Mathf.Abs(transform.position.y - position.y);
        }

        public Vector2 GetDirectionX(GameObject2D target)
        {
            return GetDirectionX(target.Position);
        }

        public Vector2 GetDirectionX(Vector2 target)
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

        public Vector2 GetDirectionY(Vector2 target)
        {
            if (target.y >= Position.y)
            {
                return new Vector2(0f, 1f);
            }
            else if (target.y < Position.y)
            {
                return new Vector2(0f, -1f);
            }
            return Vector2.zero;
        }
    }
}