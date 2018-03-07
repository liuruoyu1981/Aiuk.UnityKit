/***********************************************************************************************
Author：liuruoyu1981
CreateDate: 2017/03/01 11:09:50
Email: 35490136@qq.com
QQCode: 35490136
CreateNote: 
***********************************************************************************************/


/****************************************修改日志***********************************************
1. 修改日期： 修改人： 修改内容：
2. 修改日期： 修改人： 修改内容：
3. 修改日期： 修改人： 修改内容：
4. 修改日期： 修改人： 修改内容：
5. 修改日期： 修改人： 修改内容：
****************************************修改日志***********************************************/


using System;
using UnityEngine;

namespace AiukUnityRuntime.Tween
{
    /// <summary>
    /// 动画对象
    /// </summary>
    public class AiukTween<T> : IAiukTween<T> where T : struct
    {
        /// <summary>
        /// 动画的插值计算委托
        /// </summary>
        private readonly Func<IAiukTween<T>, T, T, float, T> lerpFunc;
        private float mCurrentTime;
        private float mDuration;
        private Func<float, float> scaleFunc;
        private Action<IAiukTween<T>> progressCallback;
        private Action<IAiukTween<T>> completeDelegate;
        private Action<IAiukTween<T>> pauseDelegate;
        private Action<IAiukTween<T>> startDelegate;
        private Action<IAiukTween<T>> resumeDelegate;
        private Action<IAiukTween<T>> checkeOkDelegate;
        private Func<IAiukTween<T>, bool> checkCondition;

        private AiukTweenState state;
        private AiukTweenLoopType _loopTypeType = AiukTweenLoopType.Once;

        private T mStart;
        private T mEnd;
        private T value;

        /// <summary>
        /// 对人友好的动画名
        /// </summary>
        public string FriendNme { get; set; }

        /// <summary>
        /// 剩余执行次数
        /// </summary>
        private int residueCount = -1;

        /// <summary>
        /// 当前已执行次数
        /// </summary>
        private int executeCount;

        /// <summary>
        /// 动画周期已执行次数
        /// </summary>
        private int mTickExexuteCount;

        /// <summary>
        /// 动画周期执行次数
        /// </summary>
        private int mTickCount = -9999;

        /// <summary>
        /// 周期动画回调委托
        /// </summary>
        private Action<IAiukTween<T>> mTickAction;

        /// <summary>
        /// 检查器委托可执行次数
        /// </summary>
        private int checkOkExecuteCount;

        public object Key { get; set; }

        public float MCurrentTime { get { return mCurrentTime; } }

        public float MDuration { get { return mDuration; } }

        public AiukTweenState State { get { return state; } }

        public T MStartValue { get { return mStart; } }

        public T MEndValue { get { return mEnd; } }

        public T CurrentValue { get { return value; } }


        public Renderer Renderer;

        /// <summary>
        /// 获取动画当前的执行进度（0 - 1）
        /// </summary>
        public float CurrentProgress { get; private set; }

