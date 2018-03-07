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
    /// 精灵图片。
    /// 1. 精灵归属于图集。
    /// </summary>
    public class AiukImage : Image, IAiukImage
    {
        #region 字段

        protected IAiukUnityApp App { get; private set; }

        /// <summary>
        /// 精灵所在的视图控件容器。
        /// </summary>
        protected IAiukViewControlContainer CContainer { get; private set; }

        #endregion

        #region 公开接口

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
