﻿namespace AiukUnityRuntime
{
    /// <summary>
    /// 事件码，用于承载以下数据。
    /// 1. 事件所属的业务模块类型。
    /// 2. 事件的识别码ID。
    /// </summary>
    public class AiukEventCode
    {
        /// <summary>
        /// 事件所属的业务模块类型。
        /// </summary>
        public string EventModuleType { get; private set; }

        /// <summary>
        /// 事件的识别码ID。
        /// </summary>
        public string EventName { get; private set; }

        public AiukEventCode(string moduleType, string Id)
        {
            EventModuleType = moduleType;
            EventName = Id;
        }
    }
}

