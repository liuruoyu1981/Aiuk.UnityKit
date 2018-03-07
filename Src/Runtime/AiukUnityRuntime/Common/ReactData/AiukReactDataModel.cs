#region Head

// Author:        liuruoyu1981
// CreateDate:    3/7/2018 10:11:32 AM
// Email:         liuruoyu1981@gmail.com || 35490136@qq.com

#endregion

using System.Collections.Generic;

namespace AiukUnityRuntime
{
    /// <summary>
    /// ��Ӧʽ����ģ�͡�
    /// ��ʶ�ӿڡ�
    /// ʵ�ָýӿڵĶ������ֶα���ΪIAiukReactData�ľ���ʵ�֡�
    /// </summary>
    public interface IAiukReactDataModel
    {
        /// <summary>
        /// ��������ע����ϢЭ���Id����б�
        /// ÿ���б��е���Ϣ����ʱ��ģ�Ͷ����յ�֪ͨ��
        /// </summary>
        List<int> ConcernedProtoIds { get; }

        /// <summary>
        /// �͸�����Ӧ��ʵ�������󶨡�
        /// </summary>
        /// <param name="app"></param>
        void BingdingApp(IAiukUnityApp app);
    }
}
