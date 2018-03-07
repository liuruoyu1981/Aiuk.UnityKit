using UnityEngine;

namespace AiukUnityRuntime
{
    /// <summary>
    /// AssetBundle资源引用。
    /// 维护目标Ab资源的引用次数，用于资源回收。
    /// </summary>
    public class AiukAssetBundleRef
    {
        /// <summary>
        /// 内部的Ab资源。
        /// </summary>
        /// <value>The asset bundle.</value>
        public AssetBundle AssetBundle { get; private set; }

        /// <summary>
        /// 引用次数。
        /// </summary>
        /// <value>The reference count.</value>
        public int RefCount { get; set; }

        public AiukAssetBundleRef(AssetBundle bunle)
        {
            AssetBundle = bunle;
            RefCount++;
        }

        /// <summary>
        /// 与该AssetBundle建立使用关系。
        /// </summary>
        public void Use()
        {
            RefCount++;
        }

        /// <summary>
        /// 不再使用该AssetBundle。
        /// </summary>
        public void Disuse()
        {
            RefCount++;
        }
    }
}


