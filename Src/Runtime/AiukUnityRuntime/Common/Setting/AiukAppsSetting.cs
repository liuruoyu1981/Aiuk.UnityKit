using System;
using System.Collections.Generic;
using System.IO;
using Aiuk.Common.Utility;
using UnityEngine;

namespace AiukUnityRuntime
{
    /// <summary>
    /// unity应用集合设置。
    /// aiuk框架可以选择任意多个App实例进行打包及启动，相关的设置保存着AppsSetting中。
    /// 应用集合设置不做热更新处理。
    /// </summary>
    [Serializable]
    public class AiukAppsSetting
    {
        public string CreateDate;
        public string UpdateDate;
        public List<AiukAppSetting> AppSetings = new List<AiukAppSetting>();

        public AiukAppsSetting()
        {
            CreateDate = DateTime.Now.ToLongDateString();
        }

        #region 路径

        public static string SandboxPath
        {
            get
            {
                return Application.persistentDataPath + "/AiukAppsSetting.json";
            }
        }

        /// <summary>
        /// 编辑器下加载路径。
        /// StreamingAssets目录下以AiukAppsSetting.json保存。
        /// </summary>
        /// <value>The editor path.</value>
        public static string EditorPath
        {
            get
            {
                return Application.streamingAssetsPath + "/AiukAppsSetting.json";
            }
        }


        #endregion

        #region 单例

        private static AiukAppsSetting _Instance;
        public static AiukAppsSetting Instance
        {
            get
            {
                if (_Instance != null) return _Instance;

#if UNITY_EDITOR
                //  如果编辑器下的默认路径不存在Apps设置文件则创建一个新的。
                if (!File.Exists(EditorPath))
                {
                    var newAppsSetting = new AiukAppsSetting();
                    //  覆盖沙盒下的设置文件。
                    Save(newAppsSetting);
                }
#endif
                _Instance = ReadFromSandbox();
                return _Instance;
            }
        }

        private static AiukAppsSetting ReadFromSandbox()
        {
            if (!File.Exists(SandboxPath))
            {
#if UNITY_EDITOR
                AiukIOUtility.TryCopy(EditorPath, SandboxPath);
#else
                content = Resources.Load<TextAsset>("AiukAppsSetting").text;
                File.WriteAllText(SandboxPath, content);
#endif
            }

            var content = File.ReadAllText(SandboxPath);
            var instance = JsonUtility.FromJson<AiukAppsSetting>(content);
            return instance;
        }


        /// <summary>
        /// 置空Apps单例。
        /// </summary>
        public static void SetInstanceNull()
        {
            _Instance = null;
        }

        #endregion

        #region App

        /// <summary>
        /// 当前App名。
        /// 开发、自动化工具、运行都以当前App作为目标App。
        /// </summary>
        /// <value>The current app.</value>
        public string CurrentAppName;

        public AiukAppSetting CurrentApp
        {
            get
            {
                var app = AppSetings.Find(a => a.Name == CurrentAppName);
                return app;
            }
        }

        /// <summary>
        /// 检测一个目标App是否已在配置中存在。
        /// </summary>
        /// <returns><c>true</c>, if exist was ised, <c>false</c> otherwise.</returns>
        /// <param name="appName">App name.</param>
        public static bool IsExist(string appName)
        {
            var targetApp = Instance.AppSetings.Find(app => app.Name == appName);
            return targetApp != null;
        }

        /// <summary>
        /// 添加一个新的应用。
        /// </summary>
        /// <param name="app">App.</param>
        public static void AddApp(AiukAppSetting app)
        {
            if (IsExist(app.Name))
            {
                AiukDebugUtility.LogError(
                    string.Format("目标应用{0}已存在，添加失败！", app.Name));
                return;
            }

            Instance.AppSetings.Add(app);
            Debug.Log(Instance.AppSetings.Count);
            //  更新当前应用为新添加的应用。
            Instance.CurrentAppName = app.Name;
        }

        public static AiukAppSetting GetApp(string appName)
        {
            var app = Instance.AppSetings.Find(a => a.Name == appName);
            return app;
        }

        public static void DeleteApp(string appName)
        {
            if (!IsExist(appName))
            {
                AiukDebugUtility.LogError(string.Format("目标App{0}不存在，无法删除！", appName));
                return;
            }

            var app = GetApp(appName);
            Instance.AppSetings.Remove(app);
        }

        public static void Save(AiukAppsSetting instance)
        {
            var content = JsonUtility.ToJson(instance);
            File.WriteAllText(EditorPath, content);
            //  覆盖沙盒下的设置文件。
            File.WriteAllText(SandboxPath, content);
        }

        public static void DeleteAllAppsSetting()
        {
            AiukIOUtility.TryDeleteFile(EditorPath);
            AiukIOUtility.TryDeleteFile(SandboxPath);
        }

        #endregion


    }
}



