using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace AiukUnityRuntime.Utility
{
    /// <summary>
    /// AssetDatabase资源数据库工具。
    /// 1. 自动判断路径类型、归属。
    /// 2. 加载资源。
    /// </summary>
    public static class AiukAssetDatabaseUtility
    {
        #region 字段

        /// <summary>
        /// 加载资源的方法信息。
        /// </summary>
        private static readonly MethodInfo LoadAssetAtPathMethod;

        /// <summary>
        /// 加载所有资源的方法信息。
        /// </summary>
        private static readonly MethodInfo LoadAllAssetsAtPathMethod;

        /// <summary>
        /// 静态构造函数。
        /// 静态初始化获取AssetDatabase相关函数的函数信息。
        /// </summary>
        static AiukAssetDatabaseUtility()
        {
            var editorAssembly = Assembly.Load("UnityEditor");
            var assetDatabaseType = editorAssembly.GetType("UnityEditor.AssetDatabase");
            var methods = assetDatabaseType.GetMethods();
            LoadAssetAtPathMethod = methods.ToList().Find(m => m.Name == "LoadAssetAtPath");
            LoadAllAssetsAtPathMethod = methods.ToList().Find(m => m.Name == "LoadAllAssetsAtPath");
        }

        #endregion

        #region 公开的工具函数

        public static T LoadAssetAtPath<T>(string path) where T : Object
        {
            var args = new object[] { path, typeof(T) };
            var asset = (T)LoadAssetAtPathMethod.Invoke(null, args);
            return asset;
        }

        public static List<T> LoadAllAssetsAtPath<T>(string path) where T : Object
        {
            var args = new object[] { path };
            var objs = (object[])LoadAllAssetsAtPathMethod.Invoke(null, args);
            var assets = objs.OfType<T>().ToList();
            return assets;
        }

        #endregion


    }
}


