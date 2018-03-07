using System.Collections.Generic;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 客户端消息可发送状态初始化器。
    /// </summary>
    public interface IAiukClientMessageSendStateInitiator
    {
        /// <summary>
        /// 获得客户端消息可发送状态字典。
        /// </summary>
        /// <returns></returns>
        Dictionary<int, AiukClientMessageSendState> GetClienSendStates();
    }
}

