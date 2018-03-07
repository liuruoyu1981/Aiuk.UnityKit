namespace AiukUnityRuntime
{
    /// <summary>
    /// 资源分类接口。
    /// 实现了该接口的对象可以使用一个字符串标识进行分类。
    /// </summary>
    public interface IAiukAssetType
    {
        /// <summary>
        /// 资源分类字符串。
        /// </summary>
        /// <value>The type.</value>
        string Type { get; }

    }
}

