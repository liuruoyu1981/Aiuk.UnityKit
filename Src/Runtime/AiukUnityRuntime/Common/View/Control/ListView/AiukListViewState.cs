namespace AiukUnityRuntime.View
{
    /// <summary>
    /// 滚动列表状态
    /// </summary>
    public enum AiukListViewState : byte
    {
        Normal,

        /// <summary>
        /// 下拉时数据越界。
        /// </summary>
        PollDownOut,

        /// <summary>
        /// 上拉时数据越界。
        /// </summary>
        UpOut,

        /// <summary>
        /// 下拉刷新中，一般用于请求新的数据。
        /// </summary>
        PollDownUpdate,

        /// <summary>
        /// 下拉数据越界时返回顶部。
        /// </summary>
        AddNewData,
    }
}

