using System.Collections.Generic;
using Aiuk.Common.PoolCache;
using AiukUnityRuntime.Core.DataPipeline;
using AiukUnityRuntime.View;
using UnityEngine;

namespace AiukUnityRuntime
{
    /// <summary>
    /// unity静态通用对象池。
    /// </summary>
    public static class AiukUnityPool
    {
        private static AiukObjectPool<AiukUIInteractiveData> _UIIneractiveDataPool;

        /// <summary>
        /// UI交互数据对象池，默认容量为50。
        /// </summary>
        public static AiukObjectPool<AiukUIInteractiveData> UIIneractiveDataPool
        {
            get
            {
                if (_UIIneractiveDataPool != null) return _UIIneractiveDataPool;

                _UIIneractiveDataPool = new AiukObjectPool<AiukUIInteractiveData>(() => new AiukUIInteractiveData(), 50);
                return _UIIneractiveDataPool;
            }
        }

        #region 视图列表对象池


        private static AiukObjectPool<List<IAiukImage>> _ImageListPool;

        /// <summary>
        /// 普通图片（精灵）接口列表对象池，默认容量为100。
        /// </summary>
        public static AiukObjectPool<List<IAiukImage>> ImageListPool
        {
            get
            {
                if (_ImageListPool != null) return _ImageListPool;

                _ImageListPool = new AiukObjectPool<List<IAiukImage>>(() => new List<IAiukImage>(), 20);
                return _ImageListPool;
            }
        }

        private static AiukObjectPool<List<IAiukRawImage>> _RawImageListPool;

        /// <summary>
        /// 原始图片（动态贴图）接口列表对象池，默认容量为100。
        /// </summary>
        public static AiukObjectPool<List<IAiukRawImage>> RawImageListPool
        {
            get
            {
                if (_RawImageListPool != null) return _RawImageListPool;

                _RawImageListPool = new AiukObjectPool<List<IAiukRawImage>>(() => new List<IAiukRawImage>(), 100);
                return _RawImageListPool;
            }
        }

        private static AiukObjectPool<List<IAiukButton>> _ButtonListPool;

        /// <summary>
        /// 按钮接口列表对象池，默认容量为100。
        /// </summary>
        public static AiukObjectPool<List<IAiukButton>> ButtonListPool
        {
            get
            {
                if (_ButtonListPool != null) return _ButtonListPool;

                _ButtonListPool = new AiukObjectPool<List<IAiukButton>>(() => new List<IAiukButton>(), 100);
                return _ButtonListPool;
            }
        }

        private static AiukObjectPool<List<IAiukToggle>> _ToggleListPool;

        /// <summary>
        /// 开关接口列表对象池，默认容量为20。
        /// </summary>
        public static AiukObjectPool<List<IAiukToggle>> ToggleListPool
        {
            get
            {
                if (_ToggleListPool != null) return _ToggleListPool;

                _ToggleListPool = new AiukObjectPool<List<IAiukToggle>>(() => new List<IAiukToggle>(), 50);
                return _ToggleListPool;
            }
        }

        private static AiukObjectPool<List<IAiukSlider>> _SliderListPool;

        /// <summary>
        /// 滑动器、进度条接口列表对象池，默认容量为20。
        /// </summary>
        public static AiukObjectPool<List<IAiukSlider>> SliderListPool
        {
            get
            {
                if (_SliderListPool != null) return _SliderListPool;

                _SliderListPool = new AiukObjectPool<List<IAiukSlider>>(() => new List<IAiukSlider>(), 20);
                return _SliderListPool;
            }
        }

        private static AiukObjectPool<List<IAiukInputField>> _InputFieldListPool;

        /// <summary>
        /// 输入框接口列表对象池，默认容量为20。
        /// </summary>
        public static AiukObjectPool<List<IAiukInputField>> InputFieldListPool
        {
            get
            {
                if (_InputFieldListPool != null) return _InputFieldListPool;

                _InputFieldListPool = new AiukObjectPool<List<IAiukInputField>>(() => new List<IAiukInputField>(), 20);
                return _InputFieldListPool;
            }
        }

        #endregion

        #region 基础控件对象池

        /// <summary>
        /// UI控件池根游戏对象名。
        /// </summary>
        private const string UI_CONTROL_POOL_NAME = "aiuk_control_pool";

        private static GameObject _UIControlPoolRoot;

        /// <summary>
        /// UI控件池根游戏对象
        /// </summary>
        private static GameObject UIControlPoolRoot
        {
            get
            {
                if (_UIControlPoolRoot != null) return _UIControlPoolRoot;

                _UIControlPoolRoot = new GameObject(UI_CONTROL_POOL_NAME);
                return _UIControlPoolRoot;
            }
        }

        /// <summary>
        /// 获取指定类型UI控件的对象池根游戏对象。
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static GameObject GetControlTypeRoot(AiukUIControlType type)
        {
            var typeRootName = "aiuk_" + type + "_pool";
            var targetRoot = GameObject.Find(typeRootName);
            if (targetRoot != null) return targetRoot;

            targetRoot = new GameObject(typeRootName);
            targetRoot.transform.SetParent(UIControlPoolRoot.transform);
            return targetRoot;
        }

        private static IAiukObjectPool<GameObject> _ButtonPool;

        public static IAiukObjectPool<GameObject> ButtonPool
        {
            get
            {
                if (_ButtonPool != null) return _ButtonPool;

                _ButtonPool = new AiukObjectPool<GameObject>(GetButton, 100);
                return _ButtonPool;
            }
        }

        private static GameObject GetButton()
        {
            var buttonGo = new GameObject();
            var buttonRoot = GetControlTypeRoot(AiukUIControlType.Button);
            buttonGo.transform.SetParent(buttonRoot.transform);
            buttonGo.AddComponent<AiukButton>();

            return buttonGo;
        }

        #endregion
    }
}
