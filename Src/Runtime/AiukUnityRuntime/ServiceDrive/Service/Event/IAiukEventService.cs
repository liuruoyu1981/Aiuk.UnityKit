using System;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 事件服务。
    /// 提供事件触发和事件观察的API接口。
    /// </summary>
    public interface IAiukEventService : IAiukUnityService
    {
        #region Unity事件操作

        /// <summary>
        /// 观察一个Unity事件。
        /// </summary>
        /// <param name="type">Type.</param>
        /// <param name="action">Action.</param>
        /// <param name="executeCount">Execute count.</param>
        void WatchUnityEvent(AiukUnityEventType type, Action action, int executeCount = -1);

        /// <summary>
        /// 观察一个Unity事件，该方法用于提供给Ts脚本层操作。
        /// </summary>
        /// <param name="type">Type.</param>
        /// <param name="action">Action.</param>
        /// <param name="executeCount">Execute count.</param>
        void WatchUnityEvent(string type, Action action, int executeCount = -1);

        /// <summary>
        /// 移除一个Unity事件。
        /// </summary>
        /// <param name="type">Type.</param>
        /// <param name="action">Action.</param>
        void RemoveUnityEvent(AiukUnityEventType type, Action action);

        /// <summary>
        /// 移除一个Unity事件，该方法用于提供给Ts脚本层操作。
        /// </summary>
        /// <param name="type">Type.</param>
        /// <param name="action">Action.</param>
        void RemoveUnityEvent(string type, Action action);

        #endregion

        #region 普通事件操作API

        /// <summary>
        /// 观察一个无需数据的普通事件并返回对应事件处理器的身份Id。
        /// </summary>
        /// <param name="eventCode">Event code.</param>
        /// <param name="action">Action.</param>
        /// <param name="executeCount">Execute count.</param>
        int WatchEvent(AiukEventCode eventCode, Action action, int executeCount = -1);

        /// <summary>
        /// 观察一个需要数据的普通事件并返回对应事件处理器的身份Id。
        /// </summary>
        /// <param name="eventCode">Event code.</param>
        /// <param name="action">Action.</param>
        /// <param name="executeCount">Execute count.</param>
        int WatchEvent(AiukEventCode eventCode, Action<object> action, int executeCount = -1);

        /// <summary>
        /// 触发一个普通事件。
        /// </summary>
        /// <param name="eventCode">Event code.</param>
        /// <param name="onCompelted">On compelted.</param>
        /// <param name="data">Data.</param>
        void TriggerEvent(AiukEventCode eventCode, Action onCompelted = null,
                               object data = null);

        /// <summary>
        /// 移除一个普通事件相关的所有事件处理器。
        /// </summary>
        /// <param name="eventCode">Event code.</param>
        void RemoveAllHandler(AiukEventCode eventCode);

        /// <summary>
        /// 移除一个指定的事件处理器。
        /// </summary>
        /// <param name="eventCode">Event code.</param>
        /// <param name="handlerId">Handler identifier.</param>
        void RemoveSpecifiedHandler(AiukEventCode eventCode, int handlerId);

        #endregion
    }
}







