using System;

/// <summary>
/// 劳工工作特性。
/// 是否允许工作在多线程中。
/// 是否允许取消。
/// </summary>
public class WorkerAttribute : Attribute
{
    /// <summary>
    /// 指示一个劳工类型是否可以在多线程中工作。
    /// </summary>
    public bool WorkInMultiThreadAble { get; private set; }

    /// <inheritdoc />
    /// <summary>
    /// 构建一个劳工工作特性实例。
    /// </summary>
    /// <param name="workInMultiThread">指示一个劳工类型是否可以在多线程中工作。</param>
    public WorkerAttribute(bool workInMultiThread)
    {
        WorkInMultiThreadAble = workInMultiThread;
    }
}
