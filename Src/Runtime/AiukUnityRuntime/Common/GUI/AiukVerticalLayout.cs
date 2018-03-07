using System;
using UnityEditor;
using UnityEngine;


namespace AiukUnityRuntime
{
    /// <summary>
    /// 垂直布局对象。
    /// </summary>
    public class AiukVerticalLayout : IDisposable
    {
        public Rect Rect { get; private set; }

        public AiukVerticalLayout()
        {
            Rect = EditorGUILayout.BeginVertical();
        }

        public AiukVerticalLayout(params GUILayoutOption[] options)
        {
            Rect = EditorGUILayout.BeginVertical(GUIStyle.none, options);
        }

        public AiukVerticalLayout(GUIStyle style, params GUILayoutOption[] options)
        {
            Rect = EditorGUILayout.BeginVertical(style, options);
        }

        public void Dispose()
        {
            EditorGUILayout.EndVertical();
        }
    }
}
