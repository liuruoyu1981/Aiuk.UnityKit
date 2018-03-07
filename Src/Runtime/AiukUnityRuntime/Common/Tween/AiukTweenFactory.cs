using System;
using Aiuk.Common.PoolCache;

namespace AiukUnityRuntime.Tween
{
    /// <summary>
    /// 动画工厂
    /// </summary>
    public static class AiukTweenFactory
    {
        private const int tweenCount = 100;
        private static readonly AiukObjectPool<AiukVector3Tween> v3TweenPool = new AiukObjectPool<AiukVector3Tween>(() => new AiukVector3Tween(), tweenCount);
        private static readonly AiukObjectPool<AiukVector2Tween> v2TweenPool = new AiukObjectPool<AiukVector2Tween>(() => new AiukVector2Tween(), tweenCount);
        private static readonly AiukObjectPool<AiukFloatTween> floatTweenPool = new AiukObjectPool<AiukFloatTween>(() => new AiukFloatTween(), tweenCount);
        private static readonly AiukObjectPool<AiukVector4Tween> v4TweenPool = new AiukObjectPool<AiukVector4Tween>(() => new AiukVector4Tween(), tweenCount);
        private static readonly AiukObjectPool<AiukColorTween> colorTweenPool = new AiukObjectPool<AiukColorTween>(() => new AiukColorTween(), tweenCount);
        private static readonly AiukObjectPool<AiukQuaternionTween> quaTweenPool = new AiukObjectPool<AiukQuaternionTween>(() => new AiukQuaternionTween(), tweenCount);

        /// <summary>
        /// 归还一个动画对象
        /// </summary>
        /// <param name="aiukTween"></param>
        public static void Restore(IAiukTween aiukTween)
        {
            Type type = aiukTween.GetType();
            switch (type.Name)
            {
                case "Vector3Tween":
                    var v3Tween = aiukTween as AiukVector3Tween;
                    v3TweenPool.Restore(v3Tween);
                    break;
                case "Vector4Tween":
                    var v4Tween = aiukTween as AiukVector4Tween;
                    v4TweenPool.Restore(v4Tween);
                    break;
                case "Vector2Tween":
                    var v2Tween = aiukTween as AiukVector2Tween;
                    v2TweenPool.Restore(v2Tween);
                    break;
                case "FloatTween":
                    var floatTween = aiukTween as AiukFloatTween;
                    floatTweenPool.Restore(floatTween);
                    break;
                case "ColorTween":
                    var colorTween = aiukTween as AiukColorTween;
                    colorTweenPool.Restore(colorTween);
                    break;
                case "QuaternionTween":
                    var quaTween = aiukTween as AiukQuaternionTween;
                    quaTweenPool.Restore(quaTween);
                    break;
            }
        }

        public static AiukVector3Tween GetVector3Tween()
        {
            var tween = v3TweenPool.Take();
            return tween;
        }

        public static AiukFloatTween GetFloatTween()
        {
            var tween = floatTweenPool.Take();
            return tween;
        }

        public static AiukVector2Tween GetVector2Tween()
        {
            var tween = v2TweenPool.Take();
            return tween;
        }

        public static AiukVector4Tween GetVector4Tween()
        {
            var tween = v4TweenPool.Take();
            return tween;
        }

        public static AiukColorTween GetColorTween()
        {
            var tween = colorTweenPool.Take();
            return tween;
        }

        public static AiukQuaternionTween GetQuaternionTween()
        {
            var tween = quaTweenPool.Take();
            return tween;
        }

    }
}
