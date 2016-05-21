using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ------------------------------------------------------------ //
// 版权所有：CopyRight (C) LanwahSoft
// 项目名称：Lanwah.CSharp.NET.sln
// 文件名称：Kernel32.cs
// 创 建 者：Lanwah
// 创建日期：2016-05-21
// 功能描述：Win32 Kernel32 API
// 调用依赖：
// -------------------------------------------------------------
// 修 改 者：
// 修改时间：
// 修改原因：
// 修改描述：
// ------------------------------------------------------------ //

using System.Runtime.InteropServices;

namespace Lanwah.CSharp.NET.Win32Lib
{
    /// <summary>
    /// Win32 Kernel32 API
    /// </summary>
    public static partial class Kernel32
    {

        // G

        /// <summary>
        /// 该函数返回调用线程最近的错误代码值，错误代码以单线程为基础来维护的，多线程不重写各自的错误代码值。
        /// </summary>
        /// <link>https://msdn.microsoft.com/en-us/library/windows/desktop/ms679360(v=vs.85).aspx</link>
        /// <linkalso>http://baike.baidu.com/view/1730168.htm?fr=aladdin</linkalso>
        /// <returns>返回值为调用的线程的错误代码值(unsigned long)，函数通过调 SetLastError 函数来设置此值，每个函数资料的返回值部分都注释了函数设置错误代码的情况。</returns>
        [DllImport("Kernel32.dll")]
        public static extern UInt32 GetLastError();

        // Q

        /// <summary>
        /// 用于得到高精度计时器的值(如果存在这样的计时器)
        /// </summary>
        /// <param name="performanceCount">指向现时计数器的值.如果安装的硬件不支持高精度计时器,该参数将返回0（输出参数）</param>
        /// <link>https://msdn.microsoft.com/en-us/library/windows/desktop/ms644904(v=vs.85).aspx</link>
        /// <linkalso>http://baike.baidu.com/link?url=orq-Nu0ORaUmxrPg4Gcnc7gJ0A1KcVvs7gQ2szdUy9Ej1cHelq3D792B23Xd_P1CwmRH_L4YgplCcIIt-zNOza</linkalso>
        /// <returns>true： 硬件支持高精度计数器；false： 硬件不支持，读取失败</returns>
        [DllImport("Kernel32.dll", EntryPoint = "QueryPerformanceCounter")]
        public static extern bool QueryPerformanceCounter(out Int64 performanceCount);
        /// <summary>
        /// 返回硬件支持的高精度计数器的频率
        /// </summary>
        /// <param name="frequency">高精度计数器每秒的计数值（输出参数）</param>
        /// <link>https://msdn.microsoft.com/en-us/library/windows/desktop/ms644905(v=vs.85).aspx</link>
        /// <linkalso>http://www.baike.com/wiki/QueryPerformanceFrequency()</linkalso>
        /// <returns>true： 硬件支持高精度计数器；false： 硬件不支持，读取失败</returns>
        [DllImport("Kernel32.dll")]
        public static extern bool QueryPerformanceFrequency(out Int64 frequency);
    }
}
