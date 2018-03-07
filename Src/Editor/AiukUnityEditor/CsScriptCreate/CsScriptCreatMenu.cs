using UnityEditor;

namespace AiukUnityEditor
{
    public static class CsScriptCreatMenu
    {
        /// <summary>
        /// Cs脚本创建窗口的静态实例引用。
        /// </summary>
        private static CsCreateWindow _window;

        [MenuItem("Assets/AiukUnityKit/脚本创建")]
        public static void ShowWindow()
        {
            var targetDir = AiukEditorIOUtility.GetSingleSelectDir();
            if (targetDir == null)
            {
                return;
            }

            CsCreateWindow.ShowWindow(targetDir);
        }
    }
}

