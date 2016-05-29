using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

// ------------------------------------------------------------ //
// 版权所有：CopyRight (C) LanwahSoft
// 项目名称：Lanwah.CSharp.NET.sln
// 文件名称：Advapi32.cs
// 创 建 者：Lanwah
// 创建日期：2016-05-29
// 功能描述：Advapi32 Win32 API
// 调用依赖：
// -------------------------------------------------------------
// 修 改 者：
// 修改时间：
// 修改原因：
// 修改描述：
// ------------------------------------------------------------ //

namespace Lanwah.CSharp.NET.Win32Lib
{
    /// <summary>
    /// Advapi32 Win32 API
    /// </summary>
    public sealed partial class Advapi32
    {
        // Q


        /// <summary>
        /// 查询服务相关信息
        /// </summary>
        /// <param name="service"></param>
        /// <param name="infoLevel"></param>
        /// <param name="buffer"></param>
        /// <param name="bufSize"></param>
        /// <param name="bytesNeeded"></param>
        /// <link>https://msdn.microsoft.com/en-us/library/windows/desktop/ms684935(v=vs.85).aspx</link>
        /// <returns></returns>
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("Advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool QueryServiceConfig2(SafeHandle service, int infoLevel, IntPtr buffer, int bufSize, ref int bytesNeeded);
    }
}
