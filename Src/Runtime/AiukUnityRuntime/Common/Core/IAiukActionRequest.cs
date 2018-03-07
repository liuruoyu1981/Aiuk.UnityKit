 
 

#region Head

// Author:        liuruoyu1981
// CreateDate:    3/5/2018 12:41:37 PM
// Email:         liuruoyu1981@gmail.com

#endregion

namespace AiukUnityRuntime
{
    /// <summary>
    /// ��Ϊ��������
    /// </summary>
    public interface IAiukActionRequest<TRequester>
    {
        /// <summary>
        /// �������ߡ�
        /// </summary>
        TRequester Requester { get; }

        /// <summary>
        /// ��ΪΨһId��
        /// </summary>
        string Token { get; }

        void Init(TRequester requester, string token);
    }
}
