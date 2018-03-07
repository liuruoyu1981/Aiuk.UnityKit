#region Head

// Author:        liuruoyu1981
// CreateDate:    3/6/2018 9:17:03 AM
// Email:         liuruoyu1981@gmail.com

#endregion

using System.Collections.Generic;
using UnityEngine;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 精灵图片助手。
    /// 1. 维护精灵的图集数据。
    /// </summary>
    public class AiukImageHelper
    {
        /// <summary>
        /// 当前应用实体中所使用的所有精灵。
        /// </summary>
        private readonly Dictionary<string, List<Sprite>> Sprites
        = new Dictionary<string, List<Sprite>>();

        public Sprite GetSprite(string name)
        {
            return null;
        }
    }
}
