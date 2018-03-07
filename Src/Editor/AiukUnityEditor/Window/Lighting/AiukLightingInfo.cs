using System.IO;
using UnityEngine;


namespace AuikEditor
{
    /// <summary>
    /// 闪电窗口搜索功能页所使用的资源数据。
    /// </summary>
    public class AiukLightingInfo
    {
        /// <summary>
        /// 资源的上级路径。
        /// </summary>
        public string ParentPath { get; private set; }

        /// <summary>
        /// 资源的完整路径。
        /// </summary>
        public string FullPath { get; private set; }

        /// <summary>
        /// 资源名（带扩展名）
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool StripExtension { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int PenaltyScore { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Object[] UnityObjects { get; set; }

        public AiukLightingInfo(string fullPath)
        {
            FullPath = fullPath;
            var lastSlash = FullPath.LastIndexOf('/');
            ParentPath = lastSlash < 0 ? "" : FullPath.Substring(0, lastSlash);
            Name = lastSlash < 0 ? FullPath : FullPath.Substring(lastSlash + 1);
            StripExtension = true;
        }

        public AiukLightingInfo(string parentPath, string name, params Object[] unityObjects)
        {
            ParentPath = parentPath;
            Name = name;
            UnityObjects = unityObjects;
            PenaltyScore = -1;
        }

        /// <summary>
        /// 如果StripExtension为真则返回不带扩展名的资源名，
        /// 如果StripExtension为假则返回带扩展名的资源名。
        /// </summary>
        private string NameWithoutExtension
        {
            get { return StripExtension ? Path.GetFileNameWithoutExtension(FullPath) : Name; }
        }


    }

}

