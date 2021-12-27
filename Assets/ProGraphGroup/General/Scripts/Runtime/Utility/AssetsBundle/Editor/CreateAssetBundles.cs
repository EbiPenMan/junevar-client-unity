using System.IO;
using UnityEditor;
using UnityEngine;

namespace ProGraphGroup.General.Utility.AssetsBundle.Editor
{
    public class CreateAssetBundles
    {
        [MenuItem("PrographGroup/Utility/AssetBundles/Build AssetBundles")]
        static void BuildAllAssetBundles()
        {
            string assetBundleDirectory = "Assets/StreamingAssets";
            if (!Directory.Exists(Application.streamingAssetsPath))
            {
                Directory.CreateDirectory(assetBundleDirectory);
            }
            BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
        }
    }
}