        public int ExecuteCount { get { return executeCount; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lerpFunc"></param>
        protected AiukTween(Func<IAiukTween<T>, T, T, float, T> lerpFunc)
        {
            this.lerpFunc = lerpFunc;
            state = AiukTweenState.Stopped;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="duration"></param>
        /// <param name="scaleFunc"></param>
        /// <param name="progress"></param>
        /// <param name="completion"></param>
        public void Init(T start, T end, float duration, Func<float, float> scaleFunc, Action<IAiukTween<T>> progress,
            Action<IAiukTween<T>> completion)
        {
            if (duration <= 0)
            {
                throw new ArgumentException("duration must be greater than 0");
            }
            // ReSharper disable once JoinNullCheckWithUsage
            if (scaleFunc == null)
            {
                throw new ArgumentException("scaleFunc");
            }

            mCurrentTime = 0;
            this.mDuration = duration;
            this.scaleFunc = scaleFunc;
            progressCallback = progress;
            completeDelegate = completion;
            state = AiukTweenState.Start;

            this.mStart = start;
            this.mEnd = end;

            //UpdateValue();
        }


        /// <summary>
        /// 重置动画对象
        /// 该方法负责清理动画实例的状态使动画对象恢复初始值
        /// </summary>
        /// <returns></returns>
        public void Reset()
        {
            FriendNme = null;
            residueCount = -1;
            executeCount = 0;
            mTickExexuteCount = 0;
            mTickCount = -9999;
            mTickAction = null;
            checkOkExecuteCount = 0;
        }

        public void Pause()
        {
            if (state != AiukTweenState.Running) return;

            state = AiukTweenState.Paused;
            if (pauseDelegate != null)
            {
                pauseDelegate(this);
            }
        }

        /// <summary>
        /// 恢复一个动画
        /// 如果动画处于Stop状态则无法恢复
        /// </summary>
        public void Resume()
        {
            if (state != AiukTweenState.Paused) return;

            state = AiukTweenState.Running;
            if (resumeDelegate != null)
            {
                resumeDelegate(this);
            }
        }

        /// <summary>
        /// 反向播放
        /// </summary>
        public void PlayReverse()
        {
            Pause();
            var temp = value;
            var newDuration = CurrentProgress * mDuration;   //  计算出新的反向动画的过渡时间
            mDuration = newDuration;
            mEnd = mStart;
            mStart = temp;
            mCurrentTime = 0f;
            CurrentProgress = 0f;
            Resume();
        }

        public void Stop(AiukTweenStopBehavior stopBehavior)
        {
            if (state == AiukTweenState.Stopped) return;

            state = AiukTweenState.Stopped;
            if (stopBehavior != AiukTweenStopBehavior.Complete) return;

            mCurrentTime = mDuration;
            UpdateValue();
            if (completeDelegate != null)
            {
                completeDelegate(this);
            }
            completeDelegate = null;
        }

        /// <summary>
        /// 更新动画
        /// 如果返回真则说明动画已经停止
        /// </summary>
        /// <param name="elapsedTime"></param>
        /// <returns></returns>
        public bool Update(float elapsedTime)
        {
            if (state == AiukTweenState.Start)
            {
                if (startDelegate != null)
                {
                    startDelegate(this);
                }
                state = AiukTweenState.Running;
            }

            if (checkCondition != null && checkCondition(this) && checkOkExecuteCount > 0 || checkOkExecuteCount < 0)
            {
                checkOkExecuteCount--;
                if (checkeOkDelegate != null)
                {
                    checkeOkDelegate(this);
                }
            }

            if (state == AiukTweenState.Running)
            {
                mCurrentTime += elapsedTime;
                if (mCurrentTime >= mDuration)    //  执行完一次动画
                {
                    residueCount--;                     //  剩余执行次数减一
                    executeCount++;                 //   总执行次数加一
                    mTickExexuteCount++;    //  周期执行次数加一

                    // 判断动画周期执行相关
                    if (mTickExexuteCount == mTickCount)
                    {
                        if (mTickAction != null)
                        {
                            mTickAction(this);
                        }
                        mTickExexuteCount = 0;
                    }

                    switch (_loopTypeType)
                    {
                        case AiukTweenLoopType.Restart:
                            if (residueCount == 0) Stop(AiukTweenStopBehavior.Complete);
                            Restart();
                            break;
                        case AiukTweenLoopType.Yoyo:
                            if (residueCount == 0) Stop(AiukTweenStopBehavior.Complete);
                            Yoyo();
                            break;
                        case AiukTweenLoopType.Once:
                            Stop(AiukTweenStopBehavior.Complete);
                            return true;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                UpdateValue();
                return false;
            }
            var result = state == AiukTweenState.Stopped;
            return result;
        }

        private void Yoyo()
        {
            var temp = mEnd;
            mEnd = mStart;
            mStart = temp;
            mCurrentTime = 0f;
            CurrentProgress = 0f;
        }

        private void Restart()
        {
            value = mStart;
            mCurrentTime = 0f;
            CurrentProgress = 0f;
        }

        private void UpdateValue()
        {
#if UNITY || UNITY_5_3_OR_NEWER
            if (Renderer != null && !Renderer.isVisible) return;
#endif
            CurrentProgress = scaleFunc(mCurrentTime / mDuration);
            value = lerpFunc(this, mStart, mEnd, CurrentProgress);
            if (progressCallback == null) return;

            try
            {
                progressCallback(this);
            }
            catch
            {
                //  捕捉到异常说明动画执行依赖的上下文环境已被破坏，直接关闭动画。
                Stop(AiukTweenStopBehavior.DoNotModify);
            }
        }

        public IAiukTween<T> OnCompleted(Action<IAiukTween<T>> completedDelg)
        {
            completeDelegate = completedDelg;
            return this;
        }

        public IAiukTween<T> OnStart(Action<IAiukTween<T>> startDelg)
        {
            startDelegate = startDelg;
            return this;
        }

        public IAiukTween<T> OnPuase(Action<IAiukTween<T>> pauseDelg)
        {
            pauseDelegate = pauseDelg;
            return this;
        }

        public IAiukTween<T> OnResume(Action<IAiukTween<T>> resumeDelg)
        {
            resumeDelegate = resumeDelg;
            return this;
        }

        public IAiukTween<T> SetEaseType(AiukEaseType esType)
        {
            scaleFunc = AiukTweenManager.scaleFuncDictionary[esType];
            return this;
        }

        public IAiukTween<T> SetLoopType(AiukTweenLoopType loopType)
        {
            _loopTypeType = loopType;
            return this;
        }

        /// <summary>
        /// 设置动画循环次数
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public IAiukTween<T> SetLoopCount(int count)
        {
            residueCount = count;
            return this;
        }

        /// <summary>
        /// 设置动画的周期委托相关数据
        /// </summary>
        /// <param name="tickCount">动画周期次数</param>
        /// <param name="tickAction">周期委托</param>
        /// <returns></returns>
        public IAiukTween<T> SetTick(int tickCount, Action<IAiukTween<T>> tickAction)
        {
            mTickCount = tickCount;
            mTickAction = tickAction;
            return this;
        }

        public IAiukTween<T> SetChecker(Func<IAiukTween<T>, bool> ckCondition, Action<IAiukTween<T>> checkeOk, int count = 1)
        {
            checkCondition = ckCondition;
            checkeOkDelegate = checkeOk;
            checkOkExecuteCount = count;
            return this;
        }


    }
}
