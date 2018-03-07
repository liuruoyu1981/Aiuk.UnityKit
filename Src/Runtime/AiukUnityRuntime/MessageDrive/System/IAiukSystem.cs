using AiukUnityRuntime.Core.DataPipeline;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 业务系统。
    /// </summary>
    public interface IAiukSystem<TData> : IAiukUpdateExecute, IAiukDataVisitor<TData>
    {
        /// <summary>
        /// 初始化一个业务系统。
        /// </summary>
        /// <param name="visitMediator">业务系统关注数据的数据访问中介者实例。</param>
        void Init(IAiukDataVisitMediator<TData> visitMediator);

        /// <summary>
        /// 系统实例之前的系统。
        /// </summary>
        IAiukSystem<TData> PrevSystem { get; }

        /// <summary>
        /// 系统实例之后的系统。
        /// </summary>
        IAiukSystem<TData> NextSystem { get; }

        /// <summary>
        /// 设置系统实例之前的系统实例。
        /// </summary>
        /// <param name="system"></param>
        /// <param name="app"></param>
        void SetPrev(IAiukSystem<TData> system, IAiukUnityApp app);

        /// <summary>
        /// 设置系统实例之后的系统实例。
        /// </summary>
        /// <param name="system"></param>
        /// <param name="app"></param>
        void SetNext(IAiukSystem<TData> system, IAiukUnityApp app);

        /// <summary>
        /// 新数据到达，进行业务处理。
        /// </summary>
        /// <param name="data"></param>
        void HandleMessage(TData data);

    }
}
