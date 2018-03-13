using UnityEditor;

namespace AiukUnityEditor
{
    /// <summary>
    /// Assets扩展函数静态工具类。
    /// 提供Assets目录相关的编辑器扩展功能方法集合。
    /// </summary>
    public static class AiukAssetsFunctionUtility
    {
        /// <summary>
        /// 显示或聚焦脚本创建窗口。
        /// </summary>
        public static void ShowScriptCreateWindow()
        {
            var targetDir = AiukEditorIOUtility.GetSingleSelectDir();
            if (targetDir == null) return;

            CsCreateWindow.ShowWindow(targetDir);
        }
    }
}

