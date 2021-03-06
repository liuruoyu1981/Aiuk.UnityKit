﻿using System.IO;
using System.Reflection;
using UnityEngine;

namespace AiukUnityRuntime
{
    /// <summary>
    /// unity工具，提供公用的常用API。
    /// </summary>
    public static class AiukUnityUtility
    {
        /// <summary>
        /// 当前是否处于编辑器下。
        /// </summary>
        /// <value><c>true</c> if is editor mode; otherwise, <c>false</c>.</value>
        public static bool IsEditorMode
        {
            get
            {
                if (
                    Application.platform == RuntimePlatform.LinuxEditor
                    || Application.platform == RuntimePlatform.OSXEditor
                    || Application.platform == RuntimePlatform.WindowsEditor
                   )
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// 当前是否处于可用的设备上。
        /// </summary>
        /// <value><c>true</c> if is player; otherwise, <c>false</c>.</value>
        public static bool IsPlayer
        {
            get
            {
                if
                (
                   Application.platform == RuntimePlatform.Android
                   || Application.platform == RuntimePlatform.IPhonePlayer
                   || Application.platform == RuntimePlatform.OSXPlayer
                   || Application.platform == RuntimePlatform.LinuxPlayer
                   || Application.platform == RuntimePlatform.WindowsPlayer
                )
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// 当前项目程序集文件路径
        /// </summary>
        public static string AssemblyCSharpPath
        {
            get
            {
                return Directory.GetParent(Application.dataPath) +
                       "/Library/ScriptAssemblies/Assembly-CSharp.dll";
            }
        }

        public static Assembly AssemblyCSharp
        {
            get
            {
                var asm = Assembly.LoadFile(AssemblyCSharpPath);
                return asm;
            }
        }

        public static Assembly AssemblyCSharpEditor
        {
            get
            {
                var asm = Assembly.LoadFile(Directory.GetParent(Application.dataPath) +
                                            "/Library/ScriptAssemblies/Assembly-CSharp-Editor.dll");
                return asm;
            }
        }

    }
}

