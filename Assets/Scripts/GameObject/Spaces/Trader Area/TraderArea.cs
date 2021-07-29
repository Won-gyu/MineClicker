using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public class TraderArea : MonoBehaviour
    {
        [SerializeField]
        private Trader trader;

        [SerializeField]
        private WidePlace start;
        public WidePlace Start
        {
            get
            {
                return start;
            }
        }
        [SerializeField]
        private WidePlace goal;
        public WidePlace Goal
        {
            get
            {
                return goal;
            }
        }
        [SerializeField]
        private WidePlace dropOff;
        public WidePlace DropOff
        {
            get
            {
                return dropOff;
            }
        }

        private void Awake()
        {
            trader.Init(this);
        }
    }
}