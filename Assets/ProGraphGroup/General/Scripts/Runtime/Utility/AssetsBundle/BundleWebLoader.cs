using System;
using System.Collections;
using UnityEngine;

namespace ProGraphGroup.General.Utility.AssetsBundle
{
    public class BundleWebLoader : MonoBehaviour
    {
        private string bundleUrl = "http://localhost/assetbundles/";
        private string assetName = "BundledObject";

        // Start is called before the first frame update
        IEnumerator load(String bundleName, String _assetName)
        {
            using (WWW web = new WWW(bundleUrl + bundleName))
            {
                yield return web;
                AssetBundle remoteAssetBundle = web.assetBundle;
                if (remoteAssetBundle == null)
                {
                    Debug.LogError("Failed to download AssetBundle!");
                    yield break;
                }
                GameObject go = Instantiate(remoteAssetBundle.LoadAsset(_assetName)) as GameObject;
                go.transform.parent = transform;
                remoteAssetBundle.Unload(false);
            }
        }
    
    

        public void LoadHome(int index)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            if (index < 3)
            {
                StartCoroutine(load("homepackage_1", "Home_1_" + index));
            }
            else
            {
                StartCoroutine(load("homepackage_2", "Home_2_" + (index - 2)));
            }
        }
        public void LoadChair(int index)
        {
            StartCoroutine(load("chairpackage_1", "Chair_1_" + index));
        }
    }
}