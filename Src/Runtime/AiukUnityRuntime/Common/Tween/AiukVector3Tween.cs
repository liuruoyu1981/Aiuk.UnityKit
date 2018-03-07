using System;
using UnityEngine;

namespace AiukUnityRuntime.Tween
{
    public class AiukVector3Tween : AiukTween<Vector3>
    {
        private static Vector3 LerpVector3(IAiukTween<Vector3> t, Vector3 start, Vector3 end, float progress)
        {
            return Vector3.Lerp(start, end, progress);
        }

        private static readonly Func<IAiukTween<Vector3>, Vector3, Vector3, float, Vector3> LerpFunc = LerpVector3;

        /// <summary>
        /// Initializes a new QuaternionTween instance.
        /// </summary>
        public AiukVector3Tween() : base(LerpFunc) { }
    }
}
