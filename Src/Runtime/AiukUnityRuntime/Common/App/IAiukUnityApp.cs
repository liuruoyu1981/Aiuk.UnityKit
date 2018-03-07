using System;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 应用实体接口，程序的抽象表示。
    /// </summary>
    public interface IAiukUnityApp : IDisposable
    {
        T GetService<T>() where T : class, IAiukUnityService;


    }
}
