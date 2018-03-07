namespace AiukUnityRuntime
{
    /// <summary>
    /// 服务测试工具。
    /// 提供用于各种底层服务相关的单元测试API接口。
    /// </summary>
    public static class AiukServiceTestUtility
    {
        public static IAiukEventService GetEventService()
        {
            var eventService = new AiukDefaultService_Event();
            return eventService;
        }
    }
}

