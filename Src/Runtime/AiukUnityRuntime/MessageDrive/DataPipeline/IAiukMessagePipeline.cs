using AiukUnityRuntime.Core.DataPipeline;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 消息管道接口。
    /// </summary>
    /// <typeparam name="TData">数据管道所承载的数据类型。</typeparam>
    public interface IAiukMessagePipeline<TData> :
        IAiukDataPipeline,
        IAiukDataVisitMediator<TData>,
        IAiukDataProducerMediator<TData>
    {

    }
}
