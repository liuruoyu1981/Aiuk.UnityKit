#region Head

// Author:        liuruoyu1981
// CreateDate:    3/7/2018 4:52:26 PM
// Email:         liuruoyu1981@gmail.com || 35490136@qq.com

#endregion

namespace AiukUnityEditor
{
    /// <summary>
    /// Assets窗口增强功能。
    /// 每个实例对应一个Assets窗口下的编辑器菜单。
    /// </summary>
    public interface IAiukEditorAssetsFuction
    {
        /// <summary>
        /// 执行功能。
        /// </summary>
        /// <param name="data"></param>
        void Execute(object data = null);

        /// <summary>
        /// 功能项的菜单文字。
        /// </summary>
        string MenuTitle { get; }
    }
}
