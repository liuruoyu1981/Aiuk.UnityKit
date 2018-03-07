 
 
#pragma warning disable CS1692

#region Head

// Author:        liuruoyu1981
// CreateDate:    3/5/2018 1:23:57 PM
// Email:         liuruoyu1981@gmail.com

#endregion

namespace AiukUnityRuntime
{
    /// <summary>
    /// 视图行为处理请求响应器。
    /// 一个响应器只处理一个视图元素的一种交互行为。
    /// </summary>
    public interface IAiukViewActionResponser<T> where T : IAiukViewlement
    {
        /// <summary>
        /// 处理一个视图行为请求。
        /// </summary>
        /// <param name="request"></param>
        void ProcessRequest(IAiukViewActionRequest<T> request);

        /// <summary>
        /// 初始化视图行为处理响应器。
        /// </summary>
        /// <param name="app"></param>
        /// <param name="request"></param>
        void Init(IAiukUnityApp app, IAiukViewActionResponser<T> request);

        /// <summary>
        /// 视图行为处理器所关注的视图Id
        /// </summary>
        string ConcernedViewId { get; }

        /// <summary>
        /// 视图行为处理器所关注的视图当前是否已经被回收
        /// </summary>
        bool IsConcernedViewClosed { get; set; }
    }
}
