#region Head

// Author:        liuruoyu1981
// CreateDate:    3/6/2018 9:01:45 AM
// Email:         liuruoyu1981@gmail.com

#endregion

using UnityEngine;

namespace AiukUnityRuntime
{
    /// <summary>
    /// ����ͼƬ��
    /// </summary>
    public interface IAiukImage : IAiukViewControl
    {
        /// <summary>
        /// ���û��ȡ�����ͼƬ��Դ����
        /// ��ᵼ�¾������۷��������
        /// </summary>
        string ImageName { get; set; }

        /// <summary>
        /// ���û��ȡ����ĵ�ǰ��ɫ��
        /// </summary>
        Color Color { get; set; }

        /// <summary>
        /// ���þ������ɫ͸���ȡ�
        /// </summary>
        /// <param name="alpha"></param>
        void SetAlpha(float alpha);

        /// <summary>
        /// ����������Ϊԭ����С�ߴ硣
        /// </summary>
        void ToNativeSeize();
    }
}
