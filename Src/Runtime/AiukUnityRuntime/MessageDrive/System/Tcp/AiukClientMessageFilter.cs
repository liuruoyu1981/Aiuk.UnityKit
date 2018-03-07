using System.Collections.Generic;
using Aiuk.Common.Utility;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 客户端消息过滤器。
    /// </summary>
    public class AiukClientMessageFliter
    {
        /// <summary>
        /// 客户端消息发送状态字典。
        /// </summary>
        private readonly Dictionary<int, AiukClientMessageSendState> m_SendStates =
            new Dictionary<int, AiukClientMessageSendState>();

        /// <summary>
        /// 目标编号协议是否可以被发送。
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public bool CanSend(int messageId)
        {
            if (!m_SendStates.ContainsKey(messageId))
            {
#if UNITY_EDITOR || DEBUG
                AiukDebugUtility.LogError(string.Format("没有对应的客户端消息编号{0}存在！", messageId));
#endif
                return false;
            }

            var state = m_SendStates[messageId];
            return state.CanSend;
        }



    }
}
