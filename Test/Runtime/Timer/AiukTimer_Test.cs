using System.Collections.Generic;
using AiukUnityRuntime;
using UnityEngine;

public class AiukTimer_Test : MonoBehaviour
{
    private readonly List<IAiukUpdateExecute> m_Systems = new List<IAiukUpdateExecute>();

    void Start()
    {
        //var timerSystem = new AiukTimerSystem();
        //timerSystem.Init(AiukTimerPipeline.Instance);
        //m_Systems.Add(timerSystem);

        //AiukTimerFactory.GetOnceTimer(3f, timer =>
        //{

        //});
    }

    void Update()
    {
        m_Systems.ForEach(s => s.Execute());
    }

    private void LateUpdate()
    {
        m_Systems.ForEach(s => s.LateExecute());
    }

    private void FixedUpdate()
    {
        m_Systems.ForEach(s => s.FixedExecute());
        m_Systems.ForEach(s => s.FinishExecute());
    }
}
