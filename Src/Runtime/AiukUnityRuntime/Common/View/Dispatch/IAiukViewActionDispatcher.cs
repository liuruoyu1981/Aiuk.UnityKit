 
 

#region Head

// Author:        liuruoyu1981
// CreateDate:    3/5/2018 1:53:03 PM
// Email:         liuruoyu1981@gmail.com

#endregion

namespace AiukUnityRuntime
{
    /// <summary>
    /// ��ͼ��Ϊ�������������
    /// </summary>
    public interface IAiukViewActionDispatcher<T> where T : IAiukViewlement
    {
        /// <summary>
        /// ����һ����ͼ��Ϊ��������
        /// </summary>
        /// <param name="request"></param>
        void IssueRequest(IAiukViewActionRequest<T> request);

        /// <summary>
        /// �͸�����Appʵ��������󶨹�ϵ��
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        IAiukViewActionDispatcher<T> BindingApp(IAiukUnityApp app);

        /// <summary>
        /// ע��һ����Ӧ������Ϊ�������С�
        /// </summary>
        /// <param name="responser"></param>
        /// <returns></returns>
        IAiukViewActionDispatcher<T> InjectResponser(IAiukViewActionResponser<T> responser);

        /// <summary>
        /// �滻һ����ͼ��Ϊ�ĵ�ǰ����
        /// ��API���ڿ��ٻ�Ƥʱʹ�á�
        /// </summary>
        /// <param name="token"></param>
        /// <param name="responser"></param>
        /// <returns></returns>
        IAiukViewActionDispatcher<T> ReplaceResponser(string token, IAiukViewActionResponser<T> responser);
    }
}
