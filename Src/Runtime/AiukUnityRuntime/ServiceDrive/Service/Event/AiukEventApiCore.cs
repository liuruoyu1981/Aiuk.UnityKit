using System;
using System.Collections.Generic;
using Aiuk.Common.Utility;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 事件服务的核心API实现类。
    /// </summary>
    public class AiukEventApiCore
    {
        private readonly Dictionary<string, AiukEventHandleUnit> m_EventHandleUnits
        = new Dictionary<string, AiukEventHandleUnit>();

        private readonly object m_EventLock = new object();

        private Queue<AiukEventTask> m_TaskQueue = new Queue<AiukEventTask>();

        /// <summary>
        /// 触发一个事件。
        /// 该操作会将一个事件任务信息实例加入任务队列，实际执行将稍后处理。
        /// 考虑到线程安全，该操作内部将会做加锁操作。
        /// </summary>
        /// <param name="eventCode">Event code.</param>
        /// <param name="onCompleted">On completed.</param>
        /// <param name="data">Data.</param>
        public void TriggerEvent(AiukEventCode eventCode, Action onCompleted = null,
                                 object data = null)
        {
            lock (m_EventLock)
            {
                var eventTask = AiukEventFactory.EventTaskPool.Take();
                eventTask.Init(eventCode, onCompleted, data);
                m_TaskQueue.Enqueue(eventTask);
            }
        }

        public void ExecuteEvent()
        {
            if (m_TaskQueue.Count == 0) return;

            var taskCount = m_TaskQueue.Count;

            for (int i = 0; i < taskCount; i++)
            {
                var task = m_TaskQueue.Dequeue();
                if (!m_EventHandleUnits.ContainsKey(task.EventCode.EventModuleType))
                {
#if UNITY_EDITOR || DEBUG
                    AiukDebugUtility.LogWarning(string.Format("尝试触发一个没有响应处理器的事件，" +
                        "事件Id为{0}", task.EventCode.EventName));
#endif
                    continue;
                }
                else
                {
                    var unit = m_EventHandleUnits[task.EventCode.EventModuleType];
                    unit.ExecuteEvent(task.EventCode.EventName, task.EventData);
                }

                if (task.OnCompelted != null)
                {
                    task.OnCompelted();
                }

                AiukEventFactory.EventTaskPool.Restore(task);
            }
        }

        #region 观察事件

        public int WatchEvent(AiukEventCode eventCode, Action action,
                               int executeCount = -1)
        {
            if (!m_EventHandleUnits.ContainsKey(eventCode.EventModuleType))
            {
                var newUnit = new AiukEventHandleUnit();
                m_EventHandleUnits.Add(eventCode.EventModuleType, newUnit);
                var handlerId = newUnit.WatchEvent(eventCode.EventName, action, executeCount);
                return handlerId;
            }
            else
            {
                var unit = m_EventHandleUnits[eventCode.EventModuleType];
                var handlerId = unit.WatchEvent(eventCode.EventName, action, executeCount);
                return handlerId;
            }
        }

        public int WatchEvent(AiukEventCode eventCode, Action<object> action,
                               int executeCount = -1)
        {
            if (!m_EventHandleUnits.ContainsKey(eventCode.EventModuleType))
            {
                var newUnit = new AiukEventHandleUnit();
                m_EventHandleUnits.Add(eventCode.EventModuleType, newUnit);
                var handlerId = newUnit.WatchEvent(eventCode.EventName, action, executeCount);
                return handlerId;
            }
            else
            {
                var unit = m_EventHandleUnits[eventCode.EventModuleType];
                var handlerId = unit.WatchEvent(eventCode.EventName, action, executeCount);
                return handlerId;
            }
        }

        #endregion

        #region 移除事件

        /// <summary>
        /// 移除指定事件的事件处理器。
        /// </summary>
        /// <param name="eventCode">Event code.</param>
        public void RemoveEvent(AiukEventCode eventCode)
        {
            if (m_EventHandleUnits.ContainsKey(eventCode.EventModuleType))
            {
#if UNITY_EDITOR || DEBUG
                AiukDebugUtility.LogWarning(
                    string.Format("尝试移除一个当前不存在的事件处理器，事件Id为{0}。",
                                  eventCode.EventName));
#endif
                return;
            }

            var unit = m_EventHandleUnits[eventCode.EventModuleType];
            unit.RemoveEvent(eventCode.EventName);
        }

        #endregion

        public void RemoveSpecifiedHandler(AiukEventCode eventCode, int handlerId)
        {
            if (!m_EventHandleUnits.ContainsKey(eventCode.EventModuleType))
            {
#if UNITY_EDITOR || DEBUG
                AiukDebugUtility.LogWarning(
                    string.Format("尝试移除一个当前不存在的事件处理器，事件Id为{0}。",
                                  eventCode.EventName));
#endif
                return;
            }

            var unit = m_EventHandleUnits[eventCode.EventModuleType];
            unit.RemoveSpecifiedHandler(eventCode.EventName, handlerId);
        }
    }
}
