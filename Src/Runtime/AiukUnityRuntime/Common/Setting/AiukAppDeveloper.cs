using System;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 应用开发者。
    /// 提供开发者的名字、邮箱及备注。
    /// 1. 用于支持在运行时环境下发送游戏运行信息、异常信息等邮件。
    /// 2. 用于在自动化脚本工具创建时自动插入当前开发者的注释信息。
    /// </summary>
    [Serializable]
    public class AiukAppDeveloper
    {
        public readonly string Name;
        public readonly string Email;

        public AiukAppDeveloper(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}

