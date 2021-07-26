using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Helper
{
    public class AssetBundleManager : MonoSingleton<AssetBundleManager>
    {
        [SerializeField]
        private List<string> assetBundleNames;

        private Dictionary<string, AssetBundle> bundleDict;
        private bool isInitialized;

        public void Initialize()
        {
            if (!isInitialized)
            {
                isInitialized = true;
                LoadAssetBundles();
            }
        }

        private void LoadAssetBundles()
        {
            bundleDict = new Dictionary<string, AssetBundle>();
            for (int i = 0; i < assetBundleNames.Count; i++)
            {
                var loadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.dataPath, "AssetBundles", assetBundleNames[i]));
                if (loadedAssetBundle == null)
                {
                    Debug.Log("Failed to load AssetBundle!");
                    return;
                }

                bundleDict.Add(assetBundleNames[i], loadedAssetBundle);
            }
        }

#if UNITY_EDITOR
        public static T LoadAssetForEditor<T>(string assetBundleName, string assetName) where T : UnityEngine.Object
        {
            string[] assetPaths = AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(assetBundleName, assetName);
            if (assetPaths.Length > 0)
            {
                return (T)AssetDatabase.LoadMainAssetAtPath(assetPaths[0]);
            }
            else
            {
                Debug.Log("[AssetBundleManager] There is no asset with name \"" + assetName + "\" in " + assetBundleName);
                return null;
            }
        }
#endif

        public T LoadAsset<T>(string assetBundleName, string assetName) where T : UnityEngine.Object
        {
#if UNITY_EDITOR
            return LoadAssetForEditor<T>(assetBundleName, assetName);
#else
            AssetBundle bundle;
            if (bundleDict.TryGetValue(assetBundleName, out bundle))
            {
                return bundle.LoadAsset<T>(assetName);
            }
            else
            {
                Debug.Log("[AssetBundleManager] There is no bundle with name, " +  assetBundleName);
                return null;
            }
#endif
        }
    }
}