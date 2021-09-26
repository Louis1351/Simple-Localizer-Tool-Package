using System.IO;
using UnityEditor;
using UnityEngine;

namespace LS.Localiser
{
    public class CreateAssetBundles
    {
        [MenuItem("Localization/Build AssetBundles")]
        static void BuildAllAssetBundles()
        {
            string assetStreamingDirectory = "Assets/StreamingAssets";
            string assetBundleDirectory = "Assets/BundledAssets";
            string assetLocalizerDirectory = "Assets/BundledAssets/localizerbundle";

            if (!Directory.Exists(Application.streamingAssetsPath))
            {
                Directory.CreateDirectory(assetStreamingDirectory);
            }

            if (!Directory.Exists(assetBundleDirectory))
            {
                Directory.CreateDirectory(assetBundleDirectory);
            }

            if (!Directory.Exists(assetLocalizerDirectory))
            {
                Directory.CreateDirectory(assetLocalizerDirectory);
            }

            BuildPipeline.BuildAssetBundles(assetStreamingDirectory, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
            AssetDatabase.Refresh();
        }
    }
}
