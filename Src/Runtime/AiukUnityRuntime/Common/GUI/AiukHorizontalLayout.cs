using System;
using UnityEditor;
using UnityEngine;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 水平布局对象。
    /// </summary>
    public class AiukHorizontalLayout : IDisposable
    {
        public Rect Rect { get; private set; }

        public AiukHorizontalLayout()
        {
            Rect = EditorGUILayout.BeginHorizontal();
        }

        public AiukHorizontalLayout(params GUILayoutOption[] options)
        {
            Rect = EditorGUILayout.BeginHorizontal(GUIStyle.none, options);
        }

        public AiukHorizontalLayout(GUIStyle style, params GUILayoutOption[] options)
        {
            Rect = EditorGUILayout.BeginHorizontal(style, options);
        }

        public void Dispose()
        {
            EditorGUILayout.EndHorizontal();
        }
    }
}

