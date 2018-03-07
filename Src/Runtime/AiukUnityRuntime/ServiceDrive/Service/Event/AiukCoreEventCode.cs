namespace AiukUnityRuntime
{
    /// <summary>
    /// 该类以静态实例的形式提供常用的Unity核心事件码。
    /// </summary>
    public static class AiukCoreEventCode
    {
        #region 测试

        /// <summary>
        /// 用于测试只执行一次的事件。
        /// </summary>
        public static readonly AiukEventCode Test_ExexuteOne
        = AiukEventFactory.GetEventCode("Test", "ExecuteOne");

        /// <summary>
        /// 用于测试无限执行的事件。
        /// </summary>
        public static readonly AiukEventCode Test_ExecuteTen
        = AiukEventFactory.GetEventCode("Test", "ExecuteTen");

        #endregion

        #region App

        /// <summary>
        /// Unity应用初始化完成。
        /// </summary>
        public static readonly AiukEventCode AppInited
        = AiukEventFactory.GetEventCode("App", "Inited");

        /// <summary>
        /// 当在安卓环境下使用APK整包更新时，用于接受APK的下载进度。
        /// </summary>
        public static readonly AiukEventCode AppApkDownProgressUpdate
        = AiukEventFactory.GetEventCode("App", "ApkDownProgressUpdate");

        /// <summary>
        /// Unity应用当前的多语言设置发生改变。
        /// </summary>
        public static readonly AiukEventCode AppIl8nChanged
        = AiukEventFactory.GetEventCode("App", "Il8nChanged");

        /// <summary>
        /// 返回初始化状态。
        /// </summary>
        public static readonly AiukEventCode AppReturnInitState
        = AiukEventFactory.GetEventCode("App", "ReturnInitState");

        /// <summary>
        /// 应用处于新手引导状态时发生了交互行为。
        /// 点击、拖拽、滑动等。
        /// </summary>
        public static readonly AiukEventCode AppHappeninteractAtGuideState
        = AiukEventFactory.GetEventCode("App", "HappeninteractAtGuideState");

        /// <summary>
        /// 热更新已完成。
        /// 各服务实可以观察该事件，用于服务在热更完成更新自身状态。
        /// </summary>
        public static readonly AiukEventCode AppHotUpdateCompeted
        = AiukEventFactory.GetEventCode("App", "HotUpdateCompeted");

        #endregion

        #region 视图

        /// <summary>
        /// Unity应用开始后加载业务逻辑上的第一个视图。
        /// </summary>
        public static readonly AiukEventCode ViewInitFirst
        = AiukEventFactory.GetEventCode("View", "InitFirst");

        /// <summary>
        /// 视图实例被挂载到视图跟对象时触发。
        /// </summary>
        public static readonly AiukEventCode ViewMounted
        = AiukEventFactory.GetEventCode("View", "Mounted");

        /// <summary>
        /// 视图被关闭。
        /// </summary>
        public static readonly AiukEventCode ViewClosed
        = AiukEventFactory.GetEventCode("View", "Closed");

        #endregion

        #region 通信

        /// <summary>
        /// 应用首次建立网络连接。
        /// </summary>
        public static readonly AiukEventCode NetOnFirstConnected
        = AiukEventFactory.GetEventCode("Net", "OnFirstConnected");

        /// <summary>
        /// 应用已重新建立网络连接。
        /// </summary>
        public static readonly AiukEventCode NetReConnected
        = AiukEventFactory.GetEventCode("Net", "ReConnected");

        /// <summary>
        /// 网络连接成功。
        /// </summary>
        public static readonly AiukEventCode NetConnectSucceed
        = AiukEventFactory.GetEventCode("Net", "ConnectSucceed");

        /// <summary>
        /// 尝试重连。
        /// </summary>
        public static readonly AiukEventCode NetTryReConnect
        = AiukEventFactory.GetEventCode("Net", "TryReConnect");

        /// <summary>
        /// 当重连尝试次数到达最大次数时放弃重连。
        /// </summary>
        public static readonly AiukEventCode NetGiveupReConnect
        = AiukEventFactory.GetEventCode("Net", "GiveupReConnect");

        /// <summary>
        /// 有网络消息到达。
        /// </summary>
        public static readonly AiukEventCode NetReceiveMessage
        = AiukEventFactory.GetEventCode("Net", "ReceiveMessage");

        /// <summary>
        /// 有发送消息（客户端请求）加入队列。
        /// </summary>
        public static readonly AiukEventCode NetSendMessageEnqueue
        = AiukEventFactory.GetEventCode("Net", "SendMessageEnqueue");

        /// <summary>
        /// 网络连接超时。
        /// </summary>
        public static readonly AiukEventCode NetConnectTimeOut
        = AiukEventFactory.GetEventCode("Net", "ConnectTimeOut");

        /// <summary>
        /// 发生通信错误。
        /// </summary>
        public static readonly AiukEventCode NetHappenError
        = AiukEventFactory.GetEventCode("Net", "HappenError");

        /// <summary>
        /// 开启网络消息处理。
        /// </summary>
        public static readonly AiukEventCode Net_EnableMessageHandle
        = AiukEventFactory.GetEventCode("Net", "EnableMessageHandle");

        /// <summary>
        /// 暂停网络消息处理，该事件用于在某些资源加载比较耗时，
        /// 后续的网络相关的业务逻辑处理依赖于正在加载的资源的情况。
        /// 当有这种情况发生时，可暂停网络消息处理以等待资源加载完成。
        /// </summary>
        public static readonly AiukEventCode Net_PauseMessageHandle
        = AiukEventFactory.GetEventCode("Net", "PauseMessageHandle");

        #endregion

        #region 按键

        /// <summary>
        /// 回退键被按下。
        /// </summary>
        public static readonly AiukEventCode Key_EscapeDown
        = AiukEventFactory.GetEventCode("Key", "EscapeDown");

        #endregion


        #region 触摸




        #endregion

    }
}



