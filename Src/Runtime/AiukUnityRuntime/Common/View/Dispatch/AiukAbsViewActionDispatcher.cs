using System.Collections.Generic;
using Aiuk.Common.Utility;




#region Head

// Author:        liuruoyu1981
// CreateDate:    3/5/2018 5:15:52 PM
// Email:         liuruoyu1981@gmail.com

#endregion

namespace AiukUnityRuntime
{
    /// <summary>
    /// 视图行为处理请求调度器基类。
    /// </summary>
    public abstract class AiukAbsViewActionDispatcher<T> : IAiukViewActionDispatcher<T>
        where T : IAiukViewlement
    {
        protected IAiukUnityApp App { get; private set; }

        public void IssueRequest(IAiukViewActionRequest<T> request)
        {
            var token = request.Token;
            if (!m_Responsers.ContainsKey(token))
            {
                return;
            }

            var responser = m_Responsers[token];
            responser.ProcessRequest(request);
        }

        public IAiukViewActionDispatcher<T> BindingApp(IAiukUnityApp app)
        {
            App = app;
            return this;
        }

        private readonly Dictionary<string, IAiukViewActionResponser<T>> m_Responsers
        = new Dictionary<string, IAiukViewActionResponser<T>>();

        public IAiukViewActionDispatcher<T> InjectResponser(IAiukViewActionResponser<T> responser)
        {
            //  命名约定，视图行为处理器的类名一定和行为的令牌（Token）相同。
            var token = responser.GetType().Name;
            if (m_Responsers.ContainsKey(token))
            {
                AiukDebugUtility.LogError(string.Format("目标响应器{0}当前已存在，无法注入！", token));
                return this;
            }

            m_Responsers.Add(token, responser);
            return this;
        }

        public IAiukViewActionDispatcher<T> ReplaceResponser(string token, IAiukViewActionResponser<T> responser)
        {
            if (!m_Responsers.ContainsKey(token))
            {
                AiukDebugUtility.LogError(string.Format("目标响应器{0}当前不存在，无法替换！", token));
                return this;
            }

            m_Responsers[token] = responser;
            return this;
        }
    }
}
