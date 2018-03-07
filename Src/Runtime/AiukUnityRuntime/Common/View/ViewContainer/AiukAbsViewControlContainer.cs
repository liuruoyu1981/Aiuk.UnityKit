using UnityEngine;

 
 
#pragma warning disable CS3003

#region Head

// Author:        liuruoyu1981
// CreateDate:    3/5/2018 9:59:08 AM
// Email:         liuruoyu1981@gmail.com

#endregion

namespace AiukUnityRuntime
{
    /// <summary>
    /// UI¿Ø¼þÈÝÆ÷¡£
    /// </summary>
    public abstract class AiukAbsViewControlContainer : IAiukViewControlContainer
    {
        public RectTransform RectRoot { get; private set; }
        public string AssetName { get; private set; }
        public string ID { get; private set; }
    }
}
