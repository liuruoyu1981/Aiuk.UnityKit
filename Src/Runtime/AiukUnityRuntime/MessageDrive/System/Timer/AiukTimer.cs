using System;
using Aiuk.Common.Utility;
using UnityEngine;

namespace AiukUnityRuntime.Timer
{
    /// <summary>
    /// 计时器。
    /// </summary>
    public class AiukTimer : AiukAbsTimer
    {
        #region 构造函数

        public AiukTimer() { }

        public AiukTimer(float frequency, Action<IAiukTimer> onTick)
        {
            m_Frequency = frequency;
            m_OnTick = onTick;
        }

        #endregion

        #region 链式

        /// <summary>
        /// 设置运行次数。
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public IAiukTimer RunCount(int num)
        {
            m_ReduceCount = num;
            return this;
        }

        /// <summary>
        /// 设置频率。
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns></returns>
        public override IAiukTimer Frequency(float frequency)
        {
            m_Frequency = frequency;
            return this;
        }

        /// <summary>
        /// 设置计时器周期处理委托。
        /// </summary>
        /// <param name="onTick"></param>
        /// <returns></returns>
        public override IAiukTimer Tick(Action<IAiukTimer> onTick)
        {
            m_OnTick = onTick;
            return this;
        }

        /// <summary>
        /// 设置计时器持续运行条件检查委托。
        /// </summary>
        /// <param name="checker"></param>
        /// <returns></returns>
        public override IAiukTimer ContinueChecker(Func<int, bool> checker)
        {
            m_ContinueFunc = checker;
            return this;
        }

        /// <summary>
        /// 设置计时器所承载的数据。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override IAiukTimer Cookie(object data)
        {
            Data = data;
            return this;
        }

        /// <summary>
        /// 设置延时启动值。
        /// </summary>
        /// <param name="delay"></param>
        /// <returns></returns>
        public override IAiukTimer DelayTime(float delay)
        {
            m_DelayStart = delay;
            return this;
        }

        /// <summary>
        /// 设置启动委托。
        /// </summary>
        /// <param name="onStart"></param>
        /// <returns></returns>
        public IAiukTimer OnStart(Action<IAiukTimer> onStart)
        {
            m_OnStart = onStart;
            return this;
        }

        /// <summary>
        /// 设置关闭委托。
        /// </summary>
        /// <param name="onClose"></param>
        /// <returns></returns>
        public IAiukTimer OnClose(Action<IAiukTimer> onClose)
        {
            m_OnClose = onClose;
            return this;
        }

        /// <summary>
        /// 设置暂停委托。
        /// </summary>
        /// <param name="onPause"></param>
        /// <returns></returns>
        public IAiukTimer OnPause(Action<IAiukTimer> onPause)
        {
            m_OnPause = onPause;
            return this;
        }

        #endregion

        #region 公有函数

        public override void Update()
        {
            if (!m_RunToggle) return;
            if (m_DelayStart > 0)
            {
                DelayStartUpdate();
                return;
            }

            TryInvokeOnStart();

            m_TickRunTime += Time.deltaTime;
            if (m_TickRunTime < m_Frequency) return;

            m_TickCount++;
            m_ReduceCount--;
            m_TickRunTime = 0f;
            if (m_OnTick != null)
            {
                m_OnTick(this);
            }
        }

        public void LateUpdate()
        {
            if (m_ContinueFunc == null) return;

            var result = m_ContinueFunc(m_ReduceCount);
            if (!result)
            {
                Close();
            }
        }

        public void Close()
        {
            if (!m_RunToggle) return;

#if UNITY_EDITOR || DEBUG
            var runAtStopTime = (DateTime.Now - m_StarTime).TotalSeconds;
            AiukDebugUtility.Log(string.Format("编号为{0}的计时器总共运行{1}秒，已停止！"
                , Id, runAtStopTime));
#endif

            m_RunToggle = false;
            m_CloseTime = DateTime.Now;
            if (m_OnClose != null)
            {
                m_OnClose(this);
            }
        }

        /// <summary>
        /// 启动计时器。
        /// </summary>
        /// <returns></returns>
        public IAiukTimer Start()
        {
            m_RunToggle = true;
            m_StarTime = DateTime.Now;
            return this;
        }

        /// <summary>
        /// 恢复计时器运行。
        /// </summary>
        /// <returns></returns>
        public IAiukTimer Resume()
        {
            m_RunToggle = true;
            return this;
        }

        /// <summary>
        /// 暂停计时器运行。
        /// </summary>
        /// <returns></returns>
        public IAiukTimer Pause()
        {
            m_RunToggle = false;
            if (m_OnPause != null)
            {
                m_OnPause(this);
            }

            return this;
        }

        #endregion

        #region 私有函数

        private void DelayStartUpdate()
        {
            m_DeferredTime = m_DeferredTime + Time.deltaTime;

            if (!(m_DeferredTime >= m_DelayStart)) return;

            m_DelayStart = -1;
            TryInvokeOnStart();
        }

        private void TryInvokeOnStart()
        {
            if (m_OnStartCalled || m_OnStart == null) return;

            m_OnStart(this);
            m_OnStartCalled = true;
        }

        #endregion

    }
}

