#region Head

// Author:        liuruoyu1981
// CreateDate:    3/7/2018 6:06:46 PM
// Email:         liuruoyu1981@gmail.com || 35490136@qq.com

#endregion

using Aiuk.Common.Utility;
using UnityEditor;
using UnityEngine;

namespace AiukUnityEditor
{
    public class AiukCleanSandbox : IAiukEditorAssetsFuction
    {
        public void Execute(object data = null)
        {
            AiukIOUtility.DeleteDirectory(Application.persistentDataPath);
            EditorUtility.DisplayDialog("清空成功", "沙盒目录已成功清空", "确定");
        }

        public string MenuTitle
        {
            get { return "快捷/清空沙盒"; }
        }
    }
}
