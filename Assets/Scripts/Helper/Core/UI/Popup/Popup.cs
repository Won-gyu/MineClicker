using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helper
{
    public class Popup : MonoBehaviour
    {
	    private Canvas canvas;

	    public virtual void Open()
	    {
	        PopupManager.Instance.Open(this);
	    }

	    public virtual void Close()
	    {
	        PopupManager.Instance.Close(this);
	    }

	    public void UpdateSortingLayer(int sortingOrder)
	    {
	        if (canvas == null)
	            canvas = GetComponent<Canvas>();

	        var rootCanvas = transform.parent.GetComponentInParent<Canvas>();
	        canvas.sortingLayerID = rootCanvas.sortingLayerID;
	        canvas.sortingOrder = sortingOrder;
	    }
    }
}