using AiukUnityRuntime.Core.DataPipeline;
using AiukUnityRuntime.System.Interactive;

namespace AiukUnityRuntime
{
    /// <summary>
    /// UI交互处理系统。
    /// </summary>
    public class AiukInteractiveSystem :
        AiukAbsSystem<AiukUIInteractiveData>
    {
        #region 构造函数

        /// <summary>
        /// UI交互数据处理调度监工。
        /// </summary>
        private readonly AiukButtonTaskMaster m_ButtonTaskMaster = new AiukButtonTaskMaster();

        #endregion

        public override void HandleMessage(AiukUIInteractiveData data)
        {
            
        }

        public override void Execute()
        {
            DispatchWoker();
        }

        public override void LateExecute()
        {
        }

        public override void FixedExecute()
        {

        }

        public override void FinishExecute()
        {

        }

        #region UI处理流程

        /// <summary>
        /// 为UI交互处理劳工分派任务。
        /// </summary>
        private void DispatchWoker()
        {
            while (true)
            {
                //switch (data.ProviderType)
                //{
                //    case AiukUIControlType.Default:
                //        break;
                //    case AiukUIControlType.Button:
                //        m_ButtonTaskMaster.Execute(data);
                //        break;
                //    case AiukUIControlType.Toggle:
                //        break;
                //    default:
                //        throw new ArgumentOutOfRangeException();
                //}
            }
        }

        #endregion
    }
}
