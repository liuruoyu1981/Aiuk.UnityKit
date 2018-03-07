using System;
using System.Collections.Generic;
using Aiuk.Common.Utility;
using AiukUnityRuntime.View;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AiukUnityRuntime
{
    public class AiukButton : Button, IAiukButton,
        IDragHandler, IBeginDragHandler, IEndDragHandler,
        IDisposable
    {
        #region 字段

        /// <summary>
        /// 当前按钮的交互事件指针数据。
        /// </summary>
        public static PointerEventData CurrentButtonEventData { get; private set; }

        /// <summary>
        /// 按钮所在的控件容器ID。
        /// </summary>
        public string ControlContainerId { get; private set; }

        /// <summary>
        /// 按钮所在的控件容器。
        /// </summary>
        protected IAiukViewControlContainer CContainer { get; private set; }

        /// <summary>
        /// 当前的应用实例。
        /// </summary>
        protected IAiukUnityApp App { get; private set; }

        /// <summary>
        /// 按钮的音效名，可以为空。
        /// 为空时则播放默认值，默认值在RunSetting中配置。
        /// </summary>
        private string m_SoundEffectName;

        /// <summary>
        /// 按钮行为处理请求调度器。
        /// </summary>
        private IAiukViewActionDispatcher<IAiukButton> m_Dispatcher;

        public GameObject GameObject
        {
            get { return gameObject; }
        }

        #endregion

        #region 公有函数

        public virtual void BindingApp(IAiukUnityApp app, IAiukViewControlContainer container)
        {
            App = app;
            CContainer = container;
        }

        public void BindingScrollRect(ScrollRect scrollRect)
        {
            m_ScrollRect = scrollRect;
        }

        #region 交互委托绑定

        private Dictionary<AiukButtonEventType, Action> m_ButtonActions;

        public void BindingAction(AiukButtonEventType type, Action action)
        {
            if (m_ButtonActions.ContainsKey(type))
            {
                AiukDebugUtility.LogError
                    (string.Format("目标行为处理委托{0}已存在，无法绑定！", type));
                return;
            }

            m_ButtonActions.Add(type, action);
        }

        public void RemoveAction(AiukButtonEventType type)
        {
            if (m_ButtonActions.ContainsKey(type))
            {
                AiukDebugUtility.LogError
                    (string.Format("目标行为处理委托{0}不存在，无法移除", type));
                return;
            }

            m_ButtonActions.Remove(type);
        }

        #endregion

        #endregion

        #region 私有函数

        #region 交互事件覆写

        /// <summary>
        /// 按钮交互行为令牌字典。
        /// </summary>
        private Dictionary<AiukButtonEventType, string> m_ActionTokens;

        /// <summary>
        /// 按钮交互行为请求字典。
        /// </summary>
        private Dictionary<AiukButtonEventType, IAiukViewActionRequest<IAiukButton>> m_Requests;

        private string GetActionToken(AiukButtonEventType type)
        {
            if (m_ActionTokens.ContainsKey(type))
            {
                var token = m_ActionTokens[type];
                return token;
            }

            var newToken = CContainer.ID + "_" + gameObject.name
                           + "_" + type;
            m_ActionTokens.Add(type, newToken);
            return newToken;
        }

        private IAiukViewActionRequest<IAiukButton> GetRequest(AiukButtonEventType type)
        {
            if (m_Requests.ContainsKey(type))
            {
                var request = m_Requests[type];
                return request;
            }

            var newRequest = new AiukViewActionRequest<IAiukButton>();
            m_Requests.Add(type, newRequest);
            return newRequest;
        }

        private void InvokeAction(PointerEventData eventData, AiukButtonEventType type)
        {
            CurrentButtonEventData = eventData;

            if (m_ButtonActions.ContainsKey(type))
            {
                var action = m_ButtonActions[type];
                action();
            }
            else
            {
                var request = GetRequest(type);
                m_Dispatcher.IssueRequest(request);
            }
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            InvokeAction(eventData, AiukButtonEventType.OnPointerClick);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            InvokeAction(eventData, AiukButtonEventType.OnPointerEnter);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            InvokeAction(eventData, AiukButtonEventType.OnPointerDown);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            InvokeAction(eventData, AiukButtonEventType.OnPointerExit);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            InvokeAction(eventData, AiukButtonEventType.OnPointerUp);
        }

        /// <summary>
        /// 按钮父级对象上可能存在的ScrollRect组件。
        /// </summary>
        private ScrollRect m_ScrollRect;

        public void OnDrag(PointerEventData eventData)
        {
            //  当按钮作为滚动列表模板子项并开启了按钮的交互功能时，按钮会屏蔽滚动列表的OnDrag方法的触发，
            //  因此需要在这里判断一下，如果父级物体存在ScrollRect组件则调用其OnDrag方法。
            if (m_ScrollRect != null)
            {
                m_ScrollRect.OnDrag(eventData);
            }

            InvokeAction(eventData, AiukButtonEventType.OnDrag);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            InvokeAction(eventData, AiukButtonEventType.OnBeginDrag);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            InvokeAction(eventData, AiukButtonEventType.OnEndDrag);
        }

        #endregion

        #endregion

        #region Mono生命周期

        protected override void Start()
        {
            OnStart();
        }

        protected virtual void OnStart()
        {
            m_ButtonActions = AiukViewFactory.GetButtonActions();
            m_ActionTokens = AiukViewFactory.GetButtonTokens();
            m_Requests = AiukViewFactory.GetButtonRequests();
        }

        #endregion

        public void Dispose()
        {
            AiukViewFactory.RestoreButtonActions(m_ButtonActions);
            AiukViewFactory.RestoreButtonTokens(m_ActionTokens);
            AiukViewFactory.RestoreButtonRequests(m_Requests);
        }
    }
}

