using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Helper
{
    [CreateAssetMenu(fileName="HelperSettings", menuName="Helper/ScriptableObject/HelperSettings")]
    public class HelperSettings : ScriptableObjectSingleton<HelperSettings>
    {
        public string googleWebServiceUrl;
        public string googleWebServicePassword;
    }
}
