using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ------------------------------------------------------------ //
// 版权所有：CopyRight (C) LanwahSoft
// 项目名称：Lanwah.CSharp.NET.sln
// 文件名称：IDES.cs
// 创 建 者：Lanwah
// 创建日期：2016-04-09
// 功能描述：DES、TripleDES加密解密接口类
// 调用依赖：
// -------------------------------------------------------------
// 修 改 者：
// 修改时间：
// 修改原因：
// 修改描述：
// ------------------------------------------------------------ //

using System.Security.Cryptography;

namespace Lanwah.CSharp.NET.SecurityLib
{
    /// <summary>
    /// DES、TripleDES加密解密接口类
    /// </summary>
    interface IDES
    {
        /// <summary>
        /// 加密接口类
        /// </summary>
        /// <param name="cipherMode">获取或设置对称算法的运算模式。（输入参数）</param>
        /// <param name="paddingMode">获取或设置对称算法中使用的填充模式。（输入参数）</param>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="IV">用于对称算法的初始化向量。（输入参数）</param>
        /// <param name="encryptBuffer">明文的需要加密的数据。（输入参数）</param>
        /// <returns>返回加密后密文数据</returns>
        byte[] Encrypt(CipherMode cipherMode, PaddingMode paddingMode, byte[] key, byte[] IV, byte[] encryptBuffer);
        /// <summary>
        /// 解密接口类
        /// </summary>
        /// <param name="cipherMode">获取或设置对称算法的运算模式。（输入参数）</param>
        /// <param name="paddingMode">获取或设置对称算法中使用的填充模式。（输入参数）</param>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="IV">用于对称算法的初始化向量。（输入参数）</param>
        /// <param name="decryptBuffer">密文的需要解密的数据。（输入参数）</param>
        /// <returns>返回解密后明文数据</returns>
        byte[] Decrypt(CipherMode cipherMode, PaddingMode paddingMode, byte[] key, byte[] IV, byte[] decryptBuffer);
        /// <summary>
        /// 加密接口类
        /// </summary>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="IV">用于对称算法的初始化向量。（输入参数）</param>
        /// <param name="encryptBuffer">明文的需要加密的数据。（输入参数）</param>
        /// <returns>返回加密后密文数据</returns>
        byte[] Encrypt(byte[] key, byte[] IV, byte[] encryptBuffer);
        /// <summary>
        /// 解密接口类
        /// </summary>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="IV">用于对称算法的初始化向量。（输入参数）</param>
        /// <param name="decryptBuffer">密文的需要解密的数据。（输入参数）</param>
        /// <returns>返回解密后明文数据</returns>
        byte[] Decrypt(byte[] key, byte[] IV, byte[] decryptBuffer);
        /// <summary>
        /// 加密接口类
        /// </summary>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="encryptBuffer">明文的需要加密的数据。（输入参数）</param>
        /// <returns>返回加密后密文数据</returns>
        byte[] Encrypt(byte[] key, byte[] encryptBuffer);
        /// <summary>
        /// 解密接口类
        /// </summary>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="decryptBuffer">密文的需要解密的数据。（输入参数）</param>
        /// <returns>返回解密后明文数据</returns>
        byte[] Decrypt(byte[] key, byte[] decryptBuffer);

        /// <summary>
        /// 加密接口类
        /// </summary>
        /// <param name="cipherMode">获取或设置对称算法的运算模式。（输入参数）</param>
        /// <param name="paddingMode">获取或设置对称算法中使用的填充模式。（输入参数）</param>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="IV">用于对称算法的初始化向量。（输入参数）</param>
        /// <param name="encryptString">明文的需要加密的数据。（输入参数）</param>
        /// <param name="webName">当前 System.Text.Encoding 的 IANA 名称。（输入参数）</param>
        /// <returns>返回加密后密文数据</returns>
        string Encrypt(CipherMode cipherMode, PaddingMode paddingMode, string key, string IV, string encryptString, string webName = "utf-8");
        /// <summary>
        /// 解密接口类
        /// </summary>
        /// <param name="cipherMode">获取或设置对称算法的运算模式。（输入参数）</param>
        /// <param name="paddingMode">获取或设置对称算法中使用的填充模式。（输入参数）</param>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="IV">用于对称算法的初始化向量。（输入参数）</param>
        /// <param name="decryptString">密文的需要解密的数据。（输入参数）</param>
        /// <param name="webName">当前 System.Text.Encoding 的 IANA 名称。（输入参数）</param>
        /// <returns>返回解密后明文数据</returns>
        string Decrypt(CipherMode cipherMode, PaddingMode paddingMode, string key, string IV, string decryptString, string webName = "utf-8");
        /// <summary>
        /// 加密接口类
        /// </summary>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="IV">用于对称算法的初始化向量。（输入参数）</param>
        /// <param name="encryptString">明文的需要加密的数据。（输入参数）</param>
        /// <param name="webName">当前 System.Text.Encoding 的 IANA 名称。（输入参数）</param>
        /// <returns>返回加密后密文数据</returns>
        string Encrypt(string key, string IV, string encryptString, string webName = "utf-8");
        /// <summary>
        /// 解密接口类
        /// </summary>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="IV">用于对称算法的初始化向量。（输入参数）</param>
        /// <param name="decryptString">密文的需要解密的数据。（输入参数）</param>
        /// <param name="webName">当前 System.Text.Encoding 的 IANA 名称。（输入参数）</param>
        /// <returns>返回解密后明文数据</returns>
        string Decrypt(string key, string IV, string decryptString, string webName = "utf-8");
        /// <summary>
        /// 加密接口类
        /// </summary>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="encryptString">明文的需要加密的数据。（输入参数）</param>
        /// <param name="webName">当前 System.Text.Encoding 的 IANA 名称。（输入参数）</param>
        /// <returns>返回加密后密文数据</returns>
        string Encrypt(string key, string encryptString, string webName = "utf-8");
        /// <summary>
        /// 解密接口类
        /// </summary>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="decryptString">密文的需要解密的数据。（输入参数）</param>
        /// <param name="webName">当前 System.Text.Encoding 的 IANA 名称。（输入参数）</param>
        /// <returns>返回解密后明文数据</returns>
        string Decrypt(string key, string decryptString, string webName = "utf-8");
    }
}
