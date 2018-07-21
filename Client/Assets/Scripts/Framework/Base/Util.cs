using System;
using System.Reflection;
using System.Threading;

namespace LightingCat
{
    public static class Util
    {
        /// <summary>
        /// 等待完成
        /// </summary>
        /// <param name="wait"></param>
        /// <param name="maxWaitTimeMs"></param>
        /// <param name="sleep"></param>
        /// <returns></returns>
        public static bool Wait(IAwait wait, int maxWaitTimeMs, int sleep = 1)
        {
            var time = 0;
            while (!wait.IsDone && time < maxWaitTimeMs)
            {
                Thread.Sleep(sleep);
                time += sleep;
            }

            return wait.IsDone;
        }

        public static bool Wait<T>(ref T[] arr, int maxWaitTimeMs, int sleep = 1)
        {
            var time = 0;
            while (arr != null && arr.Length <= 0 && time < maxWaitTimeMs)
            {
                Thread.Sleep(sleep);
                time += sleep;
            }

            return arr != null && arr.Length > 0;
        }

        /// <summary>
        /// 构建一个随机生成器
        /// </summary>
        /// <param name="seed">种子</param>
        /// <returns>随机生成器</returns>
        public static System.Random MakeRandom(int? seed = null)
        {
            return new System.Random(seed.GetValueOrDefault(MakeSeed()));
        }

        /// <summary>
        /// 生成种子
        /// </summary>
        /// <returns>种子</returns>
        public static int MakeSeed()
        {
            return Environment.TickCount ^ Guid.NewGuid().GetHashCode();
        }

        /// <summary>
        /// 标准化位置
        /// </summary>
        /// <param name="sourceLength">源长度</param>
        /// <param name="start">起始位置</param>
        /// <param name="length">作用长度</param>
        internal static void NormalizationPosition(int sourceLength, ref int start, ref int? length)
        {
            start = (start >= 0) ? Math.Min(start, sourceLength) : Math.Max(sourceLength + start, 0);

            length = (length == null)
                ? Math.Max(sourceLength - start, 0)
                : (length >= 0)
                    ? Math.Min(length.Value, sourceLength - start)
                    : Math.Max(sourceLength + length.Value - start, 0);
        }
    }
}