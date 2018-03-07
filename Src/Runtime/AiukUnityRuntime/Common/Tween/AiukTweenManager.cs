using System;
using System.Collections.Generic;
using UnityEngine;

namespace AiukUnityRuntime.Tween
{
    /// <summary>
    /// 动画管理器
    /// 不要直接添加动画脚本，而是在其他脚本中使用代码创建
    /// </summary>
    public class AiukTweenManager : MonoBehaviour
    {
        private static bool needsInitialize = true;
        private static GameObject root;
        private static readonly List<IAiukTween> tweens = new List<IAiukTween>();

        public static readonly Dictionary<AiukEaseType, Func<float, float>> scaleFuncDictionary = new Dictionary
<AiukEaseType, Func<float, float>>
        {
            { AiukEaseType.Linear, AiukTweenScaleFunctions.Linear},
            { AiukEaseType.QuadraticEaseIn, AiukTweenScaleFunctions.QuadraticEaseIn},
            { AiukEaseType.QuadraticEaseOut, AiukTweenScaleFunctions.QuadraticEaseOut},
            { AiukEaseType.QuadraticEaseInOut, AiukTweenScaleFunctions.QuadraticEaseInOut},
            { AiukEaseType.CubicEaseIn, AiukTweenScaleFunctions.CubicEaseIn},
            { AiukEaseType.CubicEaseOut, AiukTweenScaleFunctions.CubicEaseOut},
            { AiukEaseType.CubicEaseInOut, AiukTweenScaleFunctions.CubicEaseInOut},
            { AiukEaseType.QuarticEaseIn, AiukTweenScaleFunctions.QuadraticEaseIn},
            { AiukEaseType.QuarticEaseOut, AiukTweenScaleFunctions.QuarticEaseOut},
            { AiukEaseType.QuarticEaseInOut, AiukTweenScaleFunctions.QuarticEaseInOut},
            { AiukEaseType.QuinticEaseIn, AiukTweenScaleFunctions.QuinticEaseIn},
            { AiukEaseType.QuinticEaseOut, AiukTweenScaleFunctions.QuinticEaseOut},
            { AiukEaseType.QuinticEaseInOut, AiukTweenScaleFunctions.QuinticEaseInOut},
            { AiukEaseType.SineEaseIn, AiukTweenScaleFunctions.SineEaseIn},
            { AiukEaseType.SineEaseOut, AiukTweenScaleFunctions.SineEaseOut},
            { AiukEaseType.SineEaseInOut, AiukTweenScaleFunctions.SineEaseInOut},
        };

        private static void EnsureCreated()
        {
            if (!needsInitialize) return;

            needsInitialize = false;
            root = new GameObject
            {
                name = "DigitalRubyTween",
                hideFlags = HideFlags.HideAndDontSave
            };

            root.AddComponent<AiukTweenManager>();
            DontDestroyOnLoad(root);
        }

        private void Start()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneManagerSceneLoaded;
        }

        private void SceneManagerSceneLoaded(UnityEngine.SceneManagement.Scene s,
            UnityEngine.SceneManagement.LoadSceneMode m)
        {
            tweens.Clear();
        }

        /// <summary>
        /// 更新所有动画对象
        /// </summary>
        private void Update()
        {
            for (var i = tweens.Count - 1; i >= 0; i--)
            {
                var tween = tweens[i];
                var check1 = tween.Update(Time.deltaTime);
                var check2 = i < tweens.Count;
                var check3 = tween == tweens[i];

                if (!check1 || !check2 || !check3) continue;

                var t = tweens[i];
                t.Reset();  // 重置动画对象
                tweens.RemoveAt(i);
                AiukTweenFactory.Restore(tween);
            }
        }

        public static AiukFloatTween Tween(object key, float start, float end, float duration,
            Action<IAiukTween<float>> progress,
            Action<IAiukTween<float>>
            completion = null,
            AiukEaseType aiukEaseType = AiukEaseType.Linear,
            string friendName = null)
        {
            var t = AiukTweenFactory.GetFloatTween();

            // 给动画对象赋一个友好的命名如果存在
            if (friendName == null)
            {
                var id = TweenIdCounter + 1;
                t.FriendNme = "Tween_" + id;
            }
            else
            {
                t.FriendNme = friendName;
            }
            t.Key = key;
            var scaleFunc = scaleFuncDictionary[aiukEaseType];
            t.Init(start, end, duration, scaleFunc, progress, completion);
            AddTween(t);

            return t;
        }

        /// <summary>
        /// Created and add a Vector2 tween
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="start">Created value</param>
        /// <param name="end">End value</param>
        /// <param name="duration">CostTime in seconds</param>
        /// <param name="scaleFunc">Scale function</param>
        /// <param name="progress">Progress handler</param>
        /// <param name="completion">Completion handler</param>
        /// <returns>Vector2Tween</returns>
        public static AiukVector2Tween Tween(object key, Vector2 start, Vector2 end, float duration, Func<float, float> scaleFunc, Action<IAiukTween<Vector2>> progress, Action<IAiukTween<Vector2>> completion)
        {
            var t = AiukTweenFactory.GetVector2Tween();
            t.Key = key;
            t.Init(start, end, duration, scaleFunc, progress, completion);
            AddTween(t);

            return t;
        }

        /// <summary>
        /// 动画动画编号计数器。
        /// 用于自动为动画实例生成编号。
        /// </summary>
        private static int TweenIdCounter;

