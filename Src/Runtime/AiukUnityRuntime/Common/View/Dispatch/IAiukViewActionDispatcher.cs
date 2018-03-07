 
 

#region Head

// Author:        liuruoyu1981
// CreateDate:    3/5/2018 1:53:03 PM
// Email:         liuruoyu1981@gmail.com

#endregion

namespace AiukUnityRuntime
{
    /// <summary>
    /// 视图行为请求处理调度器。
    /// </summary>
    public interface IAiukViewActionDispatcher<T> where T : IAiukViewlement
    {
        /// <summary>
        /// 发布一个视图行为处理请求。
        /// </summary>
        /// <param name="request"></param>
        void IssueRequest(IAiukViewActionRequest<T> request);

        /// <summary>
        /// 和给定的App实体对象建立绑定关系。
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        IAiukViewActionDispatcher<T> BindingApp(IAiukUnityApp app);

        /// <summary>
        /// 注入一个响应器到行为调度器中。
        /// </summary>
        /// <param name="responser"></param>
        /// <returns></returns>
        IAiukViewActionDispatcher<T> InjectResponser(IAiukViewActionResponser<T> responser);

        /// <summary>
        /// 替换一个视图行为的当前处理。
        /// 该API用于快速换皮时使用。
        /// </summary>
        /// <param name="token"></param>
        /// <param name="responser"></param>
        /// <returns></returns>
        IAiukViewActionDispatcher<T> ReplaceResponser(string token, IAiukViewActionResponser<T> responser);
    }
}
