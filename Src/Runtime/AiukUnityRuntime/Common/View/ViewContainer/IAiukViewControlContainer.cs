using UnityEngine;

 
 
#pragma warning disable CS3003

#region Head

// Author:        liuruoyu1981
// CreateDate:    3/5/2018 10:02:02 AM
// Email:         liuruoyu1981@gmail.com

#endregion

namespace AiukUnityRuntime
{
    /// <summary>
    /// ��ͼ�ؼ�������
    /// </summary>
    public interface IAiukViewControlContainer
    {
        #region ��������

        /// <summary>
        /// ��ͼ�����ĸ��ڵ����
        /// </summary>
        RectTransform RectRoot { get; }

        /// <summary>
        /// ��ͼ��������Դ����
        /// </summary>
        string AssetName { get; }

        /// <summary>
        /// ��ͼ�ؼ�����ID��
        /// </summary>
        string ID { get; }

        #endregion

        #region �ؼ���ȡ





        #endregion

        #region �ؼ�������





        #endregion
    }
}
