namespace AiukUnityRuntime
{
    /// <summary>
    /// 设置助手。
    /// </summary>
    public static class AiukSettingHelper
    {
        /// <summary>
        /// 当前应用的当前模块。
        /// </summary>
        /// <value>The current app module.</value>
        public static AiukAppModuleSetting CurrentAppModule
        {
            get
            {
                var currentApp = AiukAppsSetting.Instance.CurrentApp;
                var currentModule = currentApp.CurrentModule;
                return currentModule;
            }
        }

        /// <summary>
        /// 创建一个新的应用集合设置文件。
        /// </summary>
        /// <param name="organizationName">Organization name.</param>
        /// <param name="newAppName">New app name.</param>
        /// <param name="newModuleName">New module name.</param>
        public static void CreateAppsSetting
        (
            string organizationName,
            string newAppName,
            string newModuleName,
            string rootDir
        )
        {
            var appSetting = new AiukAppSetting(organizationName, newAppName, rootDir);
            var appModule = new AiukAppModuleSetting(appSetting, newModuleName);
            appSetting.AddModule(appModule);
            //  添加各应用模块共享的Share模块。
            var shareModule = new AiukAppModuleSetting(appSetting, "Share");
            appSetting.AddModule(shareModule);
            var appsSetting = new AiukAppsSetting();
        }
    }
}


