using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace AiukUnityEditor
{
    /// <summary>
    /// 编辑器下路径目录文件工具。
    /// </summary>
    public static class AiukEditorIOUtility
    {
        /// <summary>
        /// 获得当前编辑器摸下所选择的路径列表。
        /// </summary>
        /// <returns>The select paths.</returns>
        public static List<string> GetSelectPaths()
        {
            var paths = Selection.assetGUIDs.Select(AssetDatabase.GUIDToAssetPath)
                                 .Where(AssetDatabase.IsValidFolder).ToList();
            return paths;
        }

        /// <summary>
        /// 尝试获取当前在编辑器下选择的唯一的目录。
        /// 如果同时选中了多个则会返回空。
        /// </summary>
        /// <returns>The single select dir.</returns>
        public static string GetSingleSelectDir()
        {
            var paths = GetSelectPaths();
            if (paths.Count > 1)
            {
                EditorUtility.DisplayDialog("错误", "不能同时选中多个目录执行该操作！", "知道了");
                return null;
            }

            if (paths.Count == 0)
            {
                EditorUtility.DisplayDialog("错误", "没有选中任何目录！", "知道了");
                return null;
            }

            var dirPath = paths.First();
            return dirPath;
        }

    }
}