        /// <summary>
        /// 开启一个Vector3动画
        /// 需要传递一个游戏对象
        /// </summary>
        /// <param name="key">动画key</param>
        /// <param name="start">动画开始值</param>
        /// <param name="end">动画结束值</param>
        /// <param name="duration">过渡时间</param>
        /// <param name="progress">动画进度更新委托</param>
        /// <param name="completion">完成委托</param>
        /// <param name="aiukEaseType">动画曲线类型</param>
        /// <param name="friendName">对人友好的动画名</param>
        /// <returns></returns>
        public static AiukVector3Tween Tween(object key, Vector3 start, Vector3 end, float duration, Action<IAiukTween<Vector3>> progress, Action<IAiukTween<Vector3>>
            completion = null, AiukEaseType aiukEaseType = AiukEaseType.Linear, string friendName = null)
        {
            var t = AiukTweenFactory.GetVector3Tween();

            // 给动画对象赋一个友好的命名如果存在
            if (friendName == null)
            {
                var id = TweenIdCounter + 1;
                t.FriendNme = "Tween_" + id;
            }
            else
            {
                t.FriendNme = friendName;
            }
            t.Key = key;
            var scaleFunc = scaleFuncDictionary[aiukEaseType];
            t.Init(start, end, duration, scaleFunc, progress, completion);
            AddTween(t);

            return t;
        }

        /// <summary>
        /// Created and add a Vector4 tween
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="start">Created value</param>
        /// <param name="end">End value</param>
        /// <param name="duration">CostTime in seconds</param>
        /// <param name="scaleFunc">Scale function</param>
        /// <param name="progress">Progress handler</param>
        /// <param name="completion">Completion handler</param>
        /// <returns>Vector4Tween</returns>
        public static AiukVector4Tween Tween(object key, Vector4 start, Vector4 end, float duration, Func<float, float> scaleFunc, Action<IAiukTween<Vector4>> progress, Action<IAiukTween<Vector4>> completion)
        {
            var t = AiukTweenFactory.GetVector4Tween();
            t.Key = key;
            t.Init(start, end, duration, scaleFunc, progress, completion);
            AddTween(t);

            return t;
        }

        /// <summary>
        /// 启动一个颜色动画
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start">Created value</param>
        /// <param name="end">End value</param>
        /// <param name="duration">CostTime in seconds</param> 
        /// <param name="progress">Progress handler</param>
        /// <param name="completion">Completion handler</param>
        /// <param name="aiukEaseType"></param>
        /// <returns>ColorTween</returns>
        public static AiukColorTween Tween(object key, Color start, Color end, float duration, Action<IAiukTween<Color>> progress, Action<IAiukTween<Color>> completion, AiukEaseType aiukEaseType = AiukEaseType.Linear)
        {
            var t = AiukTweenFactory.GetColorTween();
            t.Key = key;
            var scaleFunc = scaleFuncDictionary[aiukEaseType];
            t.Init(start, end, duration, scaleFunc, progress, completion);
            AddTween(t);

            return t;
        }

        /// <summary>
        /// 启动一个QuaternionTween
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="duration"></param>
        /// <param name="progress"></param>
        /// <param name="completion"></param>
        /// <param name="aiukEaseType"></param>
        /// <param name="friendName"></param>
        /// <returns></returns>
        public static AiukQuaternionTween QuaternionTween(object key, Quaternion start, Quaternion end, float duration, Action<IAiukTween<Quaternion>> progress, Action<IAiukTween<Quaternion>> completion, AiukEaseType aiukEaseType = AiukEaseType.Linear, string friendName = null)
        {
            var tween = AiukTweenFactory.GetQuaternionTween();
            // 给动画对象赋一个友好的命名如果存在
            if (friendName == null)
            {
                var id = TweenIdCounter + 1;
                tween.FriendNme = "Tween_" + id;
            }
            else
            {
                tween.FriendNme = friendName;
            }
            tween.Key = key;
            var scaleFunc = scaleFuncDictionary[aiukEaseType];
            tween.Init(start, end, duration, scaleFunc, progress, completion);
            AddTween(tween);

            return tween;
        }

        /// <summary>
        /// 添加一个动画
        /// </summary>
        /// <param name="aiukTween"></param>
        private static void AddTween(IAiukTween aiukTween)
        {
            EnsureCreated();
            if (aiukTween.Key != null)
            {
                RemoveTweenKey(aiukTween.Key, _addKeyStopBehavior);
            }
            tweens.Add(aiukTween);
        }

        /// <summary>
        /// 移除一个动画
        /// </summary>
        /// <param name="aiukTween"></param>
        /// <param name="stopBehavior"></param>
        /// <returns></returns>
        public static bool RemoveTween(IAiukTween aiukTween, AiukTweenStopBehavior stopBehavior)
        {
            aiukTween.Stop(stopBehavior);
            return tweens.Remove(aiukTween);
        }

        /// <summary>
        /// 通过key对象移除一个动画
        /// </summary>
        /// <param name="key"></param>
        /// <param name="stopBehavior"></param>
        /// <returns></returns>
        private static bool RemoveTweenKey(object key, AiukTweenStopBehavior stopBehavior)
        {
            if (key == null)
            {
                return false;
            }

            var foundOne = false;
            for (var i = tweens.Count - 1; i >= 0; i--)
            {
                var t = tweens[i];
                if (!key.Equals(t.Key)) continue;

                t.Stop(stopBehavior);
                tweens.RemoveAt(i);
                foundOne = true;
            }
            return foundOne;
        }

        private static AiukTweenStopBehavior _addKeyStopBehavior = AiukTweenStopBehavior.DoNotModify;


    }
}
