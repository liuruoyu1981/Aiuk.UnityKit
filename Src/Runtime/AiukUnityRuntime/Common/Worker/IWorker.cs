/// <summary>
/// 劳工。
/// </summary>
public interface IWorker
{
    /// <summary>
    /// 劳工的工作状态。
    /// </summary>
    WorkState WorkState { get; }

    void Work();
}

/// <summary>
/// 可取消工作劳工。
/// </summary>
public interface ICancelWorker : IWorker
{
    void Cancel();
}
