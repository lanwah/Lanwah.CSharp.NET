using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ------------------------------------------------------------ //
// 版权所有：CopyRight (C) LanwahSoft
// 项目名称：Lanwah.CSharp.NET.sln
// 文件名称：CryptoFactory.cs
// 创 建 者：Lanwah
// 创建日期：2016-04-10
// 功能描述：DES、TripleDES加密解密工厂类
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
    /// DES、TripleDES加密解密工厂类
    /// </summary>
    public sealed partial class CryptoFactory
    {
        /// <summary>
        /// 获取DES或TripleDES加密解密对象
        /// </summary>
        /// <param name="cryptoName"></param>
        /// <returns></returns>
        public static IDES Create(CryptoMode cryptoName)
        {
            IDES CryptoInstance;
            if (CryptoMode.TripleDES == cryptoName)
            {
                CryptoInstance = TripleDES.Instance;
            }
            else
            {
                CryptoInstance = DES.Instance;
            }
            return CryptoInstance;
        }
    }
    /// <summary>
    /// 加密类型
    /// </summary>
    public enum CryptoMode : int
    {
        /// <summary>
        /// DES加密解密
        /// </summary>
        DES = 1,
        /// <summary>
        /// 3DES加密解密
        /// </summary>
        TripleDES = 2
    }
}
