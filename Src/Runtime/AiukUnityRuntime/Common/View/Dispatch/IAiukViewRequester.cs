 
 

#region Head

// Author:        liuruoyu1981
// CreateDate:    3/5/2018 12:51:20 PM
// Email:         liuruoyu1981@gmail.com

#endregion

namespace AiukUnityRuntime
{
    /// <summary>
    /// 视图行为请求者。
    /// </summary>
    public interface IAiukViewRequester
    {
        /// <summary>
        /// 视图Id（唯一）。
        /// </summary>
        string ViewId { get; }
    }
}
