using System.Collections.Generic;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 消息编码解码器。
    /// </summary>
    public interface IAiukUnityEnCoder
    {
        /// <summary>
        /// 网络消息编码
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type">服务器类型用于填写第一级协议中字段</param>
        /// <returns></returns>
        byte[] Encode<T>(T message, int type = 5);

        /// <summary>
        /// 网络消息编码
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="message"></param>
        /// <param name="type">服务器类型用于填写第一级协议中字段</param>
        /// <returns></returns>
        byte[] Encode(string typeName, object message, int type = 5);

        /// <summary>
        /// 网络消息解码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        byte[] Decode(ref List<byte> data);
    }
}

