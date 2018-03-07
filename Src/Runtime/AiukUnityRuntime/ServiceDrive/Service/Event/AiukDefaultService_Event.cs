using System;
using System.Collections.Generic;
using Aiuk.Common.Utility;
using UnityEngine;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 默认的事件服务。
    /// </summary>
    public class AiukDefaultService_Event : AiukAbsUnityService, IAiukEventService
    {
        private readonly AiukEventApiCore m_ApiCore = new AiukEventApiCore();

        private readonly AiukUnityEventComponent m_EventComponent
        = AiukUnityEventComponent.Instance;

        #region Unity事件操作

        public void WatchUnityEvent(AiukUnityEventType type, Action action, int executeCount = -1)
        {
            m_EventComponent.WatchUnityEvent(type, action, executeCount);
        }

        public void WatchUnityEvent(string type, Action action, int executeCount = -1)
        {
            var enumType = (AiukUnityEventType)Enum.Parse(typeof(AiukUnityEventType), type);
            WatchUnityEvent(enumType, action, executeCount);
        }

        public void RemoveUnityEvent(AiukUnityEventType type, Action action)
        {
            m_EventComponent.RemoveUnityEvent(type, action);
        }

        public void RemoveUnityEvent(string type, Action action)
        {
            var enumType = (AiukUnityEventType)Enum.Parse(typeof(AiukUnityEventType), type);
            RemoveUnityEvent(enumType, action);
        }

        #endregion

        #region 普通事件操作API

        public int WatchEvent(AiukEventCode eventCode, Action action, int executeCount = -1)
        {
            var handlerId = m_ApiCore.WatchEvent(eventCode, action, executeCount);
            return handlerId;
        }

        public int WatchEvent(AiukEventCode eventCode, Action<object> action, int executeCount = -1)
        {
            var handlerId = m_ApiCore.WatchEvent(eventCode, action, executeCount);
            return handlerId;
        }

        public void TriggerEvent(AiukEventCode eventCode, Action onCompelted = null,
                                object data = null)
        {
            m_ApiCore.TriggerEvent(eventCode, onCompelted, data);
        }

        public void RemoveAllHandler(AiukEventCode eventCode)
        {
            m_ApiCore.RemoveEvent(eventCode);
        }

        public void RemoveSpecifiedHandler(AiukEventCode eventCode, int handlerId)
        {
            m_ApiCore.RemoveSpecifiedHandler(eventCode, handlerId);
        }

        #endregion

        /// <summary>
        /// 检测安卓环境下的返回键按下事件。
        /// </summary>
        private void CheckEscapeKeyDown()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TriggerEvent(AiukCoreEventCode.Key_EscapeDown);
            }
        }

        public AiukDefaultService_Event()
        {
            WatchUnityEvent(AiukUnityEventType.FixedUpdate, CheckEscapeKeyDown);
            WatchUnityEvent(AiukUnityEventType.Update, m_ApiCore.ExecuteEvent);
        }

        private class AiukUnityEventComponent : AiukAbsMonoSingleton<AiukUnityEventComponent>
        {
            private readonly Dictionary<AiukUnityEventType, IAiukTallyEventHandlerCaller>
m_EventCallers = new Dictionary<AiukUnityEventType, IAiukTallyEventHandlerCaller>();

            private void InvokeEventCaller(AiukUnityEventType type)
            {
                if (!m_EventCallers.ContainsKey(type)) return;

                m_EventCallers[type].CallEventHanlder();
            }

            #region App生命周期事件

            private void OnApplicationQuit()
            {
                InvokeEventCaller(AiukUnityEventType.AppQuit);
            }

            private void OnApplicationFocus(bool focus)
            {
                InvokeEventCaller(AiukUnityEventType.AppFoucs);
            }

            private void OnApplicationPause(bool pause)
            {
                InvokeEventCaller(AiukUnityEventType.AppPause);
            }

            #endregion

            #region 帧循环事件

            private void FixedUpdate()
            {
                InvokeEventCaller(AiukUnityEventType.FixedUpdate);
            }

            private void Update()
            {
                InvokeEventCaller(AiukUnityEventType.Update);
            }

            private void LateUpdate()
            {
                InvokeEventCaller(AiukUnityEventType.LateUpdate);
            }

            private void OnGUI()
            {
                InvokeEventCaller(AiukUnityEventType.OnGUI);
            }

            #endregion

            #region Unity事件API

            public void WatchUnityEvent(AiukUnityEventType type,
                                               Action action, int executeCount = -1)
            {
                if (!m_EventCallers.ContainsKey(type))
                {
                    var newCaller = new AiukTallyEventHandlerCaller();
                    newCaller.AddHandler(action, executeCount);
                    m_EventCallers.Add(type, newCaller);
                }
                else
                {
                    var caller = m_EventCallers[type];
                    caller.AddHandler(action, executeCount);
                }
            }

            /// <summary>
            /// 移除一个Unity类型的事件处理器。
            /// </summary>
            /// <param name="type">Type.</param>
            /// <param name="action">Action.</param>
            public void RemoveUnityEvent(AiukUnityEventType type, Action action)
            {
                if (!m_EventCallers.ContainsKey(type))
                {
#if UNITY_EDITOR || DEBUG
                    AiukDebugUtility.LogWarning(
                        string.Format("尝试移除一个不存在的Unity事件类型，事件类型为{0}", type));
#endif
                    return;
                }

                var caller = m_EventCallers[type];
                caller.RemoveHandler(action);
            }

            #endregion

        }
    }
}


