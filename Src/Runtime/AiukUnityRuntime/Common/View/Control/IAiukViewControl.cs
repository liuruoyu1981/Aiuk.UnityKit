using UnityEngine;

#region Head

// Author:        liuruoyu1981
// CreateDate:    3/5/2018 10:44:58 AM
// Email:         liuruoyu1981@gmail.com

#endregion

namespace AiukUnityRuntime
{
    /// <summary>
    /// 视图控件。
    /// </summary>
    public interface IAiukViewControl : IAiukViewlement
    {
        /// <summary>
        /// 和一个应用实体建立绑定关系。
        /// </summary>
        /// <param name="app">应用实体。</param>
        /// <param name="container">视图控件容器。</param>
        void BindingApp(IAiukUnityApp app, IAiukViewControlContainer container);

        /// <summary>
        /// 控件所依附的游戏对象实例。
        /// </summary>
        GameObject GameObject { get; }
    }
}
