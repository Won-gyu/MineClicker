#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

namespace Helper
{
    public class SpreadSheetTableObjectParser : ScriptableObject
    {
        public virtual string Parse(string json) 
        {
            return json;
        }
    }
}

#endif