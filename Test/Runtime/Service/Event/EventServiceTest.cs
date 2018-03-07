using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using AiukUnityRuntime;

public class EventServiceTest
{
    private int m_ExecuteOneCount;
    private int m_ExucuteTenCount;
    private IAiukEventService m_EventService;
    private int m_OneHandlerId;
    private int m_TenHandlerId;

    private void EventTestInit()
    {
        m_EventService = AiukServiceTestUtility.GetEventService();
        m_OneHandlerId = m_EventService.WatchEvent(AiukCoreEventCode.Test_ExexuteOne, ExecuteOne, 1);
        m_TenHandlerId = m_EventService.WatchEvent(AiukCoreEventCode.Test_ExecuteTen, ExecuteTen);

        for (int i = 0; i < 10; i++)
        {
            m_EventService.TriggerEvent(AiukCoreEventCode.Test_ExexuteOne);
            m_EventService.TriggerEvent(AiukCoreEventCode.Test_ExecuteTen);
        }
    }

    /// <summary>
    /// 测试事件的执行次数。
    /// 1. 指定执行一次。
    /// 2. 指定执行10次，同无限执行。
    /// </summary>
    /// <returns>The exexute count test.</returns>
    [UnityTest]
    public IEnumerator EventExexuteCount_Test()
    {
        EventTestInit();

        yield return new WaitForSeconds(1);

        Debug.Log("m_ExecuteOneCount:" + m_ExecuteOneCount);
        Debug.Log("m_ExucuteTen:" + m_ExucuteTenCount);

        Assert.AreEqual(1, m_ExecuteOneCount);
        Assert.AreEqual(10, m_ExucuteTenCount);

        yield return null;
    }

    [UnityTest]
    public IEnumerator RemoveEvent_Test()
    {
        EventTestInit();

        yield return new WaitForSeconds(1);

        m_ExecuteOneCount = 0;
        m_ExucuteTenCount = 0;

        m_EventService.RemoveSpecifiedHandler(AiukCoreEventCode.Test_ExexuteOne, m_OneHandlerId);
        m_EventService.RemoveSpecifiedHandler(AiukCoreEventCode.Test_ExecuteTen, m_TenHandlerId);

        m_EventService.TriggerEvent(AiukCoreEventCode.Test_ExexuteOne);
        m_EventService.TriggerEvent(AiukCoreEventCode.Test_ExecuteTen);

        Assert.AreEqual(0, m_ExecuteOneCount);
        Assert.AreEqual(0, m_ExucuteTenCount);

        yield return null;
    }

    private void ExecuteOne()
    {
        m_ExecuteOneCount++;
    }

    private void ExecuteTen()
    {
        m_ExucuteTenCount++;
    }

}
