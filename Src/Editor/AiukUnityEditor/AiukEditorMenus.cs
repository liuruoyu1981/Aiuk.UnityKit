using System.Diagnostics;
using Aiuk.Common.Utility;
using UnityEditor;
using UnityEngine;

namespace AiukUnityEditor
{
    /// <summary>
    /// 编辑器菜单集中管理类。
    /// </summary>
    public static class AiukEditorMenus
    {
        private const string MENU_BASE = "Assets/AiukUnityKit/";

        #region AppConstant

        /// <summary>
        /// APP脚手架菜单文字。
        /// </summary>
        private const string APP_SCAFFOLD = MENU_BASE + "App Scaffold";

        #endregion

        #region 快捷

        [MenuItem(MENU_BASE + "快捷/打开沙盒目录")]
        private static void OpenSandbox()
        {
            Process.Start(Application.persistentDataPath);
        }

        [MenuItem(MENU_BASE + "快捷/清空沙盒目录")]
        private static void DeleteSandbox()
        {
            AiukIOUtility.DeleteDirectory(Application.persistentDataPath);
            EditorUtility.DisplayDialog("清空成功", "沙盒目录已成功清空", "确定");
        }

        #endregion

        #region 脚手架窗口

#if AIUK_LANGUAGE_CHINESE
        [MenuItem(MENU_BASE + "应用/脚手架")]
#endif
        public static void ShowScaffoldWindow()
        {
            AiukScaffoldWindow.ShowWindow();
        }

        #endregion

        #region Excel

        



        #endregion


    }
}


