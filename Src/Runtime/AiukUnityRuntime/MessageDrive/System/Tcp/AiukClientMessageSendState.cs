namespace AiukUnityRuntime
{
    /// <summary>
    /// 客户都消息发送状态，该类型维护一个对应客户端消息类型的当前发送次数。
    /// 1. 如果是可发送多次的客户端消息，则用于返回true。
    /// 2. 如果是不可发送多次的客户端消息，则在收到对应的服务器答复前返回false，收到后返回true。
    /// </summary>
    public class AiukClientMessageSendState
    {
        /// <summary>
        /// 是否允许发送。
        /// </summary>
        private bool m_SendAble;

        /// <summary>
        /// 对应的消息是否允许多次发送。
        /// </summary>
        private bool m_MultiSend;

        /// <summary>
        /// 收到服务器答复。
        /// </summary>
        public void OnReceiveResp()
        {
            m_SendAble = true;
        }

        public bool CanSend
        {
            get
            {
                return m_MultiSend || m_SendAble;
            }
        }

    }
}
