using UnityEngine;

namespace AiukUnityRuntime
{
    /// <summary>
    /// Unity环境IO工具。
    /// </summary>
    public static class AiukUnityIOUtility
    {
        /// <summary>
        /// 传入完整路径计算目标文件的Unity导入路径。
        /// </summary>
        /// <returns>The importer path.</returns>
        /// <param name="fullPath">Full path.</param>
        public static string GetImporterPath(string fullPath)
        {
            var path = "Assets" + fullPath.Replace(Application.dataPath, "");
            return path;
        }

    }
}


