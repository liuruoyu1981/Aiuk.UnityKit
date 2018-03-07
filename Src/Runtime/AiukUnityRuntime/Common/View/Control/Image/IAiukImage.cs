#region Head

// Author:        liuruoyu1981
// CreateDate:    3/6/2018 9:01:45 AM
// Email:         liuruoyu1981@gmail.com

#endregion

using UnityEngine;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 精灵图片。
    /// </summary>
    public interface IAiukImage : IAiukViewControl
    {
        /// <summary>
        /// 设置或获取精灵的图片资源名。
        /// 这会导致精灵的外观发生变更。
        /// </summary>
        string ImageName { get; set; }

        /// <summary>
        /// 设置或获取精灵的当前颜色。
        /// </summary>
        Color Color { get; set; }

        /// <summary>
        /// 设置精灵的颜色透明度。
        /// </summary>
        /// <param name="alpha"></param>
        void SetAlpha(float alpha);

        /// <summary>
        /// 将精灵设置为原生大小尺寸。
        /// </summary>
        void ToNativeSeize();
    }
}
