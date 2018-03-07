#region Head

// Author:        liuruoyu1981
// CreateDate:    3/7/2018 9:36:13 AM
// Email:         liuruoyu1981@gmail.com || 35490136@qq.com

#endregion

using System;

namespace AiukUnityRuntime
{
    /// <summary>
    /// ��Ӧʽ���ݡ�
    /// ʵ���˸����ݵ����ݽṹ���Ա��۲첢��ȡ���ݱ仯���͡�
    /// </summary>
    public interface IAiukReactData<T>
    {
        /// <summary>
        /// ԭ���ݡ�
        /// </summary>
        T Data { get; }

        /// <summary>
        /// �۲�Ŀ�����ݲ��������ݸı�֪ͨ��
        /// </summary>
        /// <param name="onDataChanged"></param>
        void Watch(Action<T> onDataChanged);

        /// <summary>
        /// �޸����ݡ�
        /// </summary>
        /// <param name="newData"></param>
        void SetValue(T newData);
    }
}
