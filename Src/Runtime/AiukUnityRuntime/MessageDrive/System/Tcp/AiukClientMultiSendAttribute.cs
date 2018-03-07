using System;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 客户端消息多次发送特性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AiukClientMultiSendAttribute : Attribute
    {
        public bool CanMultiSend { get; private set; }

        public AiukClientMultiSendAttribute(bool canMultiSend)
        {
            CanMultiSend = canMultiSend;
        }
    }
}
