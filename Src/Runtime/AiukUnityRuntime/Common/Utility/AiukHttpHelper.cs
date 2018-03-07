#region Head

// Author:        liuruoyu1981
// CreateDate:    3/6/2018 3:50:13 PM
// Email:         liuruoyu1981@gmail.com

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Aiuk.Common.PoolCache;
using Aiuk.Common.Utility;
using UnityEngine.Networking;

namespace AiukUnityRuntime
{
    /// <summary>
    /// Http���֡�
    /// unity3d�µ�Http���ߣ��ṩ��Http��صĳ���API��
    /// Mono������
    /// </summary>
    public class AiukHttpHelper : AiukAbsMonoSingleton<AiukHttpHelper>
    {
        /// <summary>
        /// ��ǰ������Ҫִ�е��첽����״̬���֡������
        /// </summary>
        private readonly List<Action> m_UpdateActions = new List<Action>();

        /// <summary>
        /// Http�첽���������Ķ���ء�
        /// </summary>
        private readonly IAiukObjectPool<AiukHttpAsyncObject> m_HttpAsyncObjectPool
        = new AiukObjectPool<AiukHttpAsyncObject>(() => new AiukHttpAsyncObject(), 100);

        /// <summary>
        /// ����Http�첽���������Ķ����Ƴ����ϵ�֡�������á�
        /// </summary>
        /// <param name="asyncObject"></param>
        private void RecycleHttpAsyncObject(AiukHttpAsyncObject asyncObject)
        {
            m_UpdateActions.Remove(asyncObject.Update);
            m_HttpAsyncObjectPool.Restore(asyncObject);
        }

        private void Update()
        {
            m_UpdateActions.ForEach(del => del());
        }

        private void PushUpdateAction(Action updateAction)
        {
            m_UpdateActions.Add(updateAction);
        }

        #region Get

        private static void GetByteArray
        (
            string url,
            Action<byte[]> onCompleted,
            Action<float> onProgressUpdate = null
        )
        {
            var asyncObj = Instance.m_HttpAsyncObjectPool.Take();
            Instance.StartCoroutine(asyncObj.GetArrayByte(url, onCompleted, onProgressUpdate));
            Instance.PushUpdateAction(asyncObj.Update);
        }

        public static void GetFile
        (
            string url,
            string local,
            Action onCompleted = null,
            Action<float> onProgressUpdate = null
        )
        {
            AiukIOUtility.EnsureDirExist(local);
            GetByteArray(url, bytes =>
             {
                 AiukIOUtility.WriteAllBytes(local, bytes);
                 if (onCompleted != null)
                 {
                     onCompleted();
                 }
             }, onProgressUpdate);
        }

        #endregion

        #region �ڲ�������

        /// <summary>
        /// �첽�������������ķ�װ�ࡣ
        /// </summary>
        private class AiukHttpAsyncObject
        {
            private UnityWebRequest m_WebRequest;
            private Action<float> m_OnProgressUpdate;

            private void ThrowHttpException(string url)
            {
                var exmessage = string.Format
                    ("����Ŀ��URL��{0}ʱ����������ϢΪ��{1}��", url, m_WebRequest.error);
                throw new AiukHttpException(exmessage);
            }

            public IEnumerator GetArrayByte
            (
                string url,
                Action<byte[]> onCompleted,
                Action<float> onProgressUpdate = null
            )
            {
                m_OnProgressUpdate = onProgressUpdate;
                m_WebRequest = UnityWebRequest.Get(url);
                yield return m_WebRequest.SendWebRequest();

                if (!string.IsNullOrEmpty(m_WebRequest.error))
                {
                    ThrowHttpException(url);
                }

                if (onCompleted != null)
                {
                    onCompleted(m_WebRequest.downloadHandler.data);
                }

                Instance.RecycleHttpAsyncObject(this);
            }

            public IEnumerator GetFile
            (
                string url,
                string local,
                Action completed
            )
            {
                m_WebRequest = UnityWebRequest.Get(url);
                yield return m_WebRequest.SendWebRequest();

                if (!string.IsNullOrEmpty(m_WebRequest.error))
                {
                    ThrowHttpException(url);
                }

                File.WriteAllBytes(local, m_WebRequest.downloadHandler.data);
                completed();
            }

            public IEnumerator GetString
            (
                string url,
                Action<string> onCompleted
            )
            {
                m_WebRequest = UnityWebRequest.Get(url);
                yield return m_WebRequest.SendWebRequest();

                if (!string.IsNullOrEmpty(m_WebRequest.error))
                {
                    ThrowHttpException(url);
                }

                if (onCompleted != null)
                {
                    onCompleted(m_WebRequest.downloadHandler.text);
                }

                Instance.RecycleHttpAsyncObject(this);
            }

            public IEnumerator Put
            (
                string url,
                byte[] data,
                Action onPuted
            )
            {
                m_WebRequest = UnityWebRequest.Put(url, data);
                yield return m_WebRequest.SendWebRequest();

                if (!string.IsNullOrEmpty(m_WebRequest.error))
                {
                    ThrowHttpException(url);
                }

                if (onPuted != null)
                {
                    onPuted();
                }

                Instance.RecycleHttpAsyncObject(this);
            }

            public void Update()
            {
                if (m_OnProgressUpdate != null)
                {
                    m_OnProgressUpdate(m_WebRequest.downloadProgress);
                }
            }
        }
        #endregion
    }
}
