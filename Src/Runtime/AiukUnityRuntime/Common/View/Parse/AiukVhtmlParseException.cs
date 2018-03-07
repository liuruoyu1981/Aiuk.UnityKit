using System;

namespace AuikUnityCommon
{
    /// <summary>
    /// 视图结构描述文档（Vhtml）解析异常。
    /// </summary>
    public class AiukVhtmlParseException : Exception
    {
        public AiukVhtmlParseException(string message)
            : base(message)
        {
        }
    }
}

