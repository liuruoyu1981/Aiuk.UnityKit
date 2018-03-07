using Aiuk.Common.Utility;

namespace AiukUnityRuntime
{
    public class AiukButtonSystem : AiukAbsSystem<AiukButtonMessage>
    {
        public override void HandleMessage(AiukButtonMessage message)
        {
            AiukDebugUtility.Log("AiukButtonSystem: HandleMessage, this action name is " + message.EventTypeStr);
        }
    }
}

