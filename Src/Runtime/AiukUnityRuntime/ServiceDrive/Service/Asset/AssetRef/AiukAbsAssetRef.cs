using System.Collections.Generic;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 资源引用基类。
    /// 用于对各种业务逻辑上划分的资源做一个抽象包装。
    /// 提供统一的资源使用维护数据的数据模型及回收方法。
    /// 业务实现者可自主选择在何时进行资源回收。
    /// </summary>
    public abstract class AiukAbsAssetRef<T> where T : UnityEngine.Object
    {
        /// <summary>
        /// 目标资源。
        /// </summary>
        /// <value>The asset.</value>
        public T Asset { get; private set; }

        /// <summary>
        /// 资源所引用的AssetBundle列表。
        /// </summary>
        /// <value>The asset bundle references.</value>
        public List<AiukAssetBundleRef> AssetBundleRefs { get; private set; }

        /// <summary>
        /// 摧毁或者回收资源。
        /// </summary>
        public abstract void Destroy();

        public AiukAbsAssetRef(T asset, List<AiukAssetBundleRef> assetBundleRefs)
        {
            Asset = asset;
            AssetBundleRefs = assetBundleRefs;
        }
    }
}

