using System;

namespace AiukUnityRuntime.Timer
{
    /// <summary>
    /// 计时器基类。
    /// </summary>
    public abstract class AiukAbsTimer : IAiukTimer
    {
        #region 字段

        /// <summary>
        /// 计时器所承载的数据。
        /// </summary>
        public object Data { get; protected set; }

        /// <summary>
        /// 计时器周期委托。
        /// </summary>
        protected Action<IAiukTimer> m_OnTick;

        /// <summary>
        /// 计时器启动委托。
        /// </summary>
        protected Action<IAiukTimer> m_OnStart;

        /// <summary>
        /// 计时器启动委托是否已经被调用过。
        /// </summary>
        protected bool m_OnStartCalled;

        /// <summary>
        /// 计时器暂停委托。
        /// </summary>
        protected Action<IAiukTimer> m_OnPause;

        /// <summary>
        /// 计时器关闭委托。
        /// </summary>
        protected Action<IAiukTimer> m_OnClose;

        /// <summary>
        /// 计时器启动时间戳。
        /// </summary>
        protected DateTime m_StarTime;

        /// <summary>
        /// 计时器关闭时间戳。
        /// </summary>
        protected DateTime m_CloseTime;

        /// <summary>
        /// 计时器运行开关。
        /// </summary>
        protected bool m_RunToggle;

        /// <summary>
        /// 计时器已执行次数。
        /// </summary>
        protected int m_TickCount;

        /// <summary>
        /// 计时器已延迟时间。
        /// </summary>
        protected double m_DeferredTime;

        /// <summary>
        /// 计时器执行频率。
        /// </summary>
        protected float m_Frequency;

        /// <summary>
        /// 计时器的名字，用于调试。
        /// </summary>
        public string Name;

        /// <summary>
        /// 计时器编号。
        /// 自动生成，无法被修改。
        /// </summary>
        public readonly int Id;

        /// <summary>
        /// 剩余可运行次数。
        /// 默认规则为大于0或小于0则继续运行，等于0则停止运行。
        /// </summary>
        protected int m_ReduceCount = -1;

        /// <summary>
        /// 计时器已运行次数
        /// </summary>
        protected int m_RunedCount;

        /// <summary>
        /// 计时器继续运行检查器
        /// </summary>
        protected Func<int, bool> m_ContinueFunc;

        /// <summary>
        /// 延迟启动值，默认值为-1表示无需延时启动。
        /// </summary>
        public float m_DelayStart = -1;

        /// <summary>
        /// 计时器从启动到现在运行的总时间（秒数）。
        /// </summary>
        public double RunTotalTime
        {
            get
            {
                return (DateTime.Now - m_StarTime).TotalSeconds;
            }
        }

        /// <summary>
        /// 计时器当前计时周期内已运行时间
        /// </summary>
        protected double m_TickRunTime;

        /// <summary>
        /// 全局编号。
        /// </summary>
        private static int GloadId;

        public AiukAbsTimer()
        {
            GloadId++;
            Id = GloadId;
        }

        #endregion

        #region 私有函数

        /// <summary>
        /// 默认的计时器可否继续运行判断委托。
        /// 剩余次数大于或者小于零则可继续运行。
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        protected bool DefaultContinueFunc(int num)
        {
            var result = num > 0 || num < 0;
            return result;
        }

        #endregion

        #region 公有函数

        /// <summary>
        /// 重置计时器状态以便重复利用
        /// </summary>
        public virtual void Reset()
        {
            m_OnStart = null;
            m_OnClose = null;
            m_OnPause = null;
            m_OnTick = null;
            Data = null;
            m_ContinueFunc = null;
            m_OnStartCalled = false;
            m_DelayStart = 0f;
            m_ReduceCount = -1;
            m_RunedCount = 0;
            m_Frequency = 0;
            Name = null;
            m_TickCount = 0;
        }

        public abstract IAiukTimer Frequency(float frequency);
        public abstract IAiukTimer Tick(Action<IAiukTimer> onTick);
        public abstract IAiukTimer ContinueChecker(Func<int, bool> checker);
        public abstract IAiukTimer Cookie(object data);
        public abstract IAiukTimer DelayTime(float delay);
        public virtual void Update()
        {
        }

        #endregion

    }
}

