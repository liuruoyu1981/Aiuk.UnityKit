using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 滚动区域组件。
    /// </summary>
    public class AiukScrollRect : ScrollRect
    {
        private readonly List<Action> m_OnEndDrags = new List<Action>();

        /// <summary>
        /// 添加一个停止拖拽处理委托。
        /// </summary>
        public void AddOnEndDragHandle(Action onEndDrag)
        {
            m_OnEndDrags.Add(onEndDrag);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);

            m_OnEndDrags.ForEach(del => del());
        }
    }
}

