using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ------------------------------------------------------------ //
// 版权所有：CopyRight (C) LanwahSoft
// 项目名称：Lanwah.CSharp.NET.sln
// 文件名称：MD5Hash.cs
// 创 建 者：Lanwah
// 创建日期：2016-05-21
// 功能描述：MD5 Hash Code
// 调用依赖：
// -------------------------------------------------------------
// 修 改 者：
// 修改时间：
// 修改原因：
// 修改描述：
// ------------------------------------------------------------ //

using System.Security.Cryptography;
using System.IO;

namespace Lanwah.CSharp.NET.SecurityLib
{
    /// <summary>
    /// MD5 Hash Code
    /// </summary>
    public static partial class MD5Hash
    {
        /// <summary>
        /// 计算MD5哈希值（校验码）
        /// </summary>
        /// <param name="bytes">要计算哈希值的二进制内容（输入参数）</param>
        /// <returns>返回16字节的哈希值（校验码）</returns>
        public static byte[] MD5HashCode(byte[] bytes)
        {
            // 参数检查
            if (null == bytes)
            {
                throw new ArgumentNullException("bytes");
            }

            // 计算哈希值
            MD5CryptoServiceProvider Provider = new MD5CryptoServiceProvider();
            return Provider.ComputeHash(bytes);
        }
        /// <summary>
        /// 计算MD5哈希值（校验码）
        /// </summary>
        /// <param name="stream">要计算其哈希代码的输入（输入参数）</param>
        /// <returns>返回16字节的哈希值（校验码）</returns>
        public static byte[] MD5HashCode(System.IO.Stream stream)
        {
            // 参数检查
            if (null == stream)
            {
                throw new ArgumentNullException("stream");
            }

            // 计算哈希值
            MD5CryptoServiceProvider Provider = new MD5CryptoServiceProvider();
            return Provider.ComputeHash(stream);
        }
        /// <summary>
        /// 计算MD5哈希值（校验码）
        /// </summary>
        /// <param name="filePath">需要计算MD5 Hash Code 的文件的完整路径（输入参数）</param>
        /// <returns>返回16字节的哈希值（校验码）</returns>
        public static byte[] MD5HashCode(string filePath)
        {
            // 参数检查
            if (true == string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath");
            }

            // 计算哈希值
            using (FileStream Stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                MD5CryptoServiceProvider Provider = new MD5CryptoServiceProvider();
                return Provider.ComputeHash(Stream);
            }
        }
    }
}
