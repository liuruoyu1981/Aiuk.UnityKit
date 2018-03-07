namespace AuikUnityCommon
{
    /// <summary>
    /// 视图结构描述文档（Vhtml）内置标签枚举。
    /// </summary>
    public enum AiukVhtmlTag : byte
    {
        /// <summary>
        /// 默认类型，即不是标签。
        /// </summary>
        None,

        /// <summary>
        /// 容器，盒子。
        /// </summary>
        Div,

        /// <summary>
        /// 单独一行文本。
        /// </summary>
        Span,

        /// <summary>
        /// 多行文本
        /// </summary>
        P,


    }
}

