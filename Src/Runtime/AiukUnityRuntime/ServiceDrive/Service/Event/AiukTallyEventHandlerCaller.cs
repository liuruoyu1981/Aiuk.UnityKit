using System;
using System.Collections.Generic;
using System.Linq;
using Aiuk.Common.Utility;

namespace AiukUnityRuntime
{
    #region 非泛型

    public class AiukTallyEventHandlerCaller : IAiukTallyEventHandlerCaller
    {
        private List<IAiukTallyEventHandler> m_Handlers
        = new List<IAiukTallyEventHandler>();

        public int AddHandler(Action eventAction, int executeCount = -1)
        {
            var existHanlder = m_Handlers.Find(h => h.EventAction == eventAction);
            if (existHanlder != null)
            {
                throw new AiukRepeatWatchEventException
                ("尝试添加一个已经存在的事件处理器！");
            }

            var handler = new AiukTallyEventHandler(executeCount, eventAction);
            m_Handlers.Add(handler);
            return handler.Id;
        }

        public int EventHanlderCount
        {
            get
            {
                return m_Handlers.Count;
            }
        }

        public void CallEventHanlder()
        {
            for (int i = 0; i < m_Handlers.Count; i++)
            {
                var handler = m_Handlers[i];
                handler.HandleEvent();
            }
        }

        public void ClearZero()
        {
            m_Handlers = m_Handlers.Where(h => h.ResidueCount != 0).ToList();
        }

        public void RemoveHandler(Action evenAction)
        {
            var handler = m_Handlers.Find(h => h.EventAction == evenAction);
            if (handler == null) return;

            m_Handlers.Remove(handler);
        }

        public void RemoveHandler(int handlerId)
        {
            var handler = m_Handlers.Find(h => h.Id == handlerId);
            if (handler == null)
            {
#if UNITY_EDITOR || DEBUG
                AiukDebugUtility.LogWarning(
                    string.Format("尝试移除不存在的事件处理器，事件Id为{0}！", handlerId));
#endif
                return;
            }

            m_Handlers.Remove(handler);
        }
    }


    #endregion

    #region 泛型

    public class AiukTallyEventHandlerCaller<TData> : IAiukTallyEventHandlerCaller<TData>
    {
        private List<IAiukTallyEventHandler<TData>> m_Handlers
        = new List<IAiukTallyEventHandler<TData>>();

        public int EventHanlderCount
        {
            get
            {
                return m_Handlers.Count;
            }
        }

        public int AddHandler(Action<TData> eventAction, int executeCount = -1)
        {
            var handler = new AiukTallyEventHandler<TData>(executeCount, eventAction);
            m_Handlers.Add(handler);
            return handler.Id;
        }

        public void CallEventHanlder(TData td)
        {
            for (int i = 0; i < m_Handlers.Count(); i++)
            {
                var handler = m_Handlers[i];
                handler.EventAction(td);
            }
        }

        public void ClearZero()
        {
            m_Handlers = m_Handlers.Where(h => h.ResidueCount != 0).ToList();
        }

        public void RemoveHandler(Action<TData> evenAction)
        {
            var handler = m_Handlers.Find(h => h.EventAction == evenAction);
            if (handler == null) return;

            m_Handlers.Remove(handler);
        }

        public void RemoveHandler(int handlerId)
        {
            var handler = m_Handlers.Find(h => h.Id == handlerId);
            if (handler == null)
            {
#if UNITY_EDITOR || DEBUG
                AiukDebugUtility.LogWarning(
                    string.Format("尝试移除不存在的事件处理器，事件Id为{0}！", handlerId));
#endif
                return;
            }

            m_Handlers.Remove(handler);
        }
    }


    #endregion


}




