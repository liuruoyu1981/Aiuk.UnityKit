using System;
using UnityEngine;

namespace AiukUnityRuntime.Tween
{
    public class AiukVector4Tween : AiukTween<Vector4>
    {
        private static Vector4 LerpVector4(IAiukTween<Vector4> t, Vector4 start, Vector4 end, float progress) { return Vector4.Lerp(start, end, progress); }
        private static readonly Func<IAiukTween<Vector4>, Vector4, Vector4, float, Vector4> LerpFunc = LerpVector4;

        /// <summary>
        /// Initializes a new Vector4Tween instance.
        /// </summary>
        public AiukVector4Tween() : base(LerpFunc) { }
    }
}
