using UnityEngine;


namespace AiukUnityRuntime.View
{
    /// <summary>
    /// 滚动列表子项绘制器，负责滚动列表子项的外观表现。
    /// </summary>
    public interface IListViewItem<TData>
    {
        /// <summary>
        /// 初始化滚动列表子项持有的mono组件。
        /// 用于封装一个列表子项所需操作的所有控件。
        /// </summary>
        void Init(RectTransform rect);

        /// <summary>
        /// 更新子项外观。
        /// </summary>
        void UpdateDraw(TData data);
    }
}