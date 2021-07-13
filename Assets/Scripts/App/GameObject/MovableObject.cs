using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public class MovableObject : GameObject2D
    {
        private Vector2 direction;
        protected Vector2 Direction
        {
            set
            {
                direction = value;
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
    }
}
