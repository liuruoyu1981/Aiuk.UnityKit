using System;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 资源数据加载失败异常。
    /// </summary>
    public class AiukAssetInfoLoadFailException : Exception
    {
        public AiukAssetInfoLoadFailException(string message)
            : base(message)
        {

        }
    }
}


