using System;

namespace AuikEditor
{
    /// <summary>
    /// unity内建的资源类型。
    /// </summary>
    public class AiukBuiltInAssetType
    {
        public string Name { get; private set; }

        public Type UnityType { get; private set; }

        public string[] Extensions { get; private set; }

        public AiukBuiltInAssetType(string name, Type unityType, params string[] extensions)
        {
            Name = name;
            UnityType = unityType;
            Extensions = extensions;
        }

    }
}
