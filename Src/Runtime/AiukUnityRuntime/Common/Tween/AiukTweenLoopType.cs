namespace AiukUnityRuntime.Tween
{
    /// <summary>
    /// 动画循环类型
    /// </summary>
    public enum AiukTweenLoopType
    {
        /// <summary>
        /// 执行一次然后停止
        /// </summary>
        Once,

        /// <summary>
        /// 立即回到起点然后重新开始
        /// </summary>
        Restart,

        /// <summary>
        /// 从起点到终点再从终点到起点
        /// </summary>
        Yoyo,
    }
}
