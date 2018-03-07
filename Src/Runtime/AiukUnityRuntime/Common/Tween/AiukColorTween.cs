using System;
using UnityEngine;

namespace AiukUnityRuntime.Tween
{
    public class AiukColorTween : AiukTween<Color>
    {
        private static Color LerpColor(IAiukTween<Color> t, Color start, Color end, float progress) { return Color.Lerp(start, end, progress); }
        private static readonly Func<IAiukTween<Color>, Color, Color, float, Color> LerpFunc = LerpColor;

        /// <summary>
        /// Initializes a new ColorTween instance.
        /// </summary>
        public AiukColorTween() : base(LerpFunc) { }
    }
}
