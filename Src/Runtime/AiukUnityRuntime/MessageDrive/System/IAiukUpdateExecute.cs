namespace AiukUnityRuntime
{
    /// <summary>
    /// 帧循环执行接口。
    /// 实现了该接口的对象可以在一个帧循环上下文中重复执行。
    /// </summary>
    public interface IAiukUpdateExecute
    {
        /// <summary>
        /// 执行业务处理。
        /// </summary>
        void Execute();

        /// <summary>
        /// 执行业务处理。
        /// </summary>
        void LateExecute();

        /// <summary>
        /// 执行固定间隔的业务处理。
        /// </summary>
        void FixedExecute();

        /// <summary>
        /// 结束一次执行周期。
        /// </summary>
        void FinishExecute();
    }
}
