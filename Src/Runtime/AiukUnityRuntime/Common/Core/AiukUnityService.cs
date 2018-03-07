
using Aiuk.Common.Service;

namespace AiukUnityRuntime
{
    /// <summary>
    /// unity底层服务组件。
    /// </summary>
    public interface IAiukUnityService : IAiukService
    {
        /// <summary>
        /// 初始化一个底层服务。
        /// </summary>
        /// <param name="unityApp">unity应用实例。</param>
        void Init(AiukUnityApp unityApp);

        /// <summary>
        /// 检查一个底层服务是否已准备就绪。
        /// </summary>
        bool IsReady { get; }
    }
}

