namespace AiukUnityRuntime
{
    /// <summary>
    /// 数据消费者中介器接口。
    /// </summary>
    public interface IAiukDataVisitMediator<TData>
    {
        void BindConsumer(IAiukSystem<TData> system);
    }
}

