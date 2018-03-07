using System;
using System.Collections.Generic;
using Aiuk.Common.Utility;
using UnityEngine;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 使用反射基于帧的实例加载器。
    /// 1. 反射构造所有的类型实例。
    /// 2. 将类型实例的初始化代码放入帧循环中执行。
    /// 3. 切分时间片以优化应用性能。
    /// </summary>
    public class AiukFrameTypeLoaderAtReflect<T> : MonoBehaviour where T : class
    {
        private void Start()
        {
            m_Name = GetType().Name;
        }

        /// <summary>
        /// 加载器的类型名。
        /// </summary>
        private string m_Name;

        /// <summary>
        /// 需要实例化的目标类型列表。
        /// </summary>
        private List<Type> m_Types;

        /// <summary>
        /// 将要被实例化的类型。
        /// </summary>
        private Type m_CurrentType;

        /// <summary>
        /// 已构建的实例总数量。
        /// </summary>
        private int m_CreateTotalCount;

        /// <summary>
        /// 当前帧创建的实例数量。
        /// </summary>
        private int m_CreateCount;

        /// <summary>
        /// 待构建的类型列表索引。
        /// </summary>
        private int m_TypeIndex = -1;

        /// <summary>
        /// 每帧构建的实例数量。
        /// </summary>
        private int m_EveryCount;

        /// <summary>
        /// 目标类型实例列表。
        /// 由外部传入。
        /// </summary>
        private List<T> m_Instances;

        public void Init(List<Type> types, int everyCount, List<T> instances)
        {
            m_Types = types;
            m_EveryCount = everyCount;
            m_Instances = instances;
        }

        private void Update()
        {
            if (m_CreateTotalCount == m_Types.Count)
            {
                Destroy(this);
                AiukDebugUtility.Log(string.Format("帧实例加载器{0}已创建完所有实例，加载器将销毁！", m_Name));
                return;
            }

            CreateInstance();
        }

        /// <summary>
        /// 显式手动创建的类型索引列表。
        /// </summary>
        private readonly List<int> m_ExplicitCreatedIndexs = new List<int>();

        /// <summary>
        /// 创建实例
        /// </summary>
        private void CreateInstance()
        {
            while (true)
            {
                if (m_CreateTotalCount == m_Types.Count) return;
                if (m_CreateCount == m_EveryCount)
                {
                    m_CreateCount = 0;
                    return;
                }

                m_TypeIndex++;
                m_CreateCount++;
                m_CreateTotalCount++;
                if (m_ExplicitCreatedIndexs.Contains(m_TypeIndex)) continue;

                m_CurrentType = m_Types[m_TypeIndex];
                var instance = Activator.CreateInstance(m_CurrentType) as T;
#if UNITY_EDITOR || DEBUG
                AiukDebugUtility.Log(string.Format("加载器{0}成功创建了一个类型{1}的实例！", m_Name, m_CurrentType.Name));
#endif
                m_Instances.Add(instance);
            }
        }

        /// <summary>
        /// 显式手动创建实例。
        /// </summary>
        /// <returns></returns>
        public T ExplicitCreate(string typeName)
        {
            var index = m_Types.FindIndex(t => t.Name == typeName);
            if (index == -1)
            {
                AiukDebugUtility.LogWarning(string.Format("没有找到所要创建的目标类型{0}！", typeName));
                return null;
            }

            m_ExplicitCreatedIndexs.Add(index);
            var type = m_Types[index];
            var instance = Activator.CreateInstance(type) as T;
            m_Instances.Add(instance);

            return instance;
        }
    }
}