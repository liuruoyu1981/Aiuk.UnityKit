using System;

namespace AiukUnityRuntime
{
    /// <summary>
    /// AssetBundle设置。
    /// 1. 打包及加载类型。
    /// </summary>
    [Serializable]
    public class AiukAssetBundleSetting
    {
        /// <summary>
        /// 资源类型，资源从业务逻辑上的划分类型。
        /// 相同业务类型的资源存放在同一个目录下。
        /// 例如： Music，View。
        /// </summary>
        public string Type;

        /// <summary>
        /// 是否单独打包及加载。
        /// </summary>
        public bool IsSingle;

        public AiukAssetBundleSetting(string type, bool isSingle)
        {
            Type = type;
            IsSingle = isSingle;
        }
    }
}


