using System;
using System.Collections.Generic;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 模块设置。
    /// 模块为一个unity应用（App）的组成部分，一个应用至少需要有一个模块。
    /// </summary>
    [Serializable]
    public class AiukAppModuleSetting
    {
        /// <summary>
        /// 应用模块所在的App名。
        /// </summary>
        public string AppName;

        /// <summary>
        /// 模块名。
        /// </summary>
        public string Name;

        private string m_Token;

        /// <summary>
        /// 应用模块的唯一标识符。
        /// 由应用名_模块名组成。
        /// 处理不同的应用下可能存在同名的模块问题。
        /// </summary>
        /// <value>The token.</value>
        public string Token
        {
            get
            {
                if (m_Token != null) return m_Token;

                m_Token = AppName + "_" + Name;
                return m_Token;
            }
        }

        /// <summary>
        /// 模块的根目录（相对目录）。
        /// </summary>
        public string RootDir;

        /// <summary>
        /// 应用模块中各种资源的打包及加载设置。
        /// </summary>
        public List<AiukAssetBundleSetting> AbSettings = new List<AiukAssetBundleSetting>();

        public AiukAppModuleSetting
        (
            AiukAppSetting appSetting,
            string moduleName
        )
        {
            AppName = appSetting.Name;
            Name = moduleName;
            RootDir = appSetting.RootDir + Name + "/";

            //  初始化默认的资源加载策略。
            InitDefaultAssetSetting();
        }

        private void InitDefaultAssetSetting()
        {
            AbSettings.Add(new AiukAssetBundleSetting(AiukAssetType.Jint, false));
            AbSettings.Add(new AiukAssetBundleSetting(AiukAssetType.Animation, false));
            AbSettings.Add(new AiukAssetBundleSetting(AiukAssetType.Atlas, true));
            AbSettings.Add(new AiukAssetBundleSetting(AiukAssetType.Binary, false));
            AbSettings.Add(new AiukAssetBundleSetting(AiukAssetType.Font, true));
            AbSettings.Add(new AiukAssetBundleSetting(AiukAssetType.LocalData, true));
            AbSettings.Add(new AiukAssetBundleSetting(AiukAssetType.Material, true));
            AbSettings.Add(new AiukAssetBundleSetting(AiukAssetType.Music, true));
            AbSettings.Add(new AiukAssetBundleSetting(AiukAssetType.SoundEffect, false));
            AbSettings.Add(new AiukAssetBundleSetting(AiukAssetType.Shader, false));
            AbSettings.Add(new AiukAssetBundleSetting(AiukAssetType.Texture, true));
            AbSettings.Add(new AiukAssetBundleSetting(AiukAssetType.View, true));
        }

        /// <summary>
        /// 获得指定资源类型的资源设置。
        /// </summary>
        /// <returns>The asset setting.</returns>
        /// <param name="type">Type.</param>
        public AiukAssetBundleSetting GetAssetSetting(string type)
        {
            var assetSetting = AbSettings.Find(s => s.Type == type);
            return assetSetting;
        }

    }
}
