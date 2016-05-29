using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

// ------------------------------------------------------------ //
// 版权所有：CopyRight (C) LanwahSoft
// 项目名称：Lanwah.CSharp.NET.sln
// 文件名称：ProcessControler.cs
// 创 建 者：Lanwah
// 创建日期：2016-05-29
// 功能描述：进程信息相关类
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
    /// 进程信息相关类
    /// </summary>
    public sealed partial class ProcessControler
    {
        /// <summary>
        /// 获取进程的使用者
        /// </summary>
        /// <param name="processID">进程ID（输入参数）</param>
        /// <returns>进程使用者</returns>
        public static string GetProcessUserName(int processID)
        {
            string sRet = string.Empty;
            SelectQuery Query = new SelectQuery("SELECT * FROM Win32_Process WHERE processID=" + processID);
            ManagementObjectSearcher Searcher = new ManagementObjectSearcher(Query);
            try
            {
                foreach (ManagementObject obj in Searcher.Get())
                {
                    ManagementBaseObject Param = null;
                    Param = obj.GetMethodParameters("GetOwner");
                    return obj.InvokeMethod("GetOwner", Param, null)["User"].ToString();
                }
                return sRet;
            }
            catch
            {
                sRet = "SYSTEM";
            }
            return sRet;
        }
        /// <summary>
        /// 判断进程是否存在
        /// </summary>
        /// <param name="processName">进程名（输入参数）</param>
        /// <returns>true： 存在；false： 不存在</returns>
        public static bool IsProcessExists(string processName)
        {
            // 参数判断
            if (true == string.IsNullOrEmpty(processName))
            {
                throw new ArgumentNullException("processName");
            }

            System.Diagnostics.Process[] Process = System.Diagnostics.Process.GetProcessesByName(processName);
            if ((null != Process) && (Process.Length > 1))
            {
                return true;
            }
            return false;
        }
    }
}
