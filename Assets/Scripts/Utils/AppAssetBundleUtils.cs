using UnityEngine;
using System;
using System.IO;
using Helper;

namespace Mine
{
    public class AppAssetBundleUtils
    {
        public static T LoadCommonAsset<T>(string assetName) where T : UnityEngine.Object
        {
            return AssetBundleManager.Instance.LoadAsset<T>("common", assetName);
        }
    }
}