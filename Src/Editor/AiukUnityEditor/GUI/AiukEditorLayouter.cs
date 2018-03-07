using System;
using UnityEditor;
using UnityEngine;

namespace AiukUnityEditor
{
    public class AiukEditorLayouter
    {
        #region 字段

        private readonly int m_OriginalX, m_OriginalY;

        public int StartX { get; private set; }

        public int StartY { get; private set; }

        public int LastWidth { get; private set; }

        public int LastHeight { get; private set; }

        public int SpacingX { get; private set; }

        public int SpacingY { get; private set; }

        #endregion

        #region 构造函数

        public AiukEditorLayouter(int startX, int startY, int spacingX = 5, int spacingY = 5)
        {
            m_OriginalX = startX;
            m_OriginalY = startY;
            SpacingX = spacingX;
            SpacingY = spacingY;
            StartX = startX;
            StartY = startY;
        }

        #endregion

        #region 基础公共API

        public Rect GetRect(int width, int heigth)
        {
            LastWidth = width;
            LastHeight = heigth;
            return new Rect(StartX, StartY, width, heigth);
        }

        public void Reset()
        {
            StartX = m_OriginalX;
            StartY = m_OriginalY;
        }

        #endregion

        #region 移动绘制起始点

        public void SetStartPoint(int x, int y)
        {
            StartX = x;
            StartY = y;
        }

        public AiukEditorLayouter ToRight(int x)
        {
            StartX = StartX + SpacingX + x;
            return this;
        }

        public AiukEditorLayouter ToLeft(int x)
        {
            StartX -= x;
            return this;
        }

        public AiukEditorLayouter NextLine(int x)
        {
            StartX = x;
            StartY = StartY + LastHeight + SpacingY;
            return this;
        }

        public AiukEditorLayouter NextLine()
        {
            StartX = m_OriginalX;
            StartY = StartY + LastHeight + SpacingY;
            return this;
        }

        public AiukEditorLayouter ToDown(int y)
        {
            StartY += y;
            return this;
        }

        public AiukEditorLayouter ToUp(int y)
        {
            StartY += y;
            return this;
        }

        #endregion

        #region 控件

        #region Label

        public void LabelField(int width, int height, string title, GUIStyle style = null)
        {
            if (style == null)
            {
                style = EditorStyles.boldLabel;
            }

            EditorGUI.LabelField(GetRect(width, height), title, style);
            ToRight(width);
        }

        #endregion

        #region TextArea

        public string TextArea(int width, int heigth, string content,
                               GUIStyle style = null, Action<string> action = null)
        {
            if (style == null)
            {
                style = EditorStyles.textArea;
            }

            var last = content;
            content = EditorGUI.TextArea(GetRect(width, heigth), content, style);
            if (last != content && action != null)
            {
                action(content);
            }

            ToRight(width);
            return content;
        }

        #endregion

        #region Popup

        public int Popup(int width, int height, int index, string[] array)
        {
            var finalIndex = EditorGUI.Popup(GetRect(width, height), index, array);
            ToRight(width);
            return finalIndex;
        }

        #endregion

        #region Button

        public void Button(int width, int height, string content, Action action)
        {
            if (GUI.Button(GetRect(width, height), content))
            {
                if (action == null) return;

                action();
            }

            ToRight(width);
        }

        #endregion

        #endregion

    }
}

