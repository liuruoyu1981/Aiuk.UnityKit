using System;

namespace AiukUnityRuntime.Timer
{
    /// <summary>
    /// 计时器。
    /// </summary>
    public interface IAiukTimer
    {
        /// <summary>
        /// 设置频率。
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns></returns>
        IAiukTimer Frequency(float frequency);

        /// <summary>
        /// 设置计时器周期处理委托。
        /// </summary>
        /// <param name="onTick"></param>
        /// <returns></returns>
        IAiukTimer Tick(Action<IAiukTimer> onTick);

        /// <summary>
        /// 设置计时器持续运行条件检查委托。
        /// </summary>
        /// <param name="checker"></param>
        /// <returns></returns>
        IAiukTimer ContinueChecker(Func<int, bool> checker);

        /// <summary>
        /// 设置计时器所承载的数据。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        IAiukTimer Cookie(object data);

        /// <summary>
        /// 设置延时启动值。
        /// </summary>
        /// <param name="delay"></param>
        /// <returns></returns>
        IAiukTimer DelayTime(float delay);

        /// <summary>
        /// 更新计时器的状态。
        /// </summary>
        void Update();
    }
}
