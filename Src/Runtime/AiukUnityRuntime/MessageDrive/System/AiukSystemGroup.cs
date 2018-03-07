//using System;
//using System.Collections.Generic;
//using AiukCommon;

//namespace AiukUnityRuntime
//{
//    /// <summary>
//    /// 业务系统组。
//    /// </summary>
//    public class AiukSystemGroup : IAiukSystemGroup
//    {
//        #region 字段

//        /// <summary>
//        /// 业务系统列表。
//        /// </summary>
//        private readonly List<IAiukSystem> m_Systems = new List<IAiukSystem>();

//        /// <summary>
//        /// 业务处理完毕时委托。
//        /// </summary>
//        private readonly Action m_FinishAction;

//        #endregion

//        #region 构造函数

//        public AiukSystemGroup(Action finishAction)
//        {
//            m_FinishAction = finishAction;
//        }

//        #endregion

//        #region 公开函数

//        /// <summary>
//        /// 添加一个业务系统至末尾。
//        /// </summary>
//        /// <param name="system">待添加的业务系统。</param>
//        public void AddSystem(IAiukSystem system)
//        {
//            m_Systems.Add(system);
//        }

//        /// <summary>
//        /// 将一个业务系统添加到指定类型的业务系统的前面位置。
//        /// </summary>
//        /// <typeparam name="T">目标业务系统类型。</typeparam>
//        /// <param name="system">待添加的业务系统。</param>
//        public void AddSystemAtLeft<T>(IAiukSystem system) where T : class, IAiukSystem
//        {
//            var index = m_Systems.FindIndex(s => s.As<T>());
//            if (index == -1)
//            {
//#if UNITY_EDITOR || DEBUG
//                AiukDebugUtility.LogError(string.Format("没有找到目标类型{0}的业务系统！", typeof(T).FullName));
//#endif
//                return;
//            }

//            var leftIndex = index - 1;
//            m_Systems.Insert(leftIndex, system);
//        }

//        /// <summary>
//        /// 将一个业务系统添加到指定类型的业务系统的后面位置。
//        /// </summary>
//        /// <typeparam name="T">目标业务系统类型。</typeparam>
//        /// <param name="system">待添加的业务系统。</param>
//        public void AddSystemAtRight<T>(IAiukSystem system) where T : class, IAiukSystem
//        {
//            var index = m_Systems.FindIndex(s => s.As<T>());
//            if (index == -1)
//            {
//#if UNITY_EDITOR || DEBUG
//                AiukDebugUtility.LogError(string.Format("没有找到目标类型{0}的业务系统！", typeof(T).FullName));
//#endif
//                return;
//            }

//            var rightIndex = index + 1;
//            m_Systems.Insert(rightIndex, system);
//        }

//        public void Execute()
//        {
//            m_Systems.ForEach(s => s.Execute());
//        }

//        public void Finish()
//        {
//            m_FinishAction();
//        }

//        #endregion
//    }
//}

