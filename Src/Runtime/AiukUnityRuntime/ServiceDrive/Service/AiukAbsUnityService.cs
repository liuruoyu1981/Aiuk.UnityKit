using AiukUnityRuntime;

public abstract class AiukAbsUnityService : IAiukUnityService
{
    #region 子类可访问字段

    /// <summary>
    /// 当前应用实体。
    /// </summary>
    protected AiukUnityApp App { get; private set; }

    #endregion

    /// <summary>
    /// 释放目标服务。
    /// </summary>
    public virtual void Dispose() { }

    public virtual void Init(AiukUnityApp unityApp)
    {
        App = unityApp;
        //  观察核心事件

        unityApp.GetService<IAiukEventService>().WatchEvent(
            AiukCoreEventCode.AppHotUpdateCompeted, OnHotUpdateCompleted);
    }

    /// <summary>
    /// 热更新完成处理回调函数。
    /// </summary>
    protected virtual void OnHotUpdateCompleted() { }

    public bool IsReady { get; private set; }
}


