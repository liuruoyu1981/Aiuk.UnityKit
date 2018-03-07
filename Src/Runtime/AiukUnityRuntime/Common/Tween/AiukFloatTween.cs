using System;

namespace AiukUnityRuntime.Tween
{
    /// <summary>
    /// 浮点动画
    /// </summary>
    public class AiukFloatTween : AiukTween<float>
    {
        private static float LerpFloat(IAiukTween<float> t, float start, float end, float progress) { return start + (end - start) * progress; }
        private static readonly Func<IAiukTween<float>, float, float, float, float> LerpFunc = LerpFloat;

        /// <summary>
        /// 初始化一个浮点动画实例.
        /// </summary>
        public AiukFloatTween() : base(LerpFunc) { }
    }
}
