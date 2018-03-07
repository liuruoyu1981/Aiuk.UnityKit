using System;
using System.Collections.Generic;
using System.IO;
using Aiuk.Common.Utility;
using UnityEngine;

namespace AiukUnityRuntime
{
    /// <summary>
    /// unity项目设置，一个unity项目中可以同时存在多个AiukApp并可同时开发。
    /// </summary>
    [Serializable]
    public class AiukUnitySetting
    {
        #region unity项目默认设置

        /// <summary>
        /// unity项目默认设置文件路径（json格式）。
        /// </summary>
        private static string DefaultSettingPath
        {
            get
            {
                var path = Application.dataPath + "/.AiukUnitySetting.json";
                return path;
            }
        }

        /// <summary>
        /// 获得默认的unity项目设置实例。
        /// </summary>
        /// <returns></returns>
        public static AiukUnitySetting GetDefaultSetting()
        {
            if (!File.Exists(DefaultSettingPath))
            {
                var unitySetting = new AiukUnitySetting();
                var jsContent = JsonUtility.ToJson(unitySetting);
                AiukIOUtility.WriteAllText(DefaultSettingPath, jsContent);
            }

            var setting = JsonUtility.FromJson<AiukUnitySetting>(File.ReadAllText(DefaultSettingPath));
            return setting;
        }

        #endregion

        #region 项目设置

        /// <summary>
        /// unity项目中所有的AiukApp列表
        /// </summary>
        public List<AiukAppSetting> AiukApps { get; private set; }


        #endregion


    }
}
