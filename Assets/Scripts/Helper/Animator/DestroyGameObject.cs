using UnityEngine;

namespace Helper
{
    public class DestroyGameObject : StateMachineBehaviour
    {
        public bool atExit;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!atExit) Destroy(animator.gameObject);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (atExit) Destroy(animator.gameObject);
        }
    }
}
