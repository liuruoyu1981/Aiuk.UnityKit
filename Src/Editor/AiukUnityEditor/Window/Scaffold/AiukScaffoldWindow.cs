using System.Linq;
using AiukUnityRuntime;
using UnityEditor;
using UnityEngine;

namespace AiukUnityEditor
{
    /// <summary>
    /// 脚手架窗口。
    /// </summary>
    public class AiukScaffoldWindow : EditorWindow
    {
        #region 字段

        private static AiukScaffoldWindow _Window;

        private AiukAppsSetting m_AppsSetting;

        /// <summary>
        /// 当前所选择的应用数组索引。
        /// </summary>
        private int m_AppIndex;

        /// <summary>
        /// 应用名数组.
        /// </summary>
        private string[] m_AppArray = new string[0];

        /// <summary>
        /// 应用的根路径。
        /// </summary>
        private string m_AppRootDir;

        private string m_OrganizationName;

        private string CurrentAppName
        {
            get
            {
                var appName = m_AppArray[m_AppIndex];
                return appName;
            }
        }

        public AiukAppSetting CurrentApp
        {
            get
            {
                var app = AiukAppsSetting.Instance.AppSetings.Find(
                    a => a.Name == CurrentAppName);
                return app;
            }
        }

        /// <summary>
        /// 当前所选择的模块名数组索引。
        /// </summary>
        private int m_AppModuleIndex;

        /// <summary>
        /// 应用模块名数组。
        /// </summary>
        private string[] m_ModuleArray = new string[0];

        private string CurrentModuleName
        {
            get
            {
                var moduleName = m_ModuleArray[m_AppModuleIndex];
                return moduleName;
            }
        }

        /// <summary>
        /// 所选择应用的当前开发者数组索引。
        /// </summary>
        private int m_DeveloperIndex = 0;

        /// <summary>
        /// 当前应用的开发者列表。
        /// </summary>
        private string[] m_DeveloperArray = new string[0];

        /// <summary>
        /// 将要添加的新应用的名字。
        /// </summary>
        private string m_NewAppName;

        /// <summary>
        /// 将要添加的新应用模块的名字。
        /// </summary>
        private string m_NewModuleName;

        #endregion

        #region 样式

        /// <summary>
        /// 内容样式。
        /// </summary>
        private GUIStyle m_ContentStyle;

        private void InitStyle()
        {
            m_ContentStyle = new GUIStyle
            {
                alignment = TextAnchor.MiddleLeft
            };
        }

        #endregion

        public static void ShowWindow()
        {
            if (_Window != null)
            {
                _Window.Focus();
            }

            _Window = GetWindow<AiukScaffoldWindow>();
            _Window.titleContent = new GUIContent("应用脚手架");
            _Window.minSize = new Vector2(600, 500);
            _Window.InitContext();
            _Window.InitStyle();
        }

        /// <summary>
        /// 初始化脚手架工具运行所需的上下文环境。
        /// 加载及更新AppsSetting。
        /// </summary>
        private void InitContext()
        {
            m_AppRootDir = Application.dataPath + "/";

            //  初始化AppsSetting实例。
            m_AppsSetting = AiukAppsSetting.Instance;
            if (m_AppsSetting.AppSetings.Count == 0) return;

            var currentAppName = m_AppsSetting.CurrentAppName;
            m_AppArray = m_AppsSetting.AppSetings.Select(app => app.Name).ToArray();
            m_AppIndex = m_AppsSetting.AppSetings.FindIndex(app => app.Name == currentAppName);

            var currentApp = m_AppsSetting.CurrentApp;

            if (currentApp.AppModules.Count == 0) return;


            var currentModuleName = currentApp.CurrentModule.Name;
            m_ModuleArray = currentApp.AppModules.Select(m => m.Name).ToArray();
            m_AppModuleIndex = currentApp.AppModules.FindIndex(m => m.Name == currentModuleName);
            AssetDatabase.Refresh();
        }

        #region 绘制左部

        private void DrawCurrentApp()
        {
            using (new AiukHorizontalLayout())
            {
                EditorGUILayout.LabelField("当前应用：", EditorStyles.boldLabel);
                EditorGUILayout.LabelField(m_AppsSetting.CurrentApp == null ? "无当前应用" : m_AppsSetting.CurrentApp.Name,
                    m_ContentStyle);
            }
        }

        private void DrawAppList()
        {
            using (new AiukHorizontalLayout())
            {
                EditorGUILayout.LabelField("应用列表：", EditorStyles.boldLabel);
                EditorGUILayout.Popup(m_AppIndex, m_AppArray);
            }
        }

        private void DrawAppModuleList()
        {
            using (new AiukHorizontalLayout())
            {
                EditorGUILayout.LabelField("所选应用的模块列表：", EditorStyles.boldLabel);
                EditorGUILayout.Popup(m_AppModuleIndex, m_ModuleArray);
            }
        }

        private void DrawDeveloperList()
        {
            using (new AiukHorizontalLayout())
            {
                EditorGUILayout.LabelField("所选模块的开发者列表：", EditorStyles.boldLabel);
                EditorGUILayout.Popup(m_DeveloperIndex, m_DeveloperArray);
            }
        }

        private void DrawAddApp()
        {
            using (new AiukHorizontalLayout())
            {
                GUILayout.Label("添加一个新应用：");
                GUILayout.Label("应用名：");
                m_NewAppName = GUILayout.TextField(m_NewAppName);
                if (GUILayout.Button("添加新应用"))
                {
                    AddApp();
                }
            }
        }

