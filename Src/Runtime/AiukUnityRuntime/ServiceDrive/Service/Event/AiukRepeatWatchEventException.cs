using System;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 事件处理器重复添加异常。
    /// </summary>
    public class AiukRepeatWatchEventException : Exception
    {
        public AiukRepeatWatchEventException(string message)
            : base(message)
        {
        }
    }
}

