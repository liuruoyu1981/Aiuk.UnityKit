using Aiuk.Common.PoolCache;

namespace AiukUnityRuntime.Timer
{
    /// <summary>
    /// 计时器工厂（单例）。
    /// </summary>
    public class AiukTimerFactory : AiukAbsMonoSingleton<AiukTimerFactory>, IAiukTimerProvider
    {
        private static IAiukObjectPool<AiukTimer> _TimerPool;

        public static IAiukObjectPool<AiukTimer> TimerPool
        {
            get
            {
                if (_TimerPool != null) return _TimerPool;

                _TimerPool = new AiukObjectPool<AiukTimer>(() => new AiukTimer(), 50);
                return _TimerPool;
            }
        }

        //public static AiukTimer GetOnceTimer(float frequency, Action<IAiukTimer> onTick)
        //{
        //    var timer = TimerPool.Take();
        //    timer
        //        .Frequency(frequency)
        //        .Tick(onTick);

        //    AiukTimerPipeline.Instance.PushData(Instance, timer);
        //    return timer;
        //}
    }
}


