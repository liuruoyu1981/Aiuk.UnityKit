using System;
using System.Collections.Generic;
using Aiuk.Common.PoolCache;
using AiukUnityRuntime.View;

 

#region Head

// Author:        liuruoyu1981
// CreateDate:    3/5/2018 4:31:57 PM
// Email:         liuruoyu1981@gmail.com

#endregion

namespace AiukUnityRuntime
{
    /// <summary>
    /// 视图静态工厂。
    /// 提供各种缓存数据结构的获取及回收API。
    /// 提供各种中间数据结构的构建API。
    /// </summary>
    public static class AiukViewFactory
    {
        #region 按钮

        private static readonly IAiukObjectPool<Dictionary<AiukButtonEventType, Action>> ButtonActions
        = new AiukObjectPool<Dictionary<AiukButtonEventType, Action>>(() => new Dictionary<AiukButtonEventType, Action>(), 100);

        public static Dictionary<AiukButtonEventType, Action> GetButtonActions()
        {
            var actions = ButtonActions.Take();
            return actions;
        }

        public static void RestoreButtonActions(Dictionary<AiukButtonEventType, Action> actions)
        {
            actions.Clear();
            ButtonActions.Restore(actions);
        }

        private static readonly IAiukObjectPool<Dictionary<AiukButtonEventType, string>> ButtonTokens
        = new AiukObjectPool<Dictionary<AiukButtonEventType, string>>(() => new Dictionary<AiukButtonEventType, string>(), 100);

        public static Dictionary<AiukButtonEventType, string> GetButtonTokens()
        {
            var tokens = ButtonTokens.Take();
            return tokens;
        }

        public static void RestoreButtonTokens(Dictionary<AiukButtonEventType, string> tokens)
        {
            tokens.Clear();
            ButtonTokens.Restore(tokens);
        }

        private static readonly IAiukObjectPool<Dictionary<AiukButtonEventType, IAiukViewActionRequest<IAiukButton>>> ButtonRequests = new AiukObjectPool<Dictionary<AiukButtonEventType, IAiukViewActionRequest<IAiukButton>>>
                (
                    () => new Dictionary<AiukButtonEventType, IAiukViewActionRequest<IAiukButton>>(),
                    100
                );

        public static Dictionary<AiukButtonEventType, IAiukViewActionRequest<IAiukButton>> GetButtonRequests()
        {
            var requests = ButtonRequests.Take();
            return requests;
        }

        public static void RestoreButtonRequests(
            Dictionary<AiukButtonEventType, IAiukViewActionRequest<IAiukButton>> requests)
        {
            requests.Clear();
            ButtonRequests.Restore(requests);
        }

        #endregion






    }
}
