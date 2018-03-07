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
    /// ��ͼ��Ϊ����������������ࡣ
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
            //  ����Լ������ͼ��Ϊ������������һ������Ϊ�����ƣ�Token����ͬ��
            var token = responser.GetType().Name;
            if (m_Responsers.ContainsKey(token))
            {
                AiukDebugUtility.LogError(string.Format("Ŀ����Ӧ��{0}��ǰ�Ѵ��ڣ��޷�ע�룡", token));
                return this;
            }

            m_Responsers.Add(token, responser);
            return this;
        }

        public IAiukViewActionDispatcher<T> ReplaceResponser(string token, IAiukViewActionResponser<T> responser)
        {
            if (!m_Responsers.ContainsKey(token))
            {
                AiukDebugUtility.LogError(string.Format("Ŀ����Ӧ��{0}��ǰ�����ڣ��޷��滻��", token));
                return this;
            }

            m_Responsers[token] = responser;
            return this;
        }
    }
}
