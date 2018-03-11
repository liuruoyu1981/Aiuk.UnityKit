#region Head

// Author:        liuruoyu1981
// CreateDate:    3/7/2018 4:52:26 PM
// Email:         liuruoyu1981@gmail.com || 35490136@qq.com

#endregion

namespace AiukUnityEditor
{
    /// <summary>
    /// Assets������ǿ���ܡ�
    /// ÿ��ʵ����Ӧһ��Assets�����µı༭���˵���
    /// </summary>
    public interface IAiukEditorAssetsFuction
    {
        /// <summary>
        /// ִ�й��ܡ�
        /// </summary>
        /// <param name="data"></param>
        void Execute(object data = null);

        /// <summary>
        /// ������Ĳ˵����֡�
        /// </summary>
        string MenuTitle { get; }
    }
}
