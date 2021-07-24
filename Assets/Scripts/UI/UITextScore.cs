using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Helper;

namespace Mine
{
    public class UITextScore : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI textScore;

        private void Awake()
        {
            MessageDispatcher.Subscribe(StageManager.EVENT_ORE_STORED, OnOreStored);
        }

        protected void OnDestroy()
        {
            MessageDispatcher.UnSubscribe(StageManager.EVENT_ORE_STORED, OnOreStored);
        }
        
        private void OnOreStored(EventData eventData)
        {
            textScore.SetText(string.Format("SCORE: {0}", StageManager.Instance.OreStored));
        }
    }
}
