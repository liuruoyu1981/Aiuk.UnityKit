using System;
using UnityEngine;

namespace AiukUnityRuntime.Tween
{
    /// <summary>
    /// 使用Vector2的动画
    /// </summary>
    public class AiukVector2Tween : AiukTween<Vector2>
    {
        private static Vector2 LerpVector2(IAiukTween<Vector2> t, Vector2 start, Vector2 end, float progress) { return Vector2.Lerp(start, end, progress); }
        private static readonly Func<IAiukTween<Vector2>, Vector2, Vector2, float, Vector2> LerpFunc = LerpVector2;

        /// <summary>
        /// 初始化一个Vector2动画实例
        /// </summary>
        public AiukVector2Tween() : base(LerpFunc) { }
    }
}
