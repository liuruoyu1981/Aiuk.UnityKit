#region Head

// Author:        liuruoyu1981
// CreateDate:    3/6/2018 9:02:07 AM
// Email:         liuruoyu1981@gmail.com

#endregion

using UnityEngine;
using UnityEngine.UI;

namespace AiukUnityRuntime
{
    /// <summary>
    /// ����ͼƬ��
    /// 1. ���������ͼ����
    /// </summary>
    public class AiukImage : Image, IAiukImage
    {
        #region �ֶ�

        protected IAiukUnityApp App { get; private set; }

        /// <summary>
        /// �������ڵ���ͼ�ؼ�������
        /// </summary>
        protected IAiukViewControlContainer CContainer { get; private set; }

        #endregion

        #region �����ӿ�

        public string ControlContainerId { get; private set; }
        public void BindingApp(IAiukUnityApp app, IAiukViewControlContainer container)
        {
            App = app;
            CContainer = container;
            ControlContainerId = CContainer.ID;
        }

        public GameObject GameObject
        {
            get { return gameObject; }
        }

        public string ImageName { get; set; }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public void SetAlpha(float alpha)
        {
            var r = color.r;
            var g = color.g;
            var b = color.b;
            var newColor = new Color(r, g, b, alpha);
            color = newColor;
        }

        public void ToNativeSeize()
        {
            SetNativeSize();
        }

        #endregion
    }
}
