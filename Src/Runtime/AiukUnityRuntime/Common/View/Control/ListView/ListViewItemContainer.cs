using Aiuk.Common.Utility;
using UnityEngine;

namespace AiukUnityRuntime.View
{
    /// <summary>
    /// 滚动列表子项容器，封装一个游戏对象和一个泛型数据类型。
    /// 1. 负责子项游戏对象的位置移动。
    /// 2. 负责调用泛型数据类型的更新函数修改游戏对象的外观。
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="TItem"></typeparam>
    public class ListViewItemContainer<TData, TItem> where TItem : IListViewItem<TData>, new()
    {
        private int m_Index = -99999;
        protected readonly TItem Drawer;
        protected readonly RectTransform Rect;
        private readonly AiukAbsListView<TData, TItem> ListView;
        protected readonly BothOperateList<TData> DataList;

        protected TData Data
        {
            get
            {
                var data = DataList[Index];
                return data;
            }
        }

        public int Index
        {
            get { return m_Index; }
            set
            {
                if (m_Index == value)
                {
#if UNITY_EDITOR || DEBUG
                    AiukDebugUtility.Log("尝试赋值一个和现有索引{0}的相同的值！", m_Index);
#endif
                    return;
                }

                m_Index = value;

                //  修改位置
                UpdatePosition();
                //  修改游戏对象名
                Rect.gameObject.name = "item_" + Index;
                //  修改外观
                Drawer.UpdateDraw(Data);
            }
        }

        public ListViewItemContainer(RectTransform template, RectTransform contentParent,
            int index,
            AiukAbsListView<TData, TItem> itemOperator
            )
        {
            Rect = template.gameObject.GetComponent<RectTransform>();
            ListView = itemOperator;
            TryInitStaticField(itemOperator);    //  静态本地变量赋值，提高计算性能
            DataList = itemOperator.ItemDatas;
            Drawer = new TItem();
            Drawer.Init(template);
            Rect.gameObject.SetActive(true);
            Rect.SetParent(contentParent);
            Rect.localScale = Vector3.one;
            Index = index;
        }

        private static bool IsStaticFieldInited;

        private static void TryInitStaticField(AiukAbsListView<TData, TItem> listView)
        {
            if (IsStaticFieldInited) return;

            PerLineMax = listView.PerLineMax;
            ItemWidthUnit = listView.ItemWidthUnit;
            ItemHeightUnit = listView.ItemHeightUnit;
            Padding = listView.Padding;
            Spacing = listView.Spacing;
            CellSize = listView.CellSize;
            ArrangementType = listView.ArrangementType;

            IsStaticFieldInited = true;
        }

        #region 位置操作

        private static int PerLineMax;
        private static float ItemWidthUnit;
        private static float ItemHeightUnit;
        private static RectOffset Padding;
        private static Vector2 Spacing;
        private static Vector2 CellSize;
        private static AiukArrangementType ArrangementType;

        private void UpdatePosition()
        {
            switch (ArrangementType)
            {
                case AiukArrangementType.Horizontal:

                    break;
                case AiukArrangementType.Vertical:
                    var x = GetPositon_X_Vertical();
                    var y = GetPositon_Y_Vertical();
                    Rect.localPosition = new Vector3(x, y, 0);
                    break;
            }
        }

        private float GetPositon_X_Vertical()
        {
            var residue = Mathf.Abs(Index) % PerLineMax;
            var x = Padding.left + residue * ItemWidthUnit + CellSize.x / 2;
            return x;
        }

        private float GetPositon_Y_Vertical()
        {
            var residue = Mathf.Floor((float)Index / PerLineMax);
            var y = CellSize.y / 2 - residue * ItemHeightUnit - CellSize.y - Padding.top;
            return y;
        }



        #endregion

    }
}

