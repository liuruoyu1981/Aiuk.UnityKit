using System;
using System.Linq;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 资源数据，用于支持自动生成资源数据，维护资源的加载上下文及自动生成脚本。
    /// </summary>
    [Serializable]
    public class AiukAssetInfo : IAiukAssetType
    {
        /// <summary>
        /// 资源分类字符串。
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// AssetBundle名。
        /// </summary>
        public string BundleName;

        /// <summary>
        /// 资源名。
        /// </summary>
        public string AssetName;

        /// <summary>
        /// 相对路径。
        /// </summary>
        [NonSerialized]
        private string RelativePath;

        /// <summary>
        /// 资源归属于那个应用模块。
        /// </summary>
        public string LocModule;

        [NonSerialized]
        public string ImporterPath;

        public AiukAssetInfo
        (
            AiukAppModuleSetting module,
            string assetName,
            string fullPath
        )
        {
            AssetName = assetName;
            LocModule = module.Name;
            var helper = new AiukAppModuleHelper(module);
            Type = fullPath.Replace(helper.AssetDatabaseRootDir, "")
                             .Split('/').First();
            RelativePath = fullPath.Replace(helper.AssetDatabaseRootDir, "")
                                     .Split('.').First();
            ImporterPath = AiukUnityIOUtility.GetImporterPath(fullPath);
        }


        private void GetBundleName(string type, AiukAppModuleSetting module)
        {
            var assetSetting = module.GetAssetSetting(type);
            BundleName = assetSetting.IsSingle ? AssetName.ToLower() : type.ToLower();
        }

    }
}

