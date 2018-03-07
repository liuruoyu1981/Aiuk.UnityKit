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
    /// ���ַ���
    /// </summary>
    public class AiukMusicService : IAiukMusicService
    {
        private readonly AiukMusicPlayComponent m_PlayComponent
            = AiukMusicPlayComponent.Instance;

        private string m_LastMusicName;

        /// <summary>
        /// ������Դ�����ֵ䡣
        /// </summary>
        private readonly Dictionary<string, AiukMusicRef> m_MusicRefs
        = new Dictionary<string, AiukMusicRef>();

        #region �ڲ������

        private class AiukMusicPlayComponent : AiukAbsMonoSingleton<AiukMusicPlayComponent>
        {
            private AudioSource m_AudioSource;

            /// <summary>
            /// ���ֲ�����ʹ�õ���ƵԴ�����
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
