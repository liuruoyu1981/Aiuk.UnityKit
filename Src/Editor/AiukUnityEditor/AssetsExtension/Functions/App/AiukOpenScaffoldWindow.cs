#region Head

// Author:        liuruoyu1981
// CreateDate:    3/7/2018 6:10:21 PM
// Email:         liuruoyu1981@gmail.com || 35490136@qq.com

#endregion

using AiukUnityEditor;

namespace AiukUnityRuntime
{
    public class AiukOpenScaffoldWindow : IAiukEditorAssetsFuction
    {
        public void Execute(object data = null)
        {
            AiukScaffoldWindow.ShowWindow();
        }

        public string MenuTitle
        {
            get { return "项目/打开脚手架窗口"; }
        }
    }
}
