using System.Collections.Generic;
using UnityEngine;

namespace AiukUnityRuntime.View
{
    /// <summary>
    /// 视图组件。
    /// 由基础UI控件组合而成的可复用的视图构件块。
    /// </summary>
    public abstract class AiukAbsViewComponent
    {
        #region 字段

        /// <summary>
        /// 组件的根对象。
        /// </summary>
        public RectTransform RectRoot { get; private set; }

        /// <summary>
        /// 组件名（业务具象）。
        /// </summary>
        public string Name { get; private set; }

        #endregion

        #region 基础控件

        public List<IAiukButton> Buttons { get; private set; }
        public List<IAiukInputField> InputFields { get; private set; }
        public List<IAiukImage> Images { get; private set; }
        public List<IAiukRawImage> RawImages { get; private set; }
        public List<IAiukSlider> Sliders { get; private set; }

        #endregion

        #region 构造函数

        public AiukAbsViewComponent()
        {
            InitControlList();
        }

        /// <summary>
        /// 从unity静态工厂的对象池中初始化视图组件所持有的基础控件列表
        /// </summary>
        private void InitControlList()
        {
            Images = AiukUnityPool.ImageListPool.Take();
            RawImages = AiukUnityPool.RawImageListPool.Take();
            Buttons = AiukUnityPool.ButtonListPool.Take();
            InputFields = AiukUnityPool.InputFieldListPool.Take();
            Sliders = AiukUnityPool.SliderListPool.Take();
        }

        /// <summary>
        /// 清理视图组件所持有的基础控件列表。
        /// </summary>
        private void CleanControlList()
        {
            Images.Clear();
            RawImages.Clear();
            Buttons.Clear();
            InputFields.Clear();
            Sliders.Clear();
        }

        #endregion

        #region 生命周期

        /// <summary>
        /// 挂载前。
        /// </summary>
        protected virtual void BeforeMount() { }

        /// <summary>
        /// 挂载后。
        /// </summary>
        protected virtual void MountCompleted() { }

        /// <summary>
        /// 创建完毕（只会触发一次）。
        /// </summary>
        protected virtual void OnCreated() { }

        /// <summary>
        /// 激活时（多次触发）。
        /// </summary>
        protected virtual void OnActived() { }

        /// <summary>
        /// 隐藏时（多次触发）。
        /// </summary>
        protected virtual void OnHided() { }

        /// <summary>
        /// 关闭时（只会触发一次）。
        /// </summary>
        protected virtual void OnClosed() { }

        public virtual void Close()
        {
            CleanControlList();
        }

        #endregion


    }
}
