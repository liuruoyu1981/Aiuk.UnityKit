using System;
using System.Collections.Generic;
using AuikEditor;
using Object = UnityEngine.Object;

namespace AiukUnityEditor
{
    public class AiukLightingMode
    {
        public Func<string, IEnumerable<AiukLightingInfo>> RefreshAction { get; set; }
        public Func<AiukLightingInfo, Object[]> LoadItem { get; set; }

        public string SearchLabel { get; set; }
        public bool MoveWindowMode { get; set; }

    }
}


