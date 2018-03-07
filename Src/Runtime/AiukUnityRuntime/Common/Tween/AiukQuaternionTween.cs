using System;
using UnityEngine;

namespace AiukUnityRuntime.Tween
{
    /// <summary>
    /// Object used to tween Quaternion values.
    /// </summary>
    public class AiukQuaternionTween : AiukTween<Quaternion>
    {
        private static Quaternion LerpQuaternion(IAiukTween<Quaternion> t, Quaternion start, Quaternion end, float progress) { return Quaternion.Lerp(start, end, progress); }
        private static readonly Func<IAiukTween<Quaternion>, Quaternion, Quaternion, float, Quaternion> LerpFunc = LerpQuaternion;

        /// <summary>
        /// Initializes a new QuaternionTween instance.
        /// </summary>
        public AiukQuaternionTween() : base(LerpFunc) { }
    }
}
