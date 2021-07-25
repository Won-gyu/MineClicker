using UnityEngine;

namespace Helper
{
    public class AudioPlayer : PooledGameObject
    {
        public AudioSource source;
        private string id = "";

        public void SetId(string id)
        {
            id = this.id;
        }

        public override void ReturnToPool()
        {
            base.ReturnToPool();
        }

        private void LateUpdate()
        {
            if (!source.isPlaying)
            {
                ReturnToPool();
            }
        }
    }
}