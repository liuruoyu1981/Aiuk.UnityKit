using System;

namespace AuikUnityCommon
{
    /// <summary>
    /// IO类型异常。
    /// 1. 路径。
    /// 2. 目录。
    /// 3. 文件。
    /// </summary>
    public class AuikIOException : Exception
    {
        public AuikIOException(string message)
            : base(message)
        {
        }
    }
}
