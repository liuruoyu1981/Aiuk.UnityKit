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
    /// Http助手。
    /// unity3d下的Http工具，提供和Http相关的常用API。
    /// Mono单例。
    /// </summary>
    public class AiukHttpHelper : AiukAbsMonoSingleton<AiukHttpHelper>
    {
        /// <summary>
        /// 当前所有需要执行的异步操作状态检查帧函数。
        /// </summary>
        private readonly List<Action> m_UpdateActions = new List<Action>();

        /// <summary>
        /// Http异步操作上下文对象池。
        /// </summary>
        private readonly IAiukObjectPool<AiukHttpAsyncObject> m_HttpAsyncObjectPool
        = new AiukObjectPool<AiukHttpAsyncObject>(() => new AiukHttpAsyncObject(), 100);

        /// <summary>
        /// 回收Http异步操作上下文对象并移除其上的帧函数调用。
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

        #region 内部功能类

        /// <summary>
        /// 异步操作环境上下文封装类。
        /// </summary>
        private class AiukHttpAsyncObject
        {
            private UnityWebRequest m_WebRequest;
            private Action<float> m_OnProgressUpdate;

            private void ThrowHttpException(string url)
            {
                var exmessage = string.Format
                    ("下载目标URL：{0}时出错，错误信息为：{1}！", url, m_WebRequest.error);
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
