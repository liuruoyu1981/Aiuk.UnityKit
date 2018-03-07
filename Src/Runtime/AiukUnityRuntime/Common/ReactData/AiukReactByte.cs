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
    /// �ɱ��۲��byte���ݡ�
    /// </summary>
    public class AiukReactByte : IAiukReactData<byte>
    {
        public byte Data { get; private set; }

        /// <summary>
        /// ���ݱ仯����ί���б�
        /// </summary>
        private readonly List<Action<byte>> m_DataChangeActions
            = new List<Action<byte>>();

        /// <summary>
        /// �۲����ݡ�
        /// </summary>
        /// <param name="onDataChanged"></param>
        public void Watch(Action<byte> onDataChanged)
        {
            if (onDataChanged == null)
            {
                AiukDebugUtility.LogError("�������һ���յ����ݴ���ί�У�");
                return;
            }

            if (m_DataChangeActions.Find(del => del == onDataChanged) != null)
            {
                AiukDebugUtility.LogError
                    (string.Format("�����ظ�������ݴ���ί��,Ŀ��ί��Ϊ{0}��", onDataChanged));
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
