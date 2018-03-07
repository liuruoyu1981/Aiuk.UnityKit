using UnityEngine;

namespace AiukUnityRuntime.System.Asset
{
    /// <summary>
    /// AssetBundle引用。
    /// 用于管理AssetBundle文件的引用次数和释放。
    /// </summary>
    public class AiukAssetBundleRef
    {
        /// <summary>
        /// AssetBundle实例。
        /// </summary>
        private readonly AssetBundle m_AssetBundle;

        /// <summary>
        /// 被引用次数。
        /// </summary>
        public int RefCount { get; private set; }

        public AiukAssetBundleRef(AssetBundle assetBundle)
        {
            m_AssetBundle = assetBundle;
        }

        /// <summary>
        /// 获取Ab包中的资源并使Ab包的引用计数加一。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public T GetAsset<T>(string assetName) where T : Object
        {
            var asset = m_AssetBundle.LoadAsset<T>(assetName);
            return asset;
        }
    }
}
