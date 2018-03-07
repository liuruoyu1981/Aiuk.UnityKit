using Aiuk.Common.Utility;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 业务系统基类。
    /// </summary>
    /// <typeparam name="TData">业务系统所关注的数据类型。</typeparam>
    public abstract class AiukAbsSystem<TData> : IAiukSystem<TData>
    {
        /// <summary>
        /// 数据访问中介者。
        /// </summary>
        protected IAiukDataVisitMediator<TData> VisitMediator;

        /// <summary>
        /// 初始化一个业务系统。
        /// </summary>
        /// <param name="visitMediator">业务系统关注数据的数据访问中介者实例。</param>
        public virtual void Init(IAiukDataVisitMediator<TData> visitMediator)
        {
            VisitMediator = visitMediator;
        }

        public IAiukSystem<TData> PrevSystem { get; private set; }
        public IAiukSystem<TData> NextSystem { get; private set; }
        public void SetPrev(IAiukSystem<TData> system, IAiukUnityApp app)
        {
            if (app == null) return;

            PrevSystem = system;
        }

        public void SetNext(IAiukSystem<TData> system, IAiukUnityApp app)
        {
            if (app == null) return;

            NextSystem = system;
        }

        public abstract void HandleMessage(TData data);

        /// <summary>
        /// 执行业务处理。
        /// </summary>
        public virtual void Execute()
        {
#if UNITY_EDITOR || DEBUG
            AiukDebugUtility.Log(GetType().Name + "_Execute!");
#endif
        }

        /// <summary>
        /// 执行业务处理。
        /// </summary>
        public virtual void LateExecute()
        {
#if UNITY_EDITOR || DEBUG
            AiukDebugUtility.Log(GetType().Name + "_LateExecute!");
#endif
        }

        /// <summary>
        /// 执行固定间隔的业务处理。
        /// </summary>
        public virtual void FixedExecute()
        {
#if UNITY_EDITOR || DEBUG
            AiukDebugUtility.Log(GetType().Name + "_FixedExecute!");
#endif
        }

        /// <summary>
        /// 结束一次执行周期。
        /// </summary>
        public virtual void FinishExecute()
        {
#if UNITY_EDITOR || DEBUG
            AiukDebugUtility.Log(GetType().Name + "_FinishExecute!");
#endif
        }
    }
}

