 
 
#pragma warning disable CS1692

#region Head

// Author:        liuruoyu1981
// CreateDate:    3/5/2018 1:23:57 PM
// Email:         liuruoyu1981@gmail.com

#endregion

namespace AiukUnityRuntime
{
    /// <summary>
    /// ��ͼ��Ϊ����������Ӧ����
    /// һ����Ӧ��ֻ����һ����ͼԪ�ص�һ�ֽ�����Ϊ��
    /// </summary>
    public interface IAiukViewActionResponser<T> where T : IAiukViewlement
    {
        /// <summary>
        /// ����һ����ͼ��Ϊ����
        /// </summary>
        /// <param name="request"></param>
        void ProcessRequest(IAiukViewActionRequest<T> request);

        /// <summary>
        /// ��ʼ����ͼ��Ϊ������Ӧ����
        /// </summary>
        /// <param name="app"></param>
        /// <param name="request"></param>
        void Init(IAiukUnityApp app, IAiukViewActionResponser<T> request);

        /// <summary>
        /// ��ͼ��Ϊ����������ע����ͼId
        /// </summary>
        string ConcernedViewId { get; }

        /// <summary>
        /// ��ͼ��Ϊ����������ע����ͼ��ǰ�Ƿ��Ѿ�������
        /// </summary>
        bool IsConcernedViewClosed { get; set; }
    }
}
