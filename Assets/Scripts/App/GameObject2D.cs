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
    }
}