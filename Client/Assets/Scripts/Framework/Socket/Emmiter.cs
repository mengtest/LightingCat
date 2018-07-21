
using System;
using System.Collections.Generic;

namespace LightingCat
{
    /// <summary>
    /// 发射器
    /// </summary>
    internal sealed class Emmiter
    {
        /// <summary>
        /// 发射器
        /// </summary>
        private readonly IDictionary<string, List<Action<object>>> emmiters;

        /// <summary>
        /// 构建一个发射器
        /// </summary>
        public Emmiter()
        {
            emmiters = new Dictionary<string, List<Action<object>>>();
        }

        /// <summary>
        /// 注册一条事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="callback">回调</param>]
        public void On(string eventName, Action<object> callback)
        {
            if (eventName == null || callback == null)
                return;
            List<Action<object>> emmiter;
            if (!emmiters.TryGetValue(eventName, out emmiter))
            {
                emmiters[eventName] = emmiter = new List<Action<object>>();
            }
            emmiter.Add(callback);
        }

        /// <summary>
        /// 反注册一条事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="callback">回调</param>
        public void Off(string eventName, Action<object> callback)
        {
            if (eventName == null || callback == null)
                return;
            List<Action<object>> emmiter;
            if (!emmiters.TryGetValue(eventName, out emmiter))
            {
                return;
            }

            emmiter.RemoveAll((o) => o == callback);
            if (emmiter.Count <= 0)
            {
                emmiters.Remove(eventName);
            }
        }

        /// <summary>
        /// 触发一个Socket事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="payload">载荷</param>
        public void Trigger(string eventName, object payload = null)
        {
            if (eventName == null)
                return;
            List<Action<object>> emmiter;
            if (!emmiters.TryGetValue(eventName, out emmiter))
            {
                return;
            }
            foreach (var action in emmiter)
            {
                action.Invoke(payload);
            }
        }
    }
}
