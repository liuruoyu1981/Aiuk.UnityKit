#region Head

// Author:        liuruoyu1981
// CreateDate:    3/7/2018 10:18:05 AM
// Email:         liuruoyu1981@gmail.com || 35490136@qq.com

#endregion

namespace AiukUnityRuntime
{
    /// <summary>
    /// ��Ӧʽ����ģ�Ͳ�������
    /// ������վ������Ϣʵ����������Ӧ����Ӧʽ����ģ�͡�
    /// �ýӿڵľ���ʵ���ɿ���Զ����ɣ������ֶ�ʵ�֡�
    /// </summary>
    public interface IAiukReactDtaModelOperator<TMessage, TModel>
        where TMessage : class
        where TModel : IAiukReactDataModel
    {
        /// <summary>
        /// ����һ����Ϣ�������Ӧ������ģ�͡�
        /// </summary>
        /// <param name="message"></param>
        /// <param name="model"></param>
        void ChangeModel(TMessage message, TModel model);
    }
}
