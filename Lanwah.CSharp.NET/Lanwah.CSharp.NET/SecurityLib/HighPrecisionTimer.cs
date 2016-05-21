using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ------------------------------------------------------------ //
// 版权所有：CopyRight (C) LanwahSoft
// 项目名称：Lanwah.CSharp.NET.sln
// 文件名称：HighPrecisionTimer.cs
// 创 建 者：Lanwah
// 创建日期：2016-05-21
// 功能描述：高精度计时器
// 调用依赖：
// -------------------------------------------------------------
// 修 改 者：
// 修改时间：
// 修改原因：
// 修改描述：
// ------------------------------------------------------------ //

namespace Lanwah.CSharp.NET.SecurityLib
{
    /// <summary>
    /// 高精度计时器
    /// </summary>
    public sealed partial class HighPrecisionTimer
    {
        #region // ============== Fields ============== //
        /// <summary>
        /// 以每秒刻度数表示的计时器频率
        /// </summary>
        private long _frequence;
        #endregion

        #region // ============= Property ============= //
        /// <summary>
        /// 开始刻度
        /// </summary>
        private Int64 StartTime;
        /// <summary>
        /// 结束刻度
        /// </summary>
        private Int64 StopTime;
        /// <summary>
        /// 获取以每秒刻度数表示的计时器频率。此字段为只读。
        /// </summary>
        public long Frequence
        {
            get { return this._frequence; }
            private set { this._frequence = value; }
        }
        /// <summary>
        /// 一个只读长整型，表示当前实例测量得出的计时器刻度的总数。使用 Frequency 字段可以将 ElapsedTicks 值转换为秒数。
        /// </summary>
        public long ElapsedTicks
        {
            get
            {
                return (this.StopTime - this.StartTime);
            }
        }
        /// <summary>
        /// 获取当前实例测量得出的总运行时间（以毫秒为单位）。
        /// </summary> 
        public long ElapsedMilliseconds
        {
            get
            {
                return (long)Math.Round((double)(ElapsedTicks) * 1000 / (double)this.Frequence);
            }
        }
        /// <summary>
        /// 获取当前实例测量得出的总运行时间（以微秒为单位）。
        /// </summary>
        public long ElapsedMicroseconds
        {
            get
            {
                return (long)Math.Round((double)(ElapsedTicks) * 1000000 / (double)this.Frequence);
            }
        }
        /// <summary>
        /// 获取当前实例测量得出的总运行时间（以纳秒为单位）。
        /// </summary>
        public long ElapsedNanoseconds
        {
            get
            {
                return (long)Math.Round((double)(ElapsedTicks) * 1000000000 / (double)this.Frequence);
            }
        }
        /// <summary>
        /// 获取当前实例测量得出的总运行时间（以皮秒为单位）。
        /// </summary>
        public long ElapsedPicoseconds
        {
            get
            {
                return (long)Math.Round((double)(ElapsedTicks) * 1000000000000 / (double)this.Frequence);
            }
        }
        #endregion
        
        #region // ======= Constructors Methods ======= //
        /// <summary>
        /// 构造函数
        /// </summary> 
        public HighPrecisionTimer()
        {
            this.StartTime = 0;
            this.StopTime = 0;
            long Freq = 0;
            if (Win32Lib.Kernel32.QueryPerformanceFrequency(out Freq) == false)
            {
                //不支持高性能计时器  
                throw new System.ComponentModel.Win32Exception();
            }
            else
            {
                this.Frequence = Freq;
            }
        }
        #endregion

        #region // =========== Class Methods ========== //
        /// <summary>
        /// 重置计时参数
        /// </summary>
        public void Reset()
        {
            this.StartTime = 0;
            this.StopTime = 0;
        }
        /// <summary>
        /// 开始计时 
        /// </summary>
        public void Start()
        {
            //让等待线程工作 
            System.Threading.Thread.Sleep(0);
            Win32Lib.Kernel32.QueryPerformanceCounter(out this.StartTime);
        }
        /// <summary>
        /// 结束计时 
        /// </summary>
        public void Stop()
        {
            Win32Lib.Kernel32.QueryPerformanceCounter(out this.StopTime);
        }
        #endregion
    }
}
