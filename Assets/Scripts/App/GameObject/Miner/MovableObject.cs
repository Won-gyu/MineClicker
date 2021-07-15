using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public class MovableObject : GameObject2D
    {
        private Vector2 direction;
        protected virtual Vector2 Direction
        {
            set
            {
                direction = value;
                if (flipX)
                    transform.localScale =  new Vector3(-direction.x, transform.localScale.y, transform.localScale.z);
            }
            get
            {
                return direction;
            }
        }

        public float speed;
        protected Vector2 Speed
        {
            get
            {
                return Direction * speed  * Time.deltaTime;
            }
        }

        protected bool flipX;
    }
}
