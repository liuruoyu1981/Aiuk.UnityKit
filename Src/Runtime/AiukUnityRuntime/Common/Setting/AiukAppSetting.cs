using System;
using System.Collections.Generic;
using Aiuk.Common.Utility;

namespace AiukUnityRuntime
{
    /// <summary>
    /// Aiuk应用设置，应用是多个App模块的容器。
    /// </summary>
    [Serializable]
    public class AiukAppSetting
    {
        /// <summary>
        /// 应用名。
        /// </summary>
        public string Name;

        public string RootDir;

        /// <summary>
        /// 应用的组织或公司标识。
        /// </summary>
        public string OrganizationName;

        /// <summary>
        /// 应用模块列表。
        /// </summary>
        public List<AiukAppModuleSetting> AppModules = new List<AiukAppModuleSetting>();

        /// <summary>
        /// 应用的当前模块。
        /// </summary>
        /// <value>The current module.</value>
        public string CurrentModuleName;

        public AiukAppModuleSetting CurrentModule
        {
            get
            {
                var module = AppModules.Find(m => m.Name == CurrentModuleName);
                return module;
            }
        }

        public AiukAppSetting
        (
            string organizationName,
            string appName,
            string rootDir)
        {
            OrganizationName = organizationName;
            Name = appName;
            RootDir = rootDir;

            var shareModule = new AiukAppModuleSetting(this, "Share");
            AddModule(shareModule);
        }

        private bool IsExist(string name)
        {
            var targetModule = AppModules.Find(module => module.Name == name);
            return targetModule != null;
        }

        /// <summary>
        /// 添加一个新的应用模块。
        /// </summary>
        /// <param name="module">Module.</param>
        public void AddModule(AiukAppModuleSetting module)
        {
            if (IsExist(module.Name))
            {
                AiukDebugUtility.LogError(
                    string.Format("目标模块{0}已存在，添加失败！", module.Name));
                return;
            }

            AppModules.Add(module);
            CurrentModuleName = module.Name;
        }

        public void AddModule(string moduleName)
        {
            if (IsExist(moduleName))
            {
                AiukDebugUtility.LogError(
                    string.Format("目标模块{0}已存在，添加失败！", moduleName));
                return;
            }
            
            var module = new AiukAppModuleSetting(this,moduleName);
            AppModules.Add(module);
            CurrentModuleName = moduleName;
        }
    }
}
