using UnityEngine.EventSystems;

namespace AiukUnityRuntime.Core.DataPipeline
{
    /// <summary>
    /// UI交互数据。
    /// </summary>
    public class AiukUIInteractiveData
    {
        #region 不可变属性

        /// <summary>
        /// 指针数据。
        /// </summary>
        public PointerEventData EventData { get; private set; }

        /// <summary>
        /// 控件事件类型字符串。
        /// </summary>
        public string EventTypeStr { get; private set; }

        #endregion

        public void Init(PointerEventData eventData, string eventTypeStr)
        {
            EventData = eventData;
            EventTypeStr = eventTypeStr;
        }
    }
}

