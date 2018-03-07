#region Head

// Author:        liuruoyu1981
// CreateDate:    3/7/2018 9:36:13 AM
// Email:         liuruoyu1981@gmail.com || 35490136@qq.com

#endregion

using System;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 响应式数据。
    /// 实现了该数据的数据结构可以被观察并获取数据变化推送。
    /// </summary>
    public interface IAiukReactData<T>
    {
        /// <summary>
        /// 原数据。
        /// </summary>
        T Data { get; }

        /// <summary>
        /// 观察目标数据并接收数据改变通知。
        /// </summary>
        /// <param name="onDataChanged"></param>
        void Watch(Action<T> onDataChanged);

        /// <summary>
        /// 修改数据。
        /// </summary>
        /// <param name="newData"></param>
        void SetValue(T newData);
    }
}
