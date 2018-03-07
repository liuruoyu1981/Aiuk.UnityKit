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
    /// �ɱ��۲��float���ݡ�
    /// ���������ݲ�ͬ��float��Ӧʵ��ÿ�α���ֵ�����׳����ݸı���Ϣ��
    /// </summary>
    public class AiukReactFloat : IAiukReactData<float>
    {
        public float Data { get; private set; }

        /// <summary>
        /// ���ݱ仯����ί���б�
        /// </summary>
        private readonly List<Action<float>> m_DataChangeActions
            = new List<Action<float>>();

        /// <summary>
        /// �۲����ݡ�
        /// </summary>
        /// <param name="onDataChanged"></param>
        public void Watch(Action<float> onDataChanged)
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
