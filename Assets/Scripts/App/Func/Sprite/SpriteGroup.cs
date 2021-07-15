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
                // Debug.Log("###1 - " + spriteRenderers[i].name + ": "  + spriteRenderers[i].sortingOrder + " + " + additionalOrder);
                spriteRenderers[i].sortingOrder += additionalOrder;
                // Debug.Log("###2 - " + spriteRenderers[i].sortingOrder);
            }
        }

        public void SetAdditionalOrder(int additionalOrder)
        {
            this.additionalOrder = additionalOrder;
        }
    }
}