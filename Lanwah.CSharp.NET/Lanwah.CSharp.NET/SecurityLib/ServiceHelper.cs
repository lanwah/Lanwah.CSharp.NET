using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ------------------------------------------------------------ //
// 版权所有：CopyRight (C) LanwahSoft
// 项目名称：Lanwah.CSharp.NET.sln
// 文件名称：ServiceController.cs
// 创 建 者：Lanwah
// 创建日期：2016-05-29
// 功能描述：Windows服务操作类
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
    /// Windows服务操作类
    /// </summary>
    public sealed partial class ServiceController
    {
        /// <summary>
        /// 安装Windows服务
        /// </summary>
        /// <param name="servicePath">服务安装的完整路径（输入参数）</param>
        /// <returns>"1" ： 成功；其他： 失败</returns>
        public static string InstallService(string servicePath)
        {
            // 参数检查
            if (false == System.IO.File.Exists(servicePath))
            {
                return "服务安装文件不存在！";
            }

            System.IO.FileInfo Info = new System.IO.FileInfo(servicePath);
            System.Collections.IDictionary StateSaver = new System.Collections.Hashtable();
            try
            {
                System.Configuration.Install.AssemblyInstaller Installer = new System.Configuration.Install.AssemblyInstaller(servicePath, new string[] { "/LogFile=" + Info.DirectoryName + @"\" + Info.Name.Substring(0, Info.Name.Length - Info.Extension.Length) + ".log" });
                Installer.UseNewContext = true;
                Installer.Install(StateSaver);
                Installer.Commit(StateSaver);
                Type[] Types = Installer.Assembly.GetTypes();
                for (int i = 0; i != Types.Length; i++)
                {
                    if (Types[i].BaseType.FullName == "System.Configuration.Install.Installer")
                    {
                        object objA = Activator.CreateInstance(Types[i]);
                        System.Reflection.FieldInfo[] Fields = Types[i].GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        for (int j = 0; j < Fields.Length; j++)
                        {
                            if (Fields[j].FieldType.FullName == "System.ServiceProcess.ServiceInstaller")
                            {
                                object objB = Fields[j].GetValue(objA);
                                if (objB != null)
                                {
                                    //serviceName = ((ServiceInstaller)objB).ServiceName;
                                    // 服务安装成功
                                    return "1";
                                }
                            }
                        }
                    }
                }
                return "服务安装文件不存在！";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 卸载Windows服务
        /// </summary>
        /// <param name="servicePath">服务的完整路径（输入参数）</param>
        /// <returns>"1" ： 成功；其他： 失败</returns>
        public static string UnInstallService(string servicePath)
        {
            // 参数检查
            if (false == System.IO.File.Exists(servicePath))
            {
                return "服务文件不存在！";
            }

            System.IO.FileInfo Info = new System.IO.FileInfo(servicePath);
            System.Collections.IDictionary SavedState = new System.Collections.Hashtable();
            try
            {
                System.Configuration.Install.AssemblyInstaller installer = new System.Configuration.Install.AssemblyInstaller(servicePath, new string[] { "/LogFile=" + Info.DirectoryName + @"\" + Info.Name.Substring(0, Info.Name.Length - Info.Extension.Length) + ".log" });
                installer.UseNewContext = true;
                installer.Uninstall(SavedState);
                //installer.Commit(SavedState); // 不注释会报找不到installState文件的异常
                // 服务卸载成功
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 检索本地计算机上的所有服务
        /// </summary>
        /// <returns>服务名称数组</returns>
        public static string[] GetLocalServices()
        {
            string[] ServiceNames;
            // 检索本地计算机上的所有服务
            System.ServiceProcess.ServiceController[] Services = System.ServiceProcess.ServiceController.GetServices();
            ServiceNames = new string[Services.Length];
            for (int i = 0; i < Services.Length; i++)
            {
                ServiceNames[i] = Services[i].ServiceName;
            }
            return ServiceNames;
        }
        /// <summary>
        /// 获取服务的描述信息
        /// </summary>
        /// <param name="serviceName">服务名（输入参数）</param>
        /// <returns>服务的描述信息</returns>
        public static string GetServiceDescription(string serviceName)
        {
            // 参数检查
            if (true == string.IsNullOrWhiteSpace(serviceName))
            {
                throw new ArgumentNullException("serviceName");
            }

            using (System.ServiceProcess.ServiceController Controller = new System.ServiceProcess.ServiceController(serviceName))
            {
                string sRet = string.Empty;
                int nBytesNeeded = 0;
                try
                {
                    Win32Lib.Advapi32.QueryServiceConfig2(Controller.ServiceHandle, 1, IntPtr.Zero, 0, ref nBytesNeeded);
                }
                catch
                {
                    return "Information Cannot Get.";
                }
                IntPtr pBuffer = System.Runtime.InteropServices.Marshal.AllocHGlobal(nBytesNeeded);
                if (Win32Lib.Advapi32.QueryServiceConfig2(Controller.ServiceHandle, 1, pBuffer, nBytesNeeded, ref nBytesNeeded))
                {
                    sRet = System.Runtime.InteropServices.Marshal.PtrToStringUni(System.Runtime.InteropServices.Marshal.ReadIntPtr(pBuffer));
                }
                System.Runtime.InteropServices.Marshal.FreeHGlobal(pBuffer);
                return sRet;
            }
        }
        /// <summary>
        /// 获取服务完整路径
        /// </summary>
        /// <param name="serviceName">服务名（输入参数）</param>
        /// <returns>服务名对应服务的完整路径</returns>
        public static string GetServicePath(string serviceName)
        {
            // 参数检查
            if (true == string.IsNullOrWhiteSpace(serviceName))
            {
                throw new ArgumentNullException("serviceName");
            }

            try
            {
                Microsoft.Win32.RegistryKey Key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SYSTEM\ControlSet001\Services\" + serviceName);
                if (Key != null)
                {
                    object obj = Key.GetValue("ImagePath");
                    if (obj != null)
                    {
                        return obj.ToString();
                    }
                }
                return string.Empty;
            }
            catch (Exception err)
            {
                return err.Message;
            }
        }
    }
}
