using System.Collections.Generic;
using UnityEngine;

namespace AiukUnityRuntime.View
{
    /// <summary>
    /// 滚动列表子项操作接口。
    /// </summary>
    public interface IAiukListViewItemOperator<TData, TItem>
        where TItem : IListViewItem<TData>, new()
    {
        /// <summary>
        /// 设置每行或每列允许的最大子项数量。
        /// </summary>
        /// <param name="perLineMax"></param>
        /// <returns></returns>
        IAiukListViewItemOperator<TData, TItem> SetPerLineMax(int perLineMax);

        IAiukListViewItemOperator<TData, TItem> SetDatas(List<TData> datas);

        void AddLast(TData data);

        void AddNetData(TData data);

        /// <summary>
        /// 启用滚动列表。
        /// </summary>
        void Start(RectTransform listViewRoot);

    }
}