#region Head

// Author:        liuruoyu1981
// CreateDate:    3/7/2018 10:00:09 AM
// Email:         liuruoyu1981@gmail.com || 35490136@qq.com

#endregion

using System;
using System.Collections.Generic;
using Aiuk.Common.Utility;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 可被观察的float数据。
    /// 与整型数据不同，float响应实例每次被赋值都会抛出数据改变消息。
    /// </summary>
    public class AiukReactFloat : IAiukReactData<float>
    {
        public float Data { get; private set; }

        /// <summary>
        /// 数据变化处理委托列表。
        /// </summary>
        private readonly List<Action<float>> m_DataChangeActions
            = new List<Action<float>>();

        /// <summary>
        /// 观察数据。
        /// </summary>
        /// <param name="onDataChanged"></param>
        public void Watch(Action<float> onDataChanged)
        {
            if (onDataChanged == null)
            {
                AiukDebugUtility.LogError("不能添加一个空的数据处理委托！");
                return;
            }

            if (m_DataChangeActions.Find(del => del == onDataChanged) != null)
            {
                AiukDebugUtility.LogError
                    (string.Format("不能重复添加数据处理委托,目标委托为{0}！", onDataChanged));
                return;
            }

            m_DataChangeActions.Add(onDataChanged);
        }

        public void SetValue(float newData)
        {
            Data = newData;
            foreach (var changeAction in m_DataChangeActions)
            {
                changeAction(Data);
            }
        }
    }
}
