using AiukUnityRuntime;
using UnityEditor;
using UnityEngine;

namespace AiukUnityEditor
{
    public class AiukEditorTextArea : AiukAbsGUIElement
    {
        public string Content { get; private set; }
        public GUIStyle Style { get; private set; }

        public AiukEditorTextArea(int x, int y, int w, int h, string content, GUIStyle style = null)
            : base(x, y, w, h)
        {
            Content = content;
            Style = style;
        }

        public override void Draw()
        {
            EditorGUI.TextArea(new Rect(StartX, StartY, Width, Height), Content, Style);
        }
    }
}


