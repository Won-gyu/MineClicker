using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public abstract class SheetDataBasedSO : ScriptableObject
    {
        [SerializeField]
        protected string shteetTableObjectDirectory;

    #if UNITY_EDITOR
        protected abstract void ApplySheetInfos();
    #endif
    }
}
