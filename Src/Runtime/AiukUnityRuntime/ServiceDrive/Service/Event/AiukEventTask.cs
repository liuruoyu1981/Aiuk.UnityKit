using System;
using Aiuk.Common.PoolCache;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 事件任务信息。
    /// </summary>
    public class AiukEventTask : IAiukRecycle
    {
        /// <summary>
        /// 事件任务的事件码，描述了事件所属的模块及事件Id。
        /// </summary>
        /// <value>The event code.</value>
        public AiukEventCode EventCode { get; private set; }

        /// <summary>
        /// 事件完成委托。
        /// </summary>
        /// <value>The on compelted.</value>
        public Action OnCompelted { get; private set; }

        /// <summary>
        /// 事件数据。
        /// </summary>
        /// <value>The event data.</value>
        public object EventData { get; private set; }

        public AiukEventTask Init
        (
            AiukEventCode eventCode,
            Action action,
            object data = null
        )
        {
            EventCode = eventCode;
            OnCompelted = action;
            EventData = data;

            return this;
        }

        public void Reset()
        {
        }

        public void Restore()
        {
        }
    }
}
