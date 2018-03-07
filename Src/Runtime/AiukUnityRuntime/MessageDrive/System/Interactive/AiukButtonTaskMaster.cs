using AiukUnityRuntime.Core.DataPipeline;
using UnityEngine;

namespace AiukUnityRuntime.System.Interactive
{
    /// <summary>
    /// 按钮交互数据处理监工。
    /// </summary>
    public class AiukButtonTaskMaster : IAiukDataVisitor<AiukUIInteractiveData>
    {
        public void Execute(AiukUIInteractiveData data)
        {
            Debug.Log(data.EventTypeStr);
//            data.SetDirty(this);
        }
    }
}


