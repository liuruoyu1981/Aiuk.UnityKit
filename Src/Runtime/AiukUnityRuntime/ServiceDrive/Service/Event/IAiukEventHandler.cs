using System;

#region 非泛型

/// <summary>
/// 事件处理器接口。
/// </summary>
public interface IAiukEventHandler
{
    /// <summary>
    /// 处理事件
    /// </summary>
    void HandleEvent();

    /// <summary>
    /// 处理事件委托。
    /// </summary>
    Action EventAction { get; }

    /// <summary>
    /// 全局唯一的事件处理器Id，用于脚本层移除指定的事件处理器。
    /// </summary>
    /// <value>The identifier.</value>
    int Id { get; }

    ///// <summary>
    ///// 初始化事件处理器。
    ///// </summary>
    ///// <param name="bridge"></param>
    //void BindingBridge(AiukEventBridge bridge);

}

#endregion

#region 泛型

/// <summary>
/// 事件处理器接口（泛型数据）
/// </summary>
/// <typeparam name="TData"></typeparam>
public interface IAiukEventHandler<TData>
{
    /// <summary>
    /// 处理事件
    /// </summary>
    /// <param name="eventData"></param>
    void HandleEvent(TData eventData);

    /// <summary>
    /// 事件处理委托。
    /// </summary>
    Action<TData> EventAction { get; }

    /// <summary>
    /// 全局唯一的事件处理器Id，用于脚本层移除指定的事件处理器。
    /// </summary>
    /// <value>The identifier.</value>
    int Id { get; }

    ///// <summary>
    ///// 初始化事件处理器。
    ///// </summary>
    ///// <param name="bridge"></param>
    //void Init(AiukEventBridge<TData> bridge);
}


#endregion


