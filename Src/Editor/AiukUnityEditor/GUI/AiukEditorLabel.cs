using AiukUnityRuntime;
using UnityEditor;
using UnityEngine;

namespace AiukUnityEditor
{
    /// <summary>
    /// 编辑器GUI元素：LabelField。
    /// </summary>
    public class AiukEditorLabel : AiukAbsGUIElement
    {
        public string Title { get; private set; }
        public GUIStyle Style { get; private set; }

        public AiukEditorLabel(int x, int y, int w, int h, string title, GUIStyle style = null)
            : base(x, y, w, h)
        {
            Title = title;
            Style = style;
        }

        public override void Draw()
        {
            EditorGUI.LabelField(new Rect(StartX, StartY, Width, Height), Title, Style);
        }
    }
}


