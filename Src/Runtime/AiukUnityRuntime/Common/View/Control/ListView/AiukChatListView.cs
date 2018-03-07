using System;
using AiukUnityRuntime.Tween;
using UnityEngine;

namespace AiukUnityRuntime.View
{
    /// <summary>
    /// 用于聊天室的滚动列表。
    /// 1. 禁止数据循环显示。
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="TItem"></typeparam>
    public class AiukChatListView<TData, TItem>
        : AiukAbsListView<TData, TItem>
        where TItem : IListViewItem<TData>, new()
    {
        #region 字段

        private RectTransform m_LoadingRect;
        private Action<IAiukListViewItemOperator<TData, TItem>> m_PollDownUpdate;

        #endregion

        #region 链式

        public AiukChatListView<TData, TItem> SetPollDownUpdate(
            Action<IAiukListViewItemOperator<TData, TItem>> pollDownUpdate)
        {
            m_PollDownUpdate = pollDownUpdate;
            return this;
        }

        #endregion

        #region 下拉刷新

        private IAiukTween<float> m_loadingTween;

        private void ShowLoadingTween()
        {
            Debug.LogError("开启loading动画！");
            m_LoadingRect.gameObject.SetActive(true);
            if (m_loadingTween == null)
            {
                m_loadingTween = m_LoadingRect.gameObject
                    .DoRotateZ(720f, 2f)
                    .SetLoopType(AiukTweenLoopType.Restart)
                    .SetLoopCount(-1);
            }
            else
            {
                m_loadingTween.Reset();
                m_loadingTween.Resume();
            }
        }

        private void CloseLoadingTween(Action callback)
        {
            Debug.LogError("关闭loading动画");
            m_loadingTween.Pause();
            m_LoadingRect.gameObject.SetActive(false);
            var originPosition = Items.First.Value.Index * ItemHeightUnit;
            Content.DoLocalMoveY(originPosition, 0.15f)
                .OnCompleted(t =>
                {
                    Debug.Log("回到顶部位置完成，开始执行可能存在的新数据绘制操作！");
                    State = AiukListViewState.Normal;
                    callback();
                });
        }

        #endregion

        #region 处理数据变动

        protected override void OnAddLast()
        {
            //  上移一单位
            var newPosition = Content.localPosition.y + ItemHeightUnit;
            Content.DoLocalMoveY(newPosition, 0.1f);
        }

        #endregion

        #region 重写基类方法

        protected override bool OnDown()
        {
            var newIndex = Items.First.Value.Index - 1;
            var canRead = ItemDatas.CanRead(newIndex);
            if (canRead)
            {
                MoveLastToFirst();
                return true;
            }

            if (State == AiukListViewState.AddNewData) return false;

            State = AiukListViewState.PollDownOut;
            DisableScroll();

            //  请求数据
            if (m_PollDownUpdate != null)
            {
                Debug.Log("状态：" + State);
                State = AiukListViewState.PollDownUpdate;
                ShowLoadingTween();
                m_PollDownUpdate(this);
            }
            return false;
        }

        private void MoveStackData()
        {
            if (m_BakDatas.Count == 0) return;

            State = AiukListViewState.AddNewData;
            var dataCount = m_BakDatas.Count;
            foreach (var data in m_BakDatas)
            {
                AddFirst(data);
            }
            m_BakDatas.Clear();
            var newPosition = Content.localPosition.y - ItemHeightUnit * dataCount;
            Debug.Log("目标位置：" + newPosition);
            Content.DoLocalMoveY(newPosition, 0.2f * dataCount)
                .OnCompleted(
                    t =>
                    {
                        State = AiukListViewState.Normal;
                        EnableScroll();
                    });
        }

        protected override void HandleScrollRectEndDrag()
        {
            base.HandleScrollRectEndDrag();

            if (State == AiukListViewState.PollDownUpdate)
            {
                CloseLoadingTween(() =>
                {
                    //  有新数据进行新数据子项的移动和绘制
                    if (m_BakDatas.Count > 0)
                    {
                        MoveStackData();
                    }

                    Debug.Log("没有新数据可以绘制！");
                    EnableScroll();
                });

                return;
            }

            if (State == AiukListViewState.PollDownOut)
            {
                //  返回顶部
                Content.gameObject.DoLocalMoveY(UpBorder, 0.1f, null, AiukEaseType.SineEaseIn)
                    .OnCompleted(t =>
                    {
                        State = AiukListViewState.Normal;
                        EnableScroll();
                    });
            }

            if (State == AiukListViewState.UpOut)
            {
                Content.gameObject.DoLocalMoveY(BottomBorder, 0.1f, null, AiukEaseType.SineEaseIn)
                    .OnCompleted(t =>
                    {
                        State = AiukListViewState.Normal;
                        EnableScroll();
                    });
            }

            if (State == AiukListViewState.Normal)
            {
                EnableScroll();
            }
        }

        /// <summary>
        /// 传递滚动列表根对象并启动滚动视图。
        /// </summary>
        /// <param name="listViewRoot"></param>
        public override void Start(RectTransform listViewRoot)
        {
            base.Start(listViewRoot);

            m_LoadingRect = listViewRoot.Find("item_loading").GetComponent<RectTransform>();
            m_LoadingRect.gameObject.SetActive(false);
        }

        #endregion


    }
}
