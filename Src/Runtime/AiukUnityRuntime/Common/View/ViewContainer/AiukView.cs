using System.Collections.Generic;
using Aiuk.Common.Utility;

namespace AiukUnityRuntime.View
{
    /// <summary>
    /// 视图。
    /// 作为视图组件容器提供一个整体的抽象实体及操作对象。
    /// </summary>
    public class AiukView : IAiukView
    {
        #region 字段

        /// <summary>
        /// 应用，代理程序实体。
        /// </summary>
        private IAiukUnityApp _mUnityApp;



        #endregion

        #region 生命周期





        #endregion

        #region 初始化

        public virtual IAiukView App(IAiukUnityApp unityApp)
        {
            _mUnityApp = unityApp;
            return this;
        }

        public virtual IAiukView Component(params AiukAbsViewComponent[] components)
        {
            foreach (var viewComponent in components)
            {
                if (m_ViewComponents.ContainsKey(viewComponent.Name))
                {
                    AiukDebugUtility.LogError(string.Format("名为{0}的视图组件当前已存在！"
                        , viewComponent.Name));
                    continue;
                }

                m_ViewComponents.Add(viewComponent.Name, viewComponent);
            }

            return this;
        }

        /// <summary>
        /// 绘制视图元素。
        /// </summary>
        public virtual void Draw()
        {

        }

        #endregion

        #region 视图组件

        /// <summary>
        /// 视图组件字典。
        /// </summary>
        private readonly Dictionary<string, AiukAbsViewComponent> m_ViewComponents
            = new Dictionary<string, AiukAbsViewComponent>();

        #endregion
    }
}

