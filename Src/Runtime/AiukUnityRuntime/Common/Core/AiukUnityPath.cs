using System.IO;
using UnityEngine;

namespace AuikUnityCommon
{
    /// <summary>
    /// 静态工具类。
    /// 用于转换在unity环境下的各种路径。
    /// </summary>
    public static class AiukUnityPath
    {
        /// <summary>
        /// 获取给定相对路径（相对于unity的Assets目录）的unity完整路径。
        /// </summary>
        /// <param name="relativePath">相对路径（相对于unity的Assets目录）。</param>
        /// <returns></returns>
        public static string GetUnityFullPath(string relativePath)
        {
            var fullPath = Application.dataPath + "/" + relativePath;
            if (!File.Exists(fullPath) && !Directory.Exists(fullPath))
                throw new AuikIOException("依据传入的相对路径所计算得出的最终路径无法访问，请检查传入路径！");

            return fullPath;
        }

        /// <summary>
        /// 获得当前unity项目的根目录。
        /// </summary>
        /// <returns></returns>
        public static string GetRootPath()
        {
            var assetsPath = Application.dataPath;
            var rootPath = assetsPath.Substring(0, assetsPath.Length - 6);
            return rootPath;
        }

        private static string _AssetsPath;

        /// <summary>
        /// 获得当前unity项目的Assets目录路径（带/）。
        /// </summary>
        /// <returns></returns>
        public static string AssetsPath
        {
            get
            {
                if (_AssetsPath != null) return _AssetsPath;

                _AssetsPath = Application.dataPath + "/";
                return _AssetsPath;
            }
        }
    }
}
