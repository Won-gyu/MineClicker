using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public class SpriteGroup : MonoBehaviour
    {
        [SerializeField]
        private int additionalOrder;

        public void Apply()
        {
            SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                spriteRenderers[i].sortingOrder += additionalOrder;
            }
        }

        public void SetAdditionalOrder(int additionalOrder)
        {
            this.additionalOrder = additionalOrder;
        }
    }
}