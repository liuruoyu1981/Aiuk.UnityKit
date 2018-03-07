using System;
using UnityEngine.UI;

namespace AiukUnityRuntime.View
{
    public interface IAiukButton : IAiukViewControl
    {
        /// <summary>
        /// 为按钮的一个指定交互行为绑定一个处理委托。
        /// 该处理委托可以是C#原生委托也可以是Ts函数包装委托。
        /// </summary>
        /// <param name="type">按钮交互行为类型。</param>
        /// <param name="action">按钮交互行为处理委托。</param>
        void BindingAction(AiukButtonEventType type, Action action);

        /// <summary>
        /// 为按钮绑定一个滚动区域组件。
        /// 这将会使按钮每次被拖动时都回调滚动组件的OnDrag方法。
        /// </summary>
        /// <param name="scrollRect"></param>
        void BindingScrollRect(ScrollRect scrollRect);
    }
}
