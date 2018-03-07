 
 

#region Head

// Author:        liuruoyu1981
// CreateDate:    3/5/2018 12:41:37 PM
// Email:         liuruoyu1981@gmail.com

#endregion

namespace AiukUnityRuntime
{
    /// <summary>
    /// 行为处理请求。
    /// </summary>
    public interface IAiukActionRequest<TRequester>
    {
        /// <summary>
        /// 请求发起者。
        /// </summary>
        TRequester Requester { get; }

        /// <summary>
        /// 行为唯一Id。
        /// </summary>
        string Token { get; }

        void Init(TRequester requester, string token);
    }
}
