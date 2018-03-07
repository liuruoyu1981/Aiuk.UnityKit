using System.Collections.Generic;
using UnityEngine;

namespace AiukUnityRuntime.System.Asset
{
    /// <summary>
    /// 泛型资源引用接口。
    /// </summary>
    public interface IAiukAssetRef<out T> where T : Object
    {
        T Asset { get; }

        List<AiukAssetBundleRef> AssetBundleRefs { get; }
    }
}

