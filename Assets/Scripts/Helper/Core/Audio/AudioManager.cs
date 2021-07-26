using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Helper
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        [SerializeField]
        private GameObjectPool pool;
        [SerializeField]
        private AudioDataSO audioDataSO;

        private static AudioPlayer GetPlayer(string id)
        {
            AudioDataInfo info = Instance.audioDataSO.GetAudioData(id);
            var obj = Instance.pool.GetObject().GetComponent<AudioPlayer>();
            obj.source.clip = info.clip;
            return obj;
        }
    }
}