using System;
using System.Collections.Generic;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 双向操作列表，同时支持正负数读取。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BothOperateList<T>
    {
        public readonly List<T> MinusList = new List<T>();
        public readonly List<T> PlusList = new List<T>();

        /// <summary>
        /// 目标索引位置是否可以读取数据。
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool CanRead(int index)
        {
            if (index >= 0)
            {
                return index < PlusList.Count;
            }

            var newIndex = Math.Abs(index);
            return MinusList.Count >= newIndex;
        }

        public T this[int index]
        {
            get
            {
                T result;

                if (index >= 0)
                {
                    var newIndex = index % PlusList.Count;
                    result = PlusList[newIndex];
                }
                else
                {
                    var newIndex = Math.Abs(index + 1);
                    result = MinusList[newIndex];
                }

                return result;
            }
            set
            {
                if (index >= 0)
                {
                    PlusList[index] = value;
                }
                else
                {
                    index = Math.Abs(index) - 1;
                    MinusList[index] = value;
                }
            }
        }

        public int TotalCount
        {
            get { return MinusList.Count + PlusList.Count; }
        }

        public void Clean()
        {
            MinusList.Clear();
            PlusList.Clear();
        }

    }
}
