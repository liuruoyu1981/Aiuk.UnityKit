using System;
using System.Collections.Generic;
using AiukUnityRuntime.Core.DataPipeline;
using AiukUnityRuntime.View;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

namespace AiukUnityRuntime.Core
{
    /// <summary>
    /// unity对象创建静态工厂。
    /// </summary>
    public static class AiukUnityFactory
    {
        public static ListViewItemContainer<TData, TItemDrawer> CreateListViewItem<TData, TItemDrawer>(
            RectTransform template,
            RectTransform contentParent,
            int index,
            AiukAbsListView<TData, TItemDrawer> m)
            where TItemDrawer : IListViewItem<TData>, new()
        {
            var go = Object.Instantiate(template.gameObject);
            var newItem = new ListViewItemContainer<TData, TItemDrawer>(go.GetComponent<RectTransform>(),
                contentParent, index, m);
            return newItem;
        }

        private static readonly Dictionary<string, object> Singles =
            new Dictionary<string, object>();

        public static T GetSingle<T>() where T : class, new()
        {
            var fullName = typeof(T).FullName;
            if (Singles.ContainsKey(fullName))
            {
                return Singles[fullName] as T;
            }

            var instance = Activator.CreateInstance<T>();
            Singles.Add(fullName, instance);
            return instance;
        }


        #region 数据实例

        public static AiukUIInteractiveData GetUiInteractiveData(AiukUIControlType type,
            PointerEventData eventData, string eventTypeStr)
        {
            var data = new AiukUIInteractiveData();
            data.Init(eventData, eventTypeStr);
            return data;
        }



        #endregion




    }
}