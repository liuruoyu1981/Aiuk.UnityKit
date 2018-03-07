using System;
using System.Collections.Generic;
using AiukUnityRuntime.Core;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;


namespace AiukUnityRuntime.View
{
    /// <summary>
    /// 滚动列表基类。
    /// </summary>
    public abstract class AiukAbsListView<TData, TIitem>
        : IAiukListViewItemOperator<TData, TIitem>
        where TIitem : IListViewItem<TData>, new()
    {
        #region 基础字段

        protected RectTransform Content;
        protected RectTransform m_Template;
        protected AiukScrollRect ScRect;
        protected readonly Stack<ListViewItemContainer<TData, TIitem>> m_UnusedItems =
            new Stack<ListViewItemContainer<TData, TIitem>>();
        protected readonly LinkedList<ListViewItemContainer<TData, TIitem>> Items =
            new LinkedList<ListViewItemContainer<TData, TIitem>>();
        public readonly BothOperateList<TData> ItemDatas = new BothOperateList<TData>();
        protected readonly Queue<TData> m_BakDatas = new Queue<TData>();

        private AiukListViewState m_State;
        protected AiukListViewState State
        {
            get { return m_State; }
            set
            {
                Debug.Log("老状态为：" + m_State);
                m_State = value;
                Debug.Log("新状态为：" + m_State);
            }
        }

        protected int EnableScrollNum;
        //        private float m_LastExecuteScrollTime;
        public int PerLineMax { get; private set; }
        private int m_DataLineIndex;
        public AiukArrangementType ArrangementType { get; private set; }
        public RectOffset Padding { get; private set; }
        public Vector2 Spacing { get; private set; }
        public Vector2 CellSize { get; private set; }
        public float ItemHeightUnit { get; private set; }
        public float ItemWidthUnit { get; private set; }

        protected float UpBorder
        {
            get
            {
                var result = Items.First.Value.Index * ItemHeightUnit;
                return result;
            }
        }

        protected float BottomBorder
        {
            get
            {
                var result = (Items.Last.Value.Index - EnableScrollNum + 2) * ItemHeightUnit;
                return result;
            }
        }

        #endregion

        #region 静态获取实例

        /// <summary>
        /// 创建一个滚动列表的实例。
        /// </summary>
        /// <typeparam name="IOperator">滚动列表实例类型。</typeparam>
        /// <param name="datas">滚动列表所持有的数据列表源。</param>
        /// <param name="perLineMax">滚动列表每行每列允许的最大子项数量，默认为1。</param>
        /// <returns></returns>
        public static IOperator CreateListView
            <IOperator>
            (
                List<TData> datas,
                int perLineMax = 1
            )
            where IOperator : IAiukListViewItemOperator<TData, TIitem>, new()
        {
            // todo 工厂化
            var operater = Activator.CreateInstance<IOperator>();
            operater
                .SetPerLineMax(perLineMax)
                .SetDatas(datas);

            return operater;
        }

        #endregion

        #region 链式调用

        public IAiukListViewItemOperator<TData, TIitem> SetDatas(List<TData> datas)
        {
            ItemDatas.Clean();
            ItemDatas.PlusList.AddRange(datas);
            return this;
        }

        /// <summary>
        /// 传递滚动列表根对象并启动滚动视图。
        /// </summary>
        /// <param name="listViewRoot">滚动列表根对象。</param>
        public virtual void Start(RectTransform listViewRoot)
        {
            Content = listViewRoot.Find("Viewport/Content").GetComponent<RectTransform>();
            //            LastContentPosition = Content.localPosition;
            m_Template = Content.Find("item_template").GetComponent<RectTransform>();
            //  修改模板的父物体并隐藏
            m_Template.SetParent(listViewRoot);
            m_Template.gameObject.SetActive(false);
            //  删除Content下的所有子物体
            for (var i = 0; i < Content.childCount; i++)
            {
                var child = Content.GetChild(i);
                Object.Destroy(child.gameObject);
            }

            var gridLayoutGroup = listViewRoot.Find("Viewport/Content").GetComponent<GridLayoutGroup>();
            ScRect = listViewRoot.GetComponent<AiukScrollRect>();
            ScRect.onValueChanged.RemoveAllListeners();
            ScRect.onValueChanged.AddListener(HandleScroll);
            ScRect.AddOnEndDragHandle(HandleScrollRectEndDrag);

            //  判断当前的滚动方向
            if (ScRect.horizontal && ScRect.vertical)
                throw new Exception("滚动列表不允许同时进行水平和垂直的滚动！");
            ArrangementType = ScRect.horizontal ? AiukArrangementType.Horizontal : AiukArrangementType.Vertical;

            Padding = gridLayoutGroup.padding;
            Spacing = gridLayoutGroup.spacing;
            CellSize = gridLayoutGroup.cellSize;
            ItemWidthUnit = CellSize.x + Spacing.x;
            ItemHeightUnit = CellSize.y + Spacing.y;

            Object.Destroy(gridLayoutGroup);

            //  计算需要创建多少个子项
            var num = GetInitItemNumAndTryDisableScroll();

            //  添加子项及初始化子项链表
            for (var index = 0; index < num; index++)
            {
                var item = CreateItem(index);
                Items.AddLast(item);
            }
        }

        public IAiukListViewItemOperator<TData, TIitem> SetPerLineMax(
                int perLineMax)
        {
            PerLineMax = perLineMax;
            return this;
        }



        #endregion

        #region 基础函数

        /// <summary>
        /// 获得初始化时需要创建的子项数量并依据数据尝试关闭滚动功能。
        /// </summary>
        private int GetInitItemNumAndTryDisableScroll()
        {
            var num = 0;

            switch (ArrangementType)
            {
                case AiukArrangementType.Horizontal:
                    break;
                case AiukArrangementType.Vertical:
                    EnableScrollNum = Mathf.RoundToInt(Content.sizeDelta.y / ItemHeightUnit);
                    num = (EnableScrollNum + 2) * PerLineMax;
                    num = num > ItemDatas.PlusList.Count ? ItemDatas.TotalCount : num;
                    if (ItemDatas.TotalCount <= num)
                    {
                        DisableScroll();
                    }
                    return num;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return num;
        }

        protected void DisableScroll()
        {
            ScRect.horizontal = false;
            ScRect.vertical = false;
        }

        protected void EnableScroll()
        {
            switch (ArrangementType)
            {
                case AiukArrangementType.Horizontal:
                    ScRect.horizontal = true;
                    break;
                case AiukArrangementType.Vertical:
                    ScRect.vertical = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private ListViewItemContainer<TData, TIitem> CreateItem(int index)
        {
            var item = m_UnusedItems.Count == 0
                ? AiukUnityFactory.CreateListViewItem(
                    m_Template,
                    Content,
                    index,
                    this)
                : m_UnusedItems.Pop();

            return item;
        }

        #endregion

        #region 滚动处理

        protected virtual void HandleScrollRectEndDrag()
        {
            Debug.Log("下拉后抬起处理！");
        }

        private void HandleScroll(Vector2 vx2)
        {
            //            m_LastExecuteScrollTime += Time.deltaTime;
            //            if (m_LastExecuteScrollTime < 0.5) return;

            //            m_LastExecuteScrollTime = 0f;

            switch (ArrangementType)
            {
                case AiukArrangementType.Horizontal:
                    break;
                case AiukArrangementType.Vertical:
                    HandleVerticalScroll();
                    break;
            }
        }

        private void HandleVerticalScroll()
        {
            var contentY = Content.localPosition.y;
            var index = (int)(contentY / ItemHeightUnit);
            if (index == m_DataLineIndex) return;

            if (index < m_DataLineIndex)
            {
                Debug.Log(string.Format("old index {0}! new index {1}", m_DataLineIndex, index));
                if (!OnDown())  //  下拉数据索引--，判断是否可以读取。
                {
                    return;
                }
            }
            else
            {
                if (!OnUp())
                {
                    return;
                }
            }

            m_DataLineIndex = index;
        }

        protected virtual bool OnDown()
        {
            var newIndex = Items.First.Value.Index - 1;
            var canRead = ItemDatas.CanRead(newIndex);
            if (canRead)
            {
                MoveLastToFirst();
            }
            else
            {
                State = AiukListViewState.PollDownOut;
                DisableScroll();
            }

            return canRead;
        }

        protected virtual bool OnUp()
        {
            var newIndex = Items.Last.Value.Index + 1;
            var canRead = ItemDatas.CanRead(newIndex);

            if (canRead)
            {
                MoveFirstToLast();
                return true;
            }

            if (Content.localPosition.y >= BottomBorder)
            {
                Debug.Log("上拉到极限值：" + BottomBorder);
                State = AiukListViewState.UpOut;
                DisableScroll();
            }

            return false;
        }

        #endregion

        #region 子项循环

        protected void MoveLastToFirst()
        {
            for (var i = 0; i < PerLineMax; i++)
            {
                var first = Items.First;
                var last = Items.Last;
                Items.RemoveLast();
                Items.AddFirst(last);
                last.Value.Index = first.Value.Index - 1;
            }
        }

        protected void MoveFirstToLast()
        {
            for (var i = 0; i < PerLineMax; i++)
            {
                var first = Items.First;
                var last = Items.Last;
                Items.RemoveFirst();
                Items.AddLast(first);
                first.Value.Index = last.Value.Index + 1;
            }
        }

        #endregion

        #region 数据操作


        public void AddLast(TData data)
        {
            ItemDatas.PlusList.Add(data);
            OnAddLast();
        }

        protected virtual void OnAddLast() { }

        public void AddNetData(TData data)
        {
            m_BakDatas.Enqueue(data);
        }

        protected void AddFirst(TData data)
        {
            ItemDatas.MinusList.Add(data);
        }

        #endregion
    }
}