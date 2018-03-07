using UnityEngine;

 
 
#pragma warning disable CS3003

#region Head

// Author:        liuruoyu1981
// CreateDate:    3/5/2018 10:02:02 AM
// Email:         liuruoyu1981@gmail.com

#endregion

namespace AiukUnityRuntime
{
    /// <summary>
    /// 视图控件容器。
    /// </summary>
    public interface IAiukViewControlContainer
    {
        #region 基础属性

        /// <summary>
        /// 视图容器的根节点对象。
        /// </summary>
        RectTransform RectRoot { get; }

        /// <summary>
        /// 视图容器的资源名。
        /// </summary>
        string AssetName { get; }

        /// <summary>
        /// 视图控件容器ID。
        /// </summary>
        string ID { get; }

        #endregion

        #region 控件获取





        #endregion

        #region 控件组属性





        #endregion
    }
}
