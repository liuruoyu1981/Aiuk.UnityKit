using Aiuk.Common.PoolCache;

namespace AiukUnityRuntime.System.Tcp
{
    /// <summary>
    /// Tcp通信消息。
    /// </summary>
    public class AiukTcpMessage : IAiukRecycle
    {
        /// <summary>
        /// 消息编号。
        /// </summary>
        public int Id;

        /// <summary>
        /// 消息所属频道编号。
        /// </summary>
        public int ChannelId;

        /// <summary>
        /// 消息体字节数组。
        /// </summary>
        public byte[] Body;

        public AiukTcpMessage Init(int id, int channelId, byte[] body)
        {
            Id = id;
            ChannelId = channelId;
            Body = body;

            return this;
        }

        public virtual void Restore()
        {
        }

        public void Reset()
        {
            throw new global::System.NotImplementedException();
        }
    }
}
