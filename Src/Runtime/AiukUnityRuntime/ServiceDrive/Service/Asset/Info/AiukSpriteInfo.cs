using System;

namespace AiukUnityRuntime
{
    [Serializable]
    public class AiukSpriteInfo : IAiukAssetType
    {
        public string Type { get; private set; }

        public string AssetName;

        /// <summary>
        /// 精灵所归属的应用模块。
        /// </summary>
        public string LocModule;

        public AiukSpriteInfo
        (
            string type,
            string assetName,
            string locModule
        )
        {
            Type = type;
            AssetName = assetName;
            LocModule = locModule;
        }

    }
}

