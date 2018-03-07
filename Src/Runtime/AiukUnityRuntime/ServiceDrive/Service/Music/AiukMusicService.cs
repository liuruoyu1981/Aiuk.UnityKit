using System.Collections.Generic;
using AiukUnityRuntime;
using UnityEngine;

 
 

#region Head

// Author:        liuruoyu1981
// CreateDate:    3/6/2018 7:51:58 AM
// Email:         liuruoyu1981@gmail.com

#endregion

namespace AiukRuntime
{
    /// <summary>
    /// 音乐服务。
    /// </summary>
    public class AiukMusicService : IAiukMusicService
    {
        private readonly AiukMusicPlayComponent m_PlayComponent
            = AiukMusicPlayComponent.Instance;

        private string m_LastMusicName;

        /// <summary>
        /// 音乐资源引用字典。
        /// </summary>
        private readonly Dictionary<string, AiukMusicRef> m_MusicRefs
        = new Dictionary<string, AiukMusicRef>();

        #region 内部组件类

        private class AiukMusicPlayComponent : AiukAbsMonoSingleton<AiukMusicPlayComponent>
        {
            private AudioSource m_AudioSource;

            /// <summary>
            /// 音乐播放所使用的音频源组件。
            /// </summary>
            public AudioSource AudioSource
            {
                get
                {
                    if (m_AudioSource != null) return m_AudioSource;

                    m_AudioSource = GetComponent<AudioSource>();
                    return m_AudioSource;
                }
            }
        }

        #endregion
    }
}
