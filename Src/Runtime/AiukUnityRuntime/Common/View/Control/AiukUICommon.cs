using UnityEngine;

namespace AiukUnityRuntime
{
    /// <summary>
    /// UI控件公共数据结构。
    /// </summary>
    public abstract class AiukUICommon
    {
        /// <summary>
        /// 原生UI控件所附着的游戏对象。
        /// </summary>
        protected GameObject NativeUI { get; private set; }

        /// <summary>
        /// 应用实例。
        /// </summary>
        protected IAiukUnityApp UnityApp { get; private set; }

        public void Init(GameObject nativeUi, IAiukUnityApp unityApp)
        {
            NativeUI = nativeUi;
            UnityApp = unityApp;
        }
    }
}
