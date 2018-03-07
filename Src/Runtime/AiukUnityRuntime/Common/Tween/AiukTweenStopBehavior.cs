namespace AiukUnityRuntime.Tween
{
    /// <summary>
    /// 手动停止一个动画时可以采取的行为
    /// </summary>
    public enum AiukTweenStopBehavior
    {
        /// <summary>
        /// 不改变当前值
        /// </summary>
        DoNotModify,

        /// <summary>
        /// 使动画立即改变到结束值
        /// </summary>
        Complete
    }
}
