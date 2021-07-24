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
            MessageDispatcher.Subscribe(OreManager.EVENT_ORE_PILE_STORED, OnOrePileStored);
        }

        protected void OnDestroy()
        {
            MessageDispatcher.UnSubscribe(OreManager.EVENT_ORE_PILE_STORED, OnOrePileStored);
        }
        
        private void OnOrePileStored(EventData eventData)
        {
            textScore.SetText(string.Format("SCORE: {0}", UserDataManager.Instance.OreStored));
        }
    }
}
