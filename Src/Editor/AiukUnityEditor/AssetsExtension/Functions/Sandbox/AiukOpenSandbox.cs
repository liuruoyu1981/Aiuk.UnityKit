#region Head

// Author:        liuruoyu1981
// CreateDate:    3/7/2018 5:16:07 PM
// Email:         liuruoyu1981@gmail.com || 35490136@qq.com

#endregion

using System.Diagnostics;
using UnityEngine;

namespace AiukUnityEditor
{
    public class AiukOpenSandbox : IAiukEditorAssetsFuction
    {
        public void Execute(object data = null)
        {
            Process.Start(Application.persistentDataPath);
        }

        public string MenuTitle
        {
            get { return "快捷/打开沙盒"; }
        }
    }
}
