using AiukUnityRuntime.Core.DataPipeline;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 数据生产者数据数据操作中介器。
    /// </summary>
    public interface IAiukDataProducerMediator<TData>
    {
        /// <summary>
        /// 注入一个数据到数据管道。
        /// </summary>
        /// <param name="producer">数据生产者。</param>
        /// <param name="message">数据。</param>
        void PushData(IAiukDataProducer<TData> producer, TData message);
    }
}

