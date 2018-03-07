#region Head

// Author:        liuruoyu1981
// CreateDate:    3/7/2018 10:02:25 AM
// Email:         liuruoyu1981@gmail.com || 35490136@qq.com

#endregion

using System;
using System.Collections.Generic;
using Aiuk.Common.Utility;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 可被观察的byte数据。
    /// </summary>
    public class AiukReactByte : IAiukReactData<byte>
    {
        public byte Data { get; private set; }

        /// <summary>
        /// 数据变化处理委托列表。
        /// </summary>
        private readonly List<Action<byte>> m_DataChangeActions
            = new List<Action<byte>>();

        /// <summary>
        /// 观察数据。
        /// </summary>
        /// <param name="onDataChanged"></param>
        public void Watch(Action<byte> onDataChanged)
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

        public void SetValue(byte newData)
        {
            if (Data == newData) return;

            Data = newData;
            foreach (var changeAction in m_DataChangeActions)
            {
                changeAction(Data);
            }
        }
    }
}
