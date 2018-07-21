using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LightingCat
{
    /// <summary>
    /// 等待接口
    /// </summary>
    public interface IAwait
    {
        /// <summary>
        /// 是否准备完成
        /// </summary>
        bool IsDone { get; }

        /// <summary>
        /// 实现
        /// </summary>
        object Result { get; }
    }
}