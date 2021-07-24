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
        
        protected IEnumerator CoroutineWalkToX(GoalOnFloor goal)
        {
            Vector2 positionGoal = goal.GetRandomPositionGoal();
            Direction = GetDirectionX(positionGoal);
            while (GetDistanceX(positionGoal) > Speed.magnitude)
            {
                Position += Speed;
                yield return null;
            }
        }

        protected IEnumerator CoroutineWalkToY(GameObject2D goal)
        {
            Vector2 positionGoal = goal.Position;
            Direction = GetDirectionY(positionGoal);
            while (GetDistanceY(goal.Position) > Speed.magnitude)
            {
                Position += Speed;
                yield return null;
            }
        }
    }
}
