#region Head

// Author:        liuruoyu1981
// CreateDate:    3/6/2018 11:27:42 AM
// Email:         liuruoyu1981@gmail.com

#endregion

using UnityEngine;

namespace AiukUnityRuntime
{
    public class AiukAssetLoadResult<T> where T : Object
    {
        public AiukAssetBundleRef BundleRef { get; private set; }

        public T Asset { get; private set; }

        public AiukAssetLoadResult(T asset, AiukAssetBundleRef bundleRef)
        {
            BundleRef = bundleRef;
            Asset = asset;
        }
    }
}
