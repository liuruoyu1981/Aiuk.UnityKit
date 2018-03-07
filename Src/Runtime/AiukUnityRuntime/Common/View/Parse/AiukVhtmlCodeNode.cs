using AuikUnityCommon;


namespace AiukUnityRuntime.View
{
    /// <summary>
    /// Vhtml（视图结构描述文档）节点元素代码。
    /// 用于管理一对闭合标签对内的所有代码文本。
    /// </summary>
    public class AiukVhtmlCodeNode
    {
        /// <summary>
        /// 元素标签类型。
        /// </summary>
        public AiukVhtmlTag VhtmlTag { get; private set; }

        /// <summary>
        /// 元素内代码。
        /// </summary>
        public string Code { get; private set; }

    }
}

