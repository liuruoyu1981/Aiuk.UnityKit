using System.Collections.Generic;
using Aiuk.Common.Utility;
using AiukUnityRuntime.Core.DataPipeline;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 消息管道。
    /// 1. 负责关联消息生产者和消费者。
    /// 2. 对消息进行缓存、过滤。
    /// </summary>
    public class AiukMessagePipeline<TMessage>
        : IAiukMessagePipeline<TMessage>
    //    where TMessage : IAiukPassiveData<TMessage>
    {
        #region 数据及管道

        /// <summary>
        /// 持久化数据，如果当前管道开启了数据持久化则所有通过过滤器的数据都会被持久化到本地。
        /// </summary>
        private readonly List<TMessage> m_PersistentDatas = new List<TMessage>();

        /// <summary>
        /// 和目标数据相关的业务处理系统。
        /// </summary>
        private readonly List<IAiukSystem<TMessage>> m_Systems = new List<IAiukSystem<TMessage>>();

        /// <summary>
        /// 注入一个数据到数据管道。
        /// </summary>
        /// <param name="producer">消息生产者。</param>
        /// <param name="message">消息。</param>
        public void PushData(IAiukDataProducer<TMessage> producer, TMessage message)
        {
#if UNITY_EDITOR || DEBUG

            if (producer == null)
            {
                AiukDebugUtility.LogError("没有合法的数据提供者，不允许置入数据！");
                return;
            }
#endif

            if (m_Systems.Count == 0) return;

            //  调用第一个系统实例的消息处理方法，并由该系统启动可能存在的系统处理链。
            m_Systems[0].HandleMessage(message);
        }

        #endregion

        public void BindConsumer(IAiukSystem<TMessage> system)
        {
            m_Systems.Add(system);
        }

        public void CleanAndReset()
        {
        }
    }
}
