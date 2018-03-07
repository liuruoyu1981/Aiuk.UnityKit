using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using Aiuk.Common.Utility;
using AiukUnityEditor;
using AiukUnityRuntime;

namespace DeadMosquito.Revealer
{
    /// <summary>
    /// Assets窗口扩展。
    /// 1. 快速刷新（点击附加按钮直接调用框架刷新功能。）
    ///     a. 刷新框架必备的脚本项、创建框架必备的资源项。
    ///     b. 刷新当前App（开发项目）的必备脚本即资源项。
    /// </summary>
    [InitializeOnLoad]
    public static class AiukAssetsExtension
    {
        private static Texture2D _darkSkinTex;
        private static Texture2D _lightSkinTex;
        private static Texture2D _lightRefreshTex;
        private static readonly float IconSize = EditorGUIUtility.singleLineHeight;
        private static List<IAiukEditorAssetsFuction> AssetsFunctions
        = new List<IAiukEditorAssetsFuction>();

        private const int Offset = 1;

        private static void InitAssetsFunction()
        {
            var functionTypes =
                AiukReflectUtility.GetTypeList<IAiukEditorAssetsFuction>
                    (false, false, AiukUnityUtility.AssemblyCSharpEditor);
            foreach (var functionType in functionTypes)
            {
                var function = Activator.CreateInstance(functionType) as IAiukEditorAssetsFuction;
                AssetsFunctions.Add(function);
            }

            AssetsFunctions = AssetsFunctions.OrderBy(f => f.MenuTitle).ToList();
        }

        static AiukAssetsExtension()
        {
            InitAssetsFunction();
            LoadTextures();

            EditorApplication.projectWindowItemOnGUI += HandleProjectWindowItemOnGUI;
        }

        private static void LoadTextures()
        {
            _darkSkinTex = AssetDatabase.LoadAssetAtPath<Texture2D>
                ("Assets/Aiuk.UnityKit/Src/Editor/AiukUnityEditor/AssetsExtension/Asset/reveal-light.png");
            _lightSkinTex = AssetDatabase.LoadAssetAtPath<Texture2D>
                ("Assets/Aiuk.UnityKit/Src/Editor/AiukUnityEditor/AssetsExtension/Asset/reveal-dark.png");
            _lightRefreshTex = AssetDatabase.LoadAssetAtPath<Texture2D>
                ("Assets/Aiuk.UnityKit/Src/Editor/AiukUnityEditor/AssetsExtension/Asset/refresh_light.png");
        }

        private static void HandleProjectWindowItemOnGUI(string guid, Rect rect)
        {
            AddRefreshIcon(guid, rect);
            AddRefresh2Icon(guid, rect);
            AddOpenFolder(guid, rect);
            EditorApplication.RepaintProjectWindow();
        }

        private static void AddOpenFolder(string guid, Rect rect)
        {
            var isHover = rect.Contains(Event.current.mousePosition) && RevealerEditorSettings.ShowOnHover;
            var isSelected = IsSelected(guid) && RevealerEditorSettings.ShowOnSelected;

            var isVisible = isHover || isSelected;

            if (!isVisible)
            {
                return;
            }

            var iconSize = EditorGUIUtility.singleLineHeight;
            var iconRect = new Rect(rect.width + rect.x - iconSize - RevealerEditorSettings.OffsetInProjectView, rect.y,
                iconSize - Offset, iconSize - Offset);

            GUI.DrawTexture(iconRect, GetTex());

            var path = AssetDatabase.GUIDToAssetPath(guid);
            if (GUI.Button(iconRect, GUIContent.none, GUIStyle.none))
            {
                EditorUtility.RevealInFinder(path);
            }
        }

        private static void AddRefreshIcon(string guid, Rect rect)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var iconRect = new Rect(rect.width + rect.x - IconSize - 225, rect.y,
                IconSize - Offset, IconSize - Offset);

            if (!path.EndsWith("Aiuk.UnityKit")) return;

            GUI.DrawTexture(iconRect, _lightRefreshTex);
            if (GUI.Button(iconRect, GUIContent.none, GUIStyle.none))
            {
                var menu = new GenericMenu();

                foreach (var assetsFuction in AssetsFunctions)
                {
                    menu.AddItem(new GUIContent(assetsFuction.MenuTitle), false, Callback, "random");
                }
//                menu.AddItem(new GUIContent("快捷/打开沙盒"), false, Callback, "item 1");
//                menu.AddItem(new GUIContent("MenuItem2"), false, Callback, "item 2");
//                menu.AddItem(new GUIContent("MenuItem2"), false, Callback, "item 2");
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("SubMenu/MenuItem3"), false, Callback, "item 3");
                menu.ShowAsContext();
            }
        }

        private static void AddRefresh2Icon(string guid, Rect rect)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var iconRect = new Rect(rect.width + rect.x - IconSize - 208, rect.y,
                IconSize - Offset, IconSize - Offset);

            if (!path.EndsWith("Aiuk.UnityKit")) return;

            GUI.DrawTexture(iconRect, _lightRefreshTex);
            if (GUI.Button(iconRect, GUIContent.none, GUIStyle.none))
            {
            }
        }

        private static void Callback(object obj)
        {
            Debug.Log("Selected: " + obj);
        }

        private static Texture2D GetTex()
        {
            if (_darkSkinTex == null || _lightSkinTex == null)
            {
                LoadTextures();
            }

            return EditorGUIUtility.isProSkin ? _darkSkinTex : _lightSkinTex;
        }

        private static bool IsSelected(string guid)
        {
            return Selection.assetGUIDs.Any(guid.Contains);
        }

    }
}