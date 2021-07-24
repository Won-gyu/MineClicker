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
            MessageDispatcher.Subscribe(UserDataManager.EVENT_CREDIT_ADDED, OnCreditAdded);
        }

        protected void OnDestroy()
        {
            MessageDispatcher.UnSubscribe(UserDataManager.EVENT_CREDIT_ADDED, OnCreditAdded);
        }
        
        private void OnCreditAdded(EventData eventData)
        {
            textScore.SetText(string.Format("SCORE: {0}", (long)UserDataManager.Instance.UserData.credit));
        }
    }
}
