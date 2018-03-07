using System.Collections.Generic;
using UnityEngine;

namespace AiukUnityRuntime.System.Asset
{
    /// <summary>
    /// 资源引用基类。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AiukAbsAssetRef<T> : IAiukAssetRef<T> where T : Object
    {
        public T Asset { get; private set; }

        public List<AiukAssetBundleRef> AssetBundleRefs { get; private set; }

        public abstract void Release();

        public AiukAbsAssetRef(T asset, List<AiukAssetBundleRef> abRefs)
        {
            Asset = asset;
            AssetBundleRefs = abRefs;
        }
    }
}

