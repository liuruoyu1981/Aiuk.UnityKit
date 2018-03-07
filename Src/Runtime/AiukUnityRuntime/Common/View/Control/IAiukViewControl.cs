using UnityEngine;

#region Head

// Author:        liuruoyu1981
// CreateDate:    3/5/2018 10:44:58 AM
// Email:         liuruoyu1981@gmail.com

#endregion

namespace AiukUnityRuntime
{
    /// <summary>
    /// ��ͼ�ؼ���
    /// </summary>
    public interface IAiukViewControl : IAiukViewlement
    {
        /// <summary>
        /// ��һ��Ӧ��ʵ�彨���󶨹�ϵ��
        /// </summary>
        /// <param name="app">Ӧ��ʵ�塣</param>
        /// <param name="container">��ͼ�ؼ�������</param>
        void BindingApp(IAiukUnityApp app, IAiukViewControlContainer container);

        /// <summary>
        /// �ؼ�����������Ϸ����ʵ����
        /// </summary>
        GameObject GameObject { get; }
    }
}