        private void DrawAppRootSelect()
        {
            using (new AiukHorizontalLayout())
            {
                GUILayout.Label("新应用根目录，默认为Assets下");
                m_AppRootDir = GUILayout.TextField(m_AppRootDir);
                if (GUILayout.Button("选择新应用根目录"))
                {
                    if (string.IsNullOrEmpty(m_NewAppName)
                        || string.IsNullOrEmpty(m_NewModuleName))
                    {
                        EditorUtility.DisplayDialog("出错了"
                            , "请先填写新建应用名或者新建应用的新建模块名", "知道了");
                        return;
                    }

                    m_AppRootDir = EditorUtility.OpenFolderPanel(
                        string.Format("选择新应用{0}的根目录", m_NewAppName),
                        Application.dataPath, "");
                    //  修正为相对于Assets的目录
                    m_AppRootDir = m_AppRootDir.Replace(Application.dataPath + "/", "");
                }
            }
        }

        private void AddApp()
        {
            if (AiukAppsSetting.IsExist(m_NewAppName))
            {
                EditorUtility.DisplayDialog("应用已存在",
                    string.Format("所要添加的应用{0}已存在，添加应用失败！", m_NewAppName),
                    "知道了");
                return;
            }

            if (string.IsNullOrEmpty(m_NewAppName))
            {
                EditorUtility.DisplayDialog("应用名为空", "不能添加名字为空的应用，添加应用失败！",
                    "知道了");
                return;
            }

            if (string.IsNullOrEmpty(m_OrganizationName))
            {
                EditorUtility.DisplayDialog("组织名为空", "请先填写应用的组织名，添加应用失败！",
                    "知道了");
                return;
            }

            if (string.IsNullOrEmpty(m_NewModuleName))
            {
                EditorUtility.DisplayDialog("新应用模块名为空", "请先填写新应用模块名，添加应用失败！",
                    "知道了");
                return;
            }

            var newApp = new AiukAppSetting(m_OrganizationName, m_NewAppName,
                m_AppRootDir + "/" + m_NewAppName + "/");
            AiukAppsSetting.AddApp(newApp);
            AiukAppsSetting.Instance.CurrentApp.AddModule(m_NewModuleName);
            m_NewAppName = null;
            m_OrganizationName = null;
            m_AppRootDir = null;
            m_NewModuleName = null;
            AiukAppsSetting.Save(AiukAppsSetting.Instance);
            AssetDatabase.Refresh();
            InitContext();
        }

        private void AddAppModule()
        {
            if (AiukAppsSetting.IsExist(m_NewModuleName))
            {
                EditorUtility.DisplayDialog("模块已存在",
                    string.Format("所要添加的模块{0}已存在，添加模块失败！", m_NewModuleName),
                    "知道了");
                return;
            }

            if (string.IsNullOrEmpty(m_NewModuleName))
            {
                EditorUtility.DisplayDialog("模块名为空", "不能添加名字为空的模块，添加模块失败！",
                    "知道了");
                return;
            }

            var newModule = new AiukAppModuleSetting(m_AppsSetting.CurrentApp, m_NewModuleName);
            m_AppsSetting.CurrentApp.AddModule(newModule);
            m_NewModuleName = null;
            AiukAppsSetting.Save(AiukAppsSetting.Instance);
            AssetDatabase.Refresh();
            InitContext();
        }

        private void DrawAddAppModule()
        {
            using (new AiukHorizontalLayout())
            {
                GUILayout.Label("添加一个新应用模块：");
                GUILayout.Label("应用模块名：");
                m_NewModuleName = GUILayout.TextField(m_NewModuleName);
                if (GUILayout.Button("添加新应用模块"))
                {
                    AddAppModule();
                }
            }
        }

        private void DrawAppOrganizationName()
        {
            using (new AiukHorizontalLayout())
            {
                GUILayout.Label("新应用组织名");
                m_OrganizationName = GUILayout.TextField(m_OrganizationName);
            }
        }

        #region 功能按钮

        private void DeleteSelectApp()
        {
        }

        private void DrawFutureButtons()
        {
            using (new AiukHorizontalLayout())
            {
                DrawUpadteSelectAppModule();
                DrawDeleteAllAppsSetting();
            }

            using (new AiukHorizontalLayout())
            {
            }
        }

        private void DrawDeleteAllAppsSetting()
        {
            if (GUILayout.Button("删除沙盒及StreamAssets下的Apps设置文件"))
            {
                AiukAppsSetting.DeleteAllAppsSetting();
                InitContext();
                AssetDatabase.Refresh();
                EditorUtility.DisplayDialog("删除成功",
                    "沙盒及StreaAssets目录下的Apps设置文件已删除。",
                    "知道了");
            }
        }

        /// <summary>
        /// 更新当前应用旗下所有模块的文件夹结构。
        /// </summary>
        private void DrawUpadteSelectAppModule()
        {
            if (!GUILayout.Button("更新当前应用下所有模块的文件夹结构")) return;
            var modules = AiukAppsSetting.Instance.CurrentApp.AppModules;
            foreach (var module in modules)
            {
                var builder = new AiukAppModuleBuilder(module);
                builder.BuildAppModule();
            }

            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("更新完毕", "当前应用的文件夹结构已更新完毕",
                "知道了");
        }

        #endregion

        private void OnGUI()
        {
            EditorGUILayout.Space();
            DrawCurrentApp();
            DrawAppList();
            DrawAppModuleList();
            DrawDeveloperList();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            DrawAppRootSelect();
            DrawAppOrganizationName();
            DrawAddApp();
            DrawAddAppModule();

            //  功能按钮
            DrawFutureButtons();
        }

        #endregion

        #region Mono生命周期

        private void OnDestroy()
        {
            AiukAppsSetting.SetInstanceNull();
        }

        #endregion
    }
}