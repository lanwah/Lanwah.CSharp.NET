using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ------------------------------------------------------------ //
// 版权所有：CopyRight (C) LanwahSoft
// 项目名称：Lanwah.CSharp.NET.sln
// 文件名称：DES.cs
// 创 建 者：Lanwah
// 创建日期：2016-04-09
// 功能描述：DES加密解密类
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
    /// DES加密解密类
    /// </summary>
    public sealed partial class DES : IDES
    {
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="cipherMode">获取或设置对称算法的运算模式。（输入参数）</param>
        /// <param name="paddingMode">获取或设置对称算法中使用的填充模式。（输入参数）</param>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="IV">用于对称算法的初始化向量。（输入参数）</param>
        /// <param name="encryptBuffer">明文的需要加密的数据。（输入参数）</param>
        /// <returns>返回加密后密文数据</returns>
        public byte[] Encrypt(CipherMode cipherMode, PaddingMode paddingMode, byte[] key, byte[] IV, byte[] encryptBuffer)
        {
            // 参数合法性检查
            if (null == key)
            {
                throw new ArgumentNullException("key");
            }
            if (8 != key.Length)
            {
                throw new ArgumentException("密钥key的长度不合法，有效数据长度为8字节。");
            }
            if (null == IV)
            {
                throw new ArgumentNullException("IV");
            }
            if (8 != IV.Length)
            {
                throw new ArgumentException("方向向量IV的长度不合法，有效数据长度为8字节。");
            }
            if (null == encryptBuffer)
            {
                throw new ArgumentNullException("encryptBuffer");
            }
            // 加密操作
            DESCryptoServiceProvider Provider = new DESCryptoServiceProvider();
            Provider.Mode = cipherMode;          // 默认值  CipherMode.CBC
            Provider.Padding = paddingMode;      // 默认值  PaddingMode.PKCS7
            MemoryStream msStream = new MemoryStream();
            CryptoStream csStream = new CryptoStream(msStream, Provider.CreateEncryptor(key, IV), CryptoStreamMode.Write);
            csStream.Write(encryptBuffer, 0, encryptBuffer.Length);
            csStream.FlushFinalBlock();
            byte[] EncryptedBuffer = msStream.ToArray();
            csStream.Close();
            msStream.Close();
            return EncryptedBuffer;
        }
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="cipherMode">获取或设置对称算法的运算模式。（输入参数）</param>
        /// <param name="paddingMode">获取或设置对称算法中使用的填充模式。（输入参数）</param>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="IV">用于对称算法的初始化向量。（输入参数）</param>
        /// <param name="decryptBuffer">密文的需要解密的数据。（输入参数）</param>
        /// <returns>返回解密后明文数据</returns>
        public byte[] Decrypt(CipherMode cipherMode, PaddingMode paddingMode, byte[] key, byte[] IV, byte[] decryptBuffer)
        {
            // 参数合法性检查
            if (null == key)
            {
                throw new ArgumentNullException("key");
            }
            if (8 != key.Length)
            {
                throw new ArgumentException("密钥key的长度不合法，有效数据长度为8字节。");
            }
            if (null == IV)
            {
                throw new ArgumentNullException("IV");
            }
            if (8 != IV.Length)
            {
                throw new ArgumentException("方向向量IV的长度不合法，有效数据长度为8字节。");
            }
            if (null == decryptBuffer)
            {
                throw new ArgumentNullException("encryptBuffer");
            }
            // 解密操作
            DESCryptoServiceProvider Provider = new DESCryptoServiceProvider();
            Provider.Mode = cipherMode;        // 默认值  CipherMode.CBC
            Provider.Padding = paddingMode;    // 默认值  PaddingMode.PKCS7
            MemoryStream msStream = new MemoryStream();
            CryptoStream csStream = new CryptoStream(msStream, Provider.CreateDecryptor(key, IV), CryptoStreamMode.Write);
            csStream.Write(decryptBuffer, 0, decryptBuffer.Length);
            csStream.FlushFinalBlock();
            byte[] DecryptedBuffer = msStream.ToArray();
            csStream.Close();
            msStream.Close();
            return DecryptedBuffer;
        }
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="IV">用于对称算法的初始化向量。（输入参数）</param>
        /// <param name="encryptBuffer">明文的需要加密的数据。（输入参数）</param>
        /// <returns>返回加密后密文数据</returns>
        public byte[] Encrypt(byte[] key, byte[] IV, byte[] encryptBuffer)
        {
            return this.Encrypt(CipherMode.ECB, PaddingMode.Zeros, key, IV, encryptBuffer);
        }
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="IV">用于对称算法的初始化向量。（输入参数）</param>
        /// <param name="decryptBuffer">密文的需要解密的数据。（输入参数）</param>
        /// <returns>返回解密后明文数据</returns>
        public byte[] Decrypt(byte[] key, byte[] IV, byte[] decryptBuffer)
        {
            return this.Decrypt(CipherMode.ECB, PaddingMode.Zeros, key, IV, decryptBuffer);
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="encryptBuffer">明文的需要加密的数据。（输入参数）</param>
        /// <returns>返回加密后密文数据</returns>
        public byte[] Encrypt(byte[] key, byte[] encryptBuffer)
        {
            // 使用默认8个0的方向向量
            byte[] IV = new byte[8];
            return this.Encrypt(CipherMode.ECB, PaddingMode.Zeros, key, IV, encryptBuffer);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="decryptBuffer">密文的需要解密的数据。（输入参数）</param>
        /// <returns>返回解密后明文数据</returns>
        public byte[] Decrypt(byte[] key, byte[] decryptBuffer)
        {
            // 使用默认8个0的方向向量
            byte[] IV = new byte[8];
            return this.Decrypt(CipherMode.ECB, PaddingMode.Zeros, key, IV, decryptBuffer);
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="cipherMode">获取或设置对称算法的运算模式。（输入参数）</param>
        /// <param name="paddingMode">获取或设置对称算法中使用的填充模式。（输入参数）</param>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="IV">用于对称算法的初始化向量。（输入参数）</param>
        /// <param name="encryptString">明文的需要加密的数据。（输入参数）</param>
        /// <param name="webName">当前 System.Text.Encoding 的 IANA 名称。（输入参数）</param>
        /// <returns>返回加密后密文数据</returns>
        public string Encrypt(CipherMode cipherMode, PaddingMode paddingMode, string key, string IV, string encryptString, string webName = "utf-8")
        {
            // 参数检查
            if (true == string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            //if (true == string.IsNullOrEmpty(IV))
            //{
            //    throw new ArgumentNullException("IV");
            //}
            if (true == string.IsNullOrEmpty(encryptString))
            {
                throw new ArgumentNullException("encryptString");
            }

            // 根据编码名称获取编码
            Encoding encoding = Encoding.GetEncoding(webName);
            byte[] KeyBuffer = encoding.GetBytes(key);
            byte[] IVBuffer = ((true == string.IsNullOrEmpty(IV)) ? new byte[8] : encoding.GetBytes(IV));
            byte[] EncryptBuffer = encoding.GetBytes(encryptString);

            // 返回加密后的密文结果
            return encoding.GetString(this.Encrypt(cipherMode, paddingMode, KeyBuffer, IVBuffer, EncryptBuffer));
        }
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="cipherMode">获取或设置对称算法的运算模式。（输入参数）</param>
        /// <param name="paddingMode">获取或设置对称算法中使用的填充模式。（输入参数）</param>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="IV">用于对称算法的初始化向量。（输入参数）</param>
        /// <param name="decryptString">密文的需要解密的数据。（输入参数）</param>
        /// <param name="webName">当前 System.Text.Encoding 的 IANA 名称。（输入参数）</param>
        /// <returns>返回解密后明文数据</returns>
        public string Decrypt(CipherMode cipherMode, PaddingMode paddingMode, string key, string IV, string decryptString, string webName = "utf-8")
        {
            // 参数检查
            if (true == string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            //if (true == string.IsNullOrEmpty(IV))
            //{
            //    throw new ArgumentNullException("IV");
            //}
            if (true == string.IsNullOrEmpty(decryptString))
            {
                throw new ArgumentNullException("encryptString");
            }

            // 根据编码名称获取编码
            Encoding encoding = Encoding.GetEncoding(webName);
            byte[] KeyBuffer = encoding.GetBytes(key);
            byte[] IVBuffer = ((true == string.IsNullOrEmpty(IV)) ? new byte[8] : encoding.GetBytes(IV));
            byte[] DecryptBuffer = encoding.GetBytes(decryptString);

            // 返回解密后的密文数据
            return encoding.GetString(this.Decrypt(cipherMode, paddingMode, KeyBuffer, IVBuffer, DecryptBuffer));
        }
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="IV">用于对称算法的初始化向量。（输入参数）</param>
        /// <param name="encryptString">明文的需要加密的数据。（输入参数）</param>
        /// <param name="webName">当前 System.Text.Encoding 的 IANA 名称。（输入参数）</param>
        /// <returns>返回加密后密文数据</returns>
        public string Encrypt(string key, string IV, string encryptString, string webName = "utf-8")
        {
            // 采用默认的 运算模式 和 填充模式
            return this.Encrypt(CipherMode.ECB, PaddingMode.Zeros, key, IV, encryptString, webName);
        }
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="IV">用于对称算法的初始化向量。（输入参数）</param>
        /// <param name="decryptString">密文的需要解密的数据。（输入参数）</param>
        /// <param name="webName">当前 System.Text.Encoding 的 IANA 名称。（输入参数）</param>
        /// <returns>返回解密后明文数据</returns>
        public string Decrypt(string key, string IV, string decryptString, string webName = "utf-8")
        {
            // 采用默认的 运算模式 和 填充模式
            return this.Decrypt(CipherMode.ECB, PaddingMode.Zeros, key, IV, decryptString, webName);
        }
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="encryptString">明文的需要加密的数据。（输入参数）</param>
        /// <param name="webName">当前 System.Text.Encoding 的 IANA 名称。（输入参数）</param>
        /// <returns>返回加密后密文数据</returns>
        public string Encrypt(string key, string encryptString, string webName = "utf-8")
        {
          return  this.Encrypt(CipherMode.ECB, PaddingMode.Zeros, key, null, encryptString, webName);
        }
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="decryptString">密文的需要解密的数据。（输入参数）</param>
        /// <param name="webName">当前 System.Text.Encoding 的 IANA 名称。（输入参数）</param>
        /// <returns>返回解密后明文数据</returns>
        public string Decrypt(string key, string decryptString, string webName = "utf-8")
        {
            return this.Decrypt(CipherMode.ECB, PaddingMode.Zeros, key, null, decryptString, webName);
        }

        // 单例模式获取实例

        /// <summary>
        /// DES实例
        /// </summary>
        private static volatile DES _instance;
        /// <summary>
        /// 共享锁
        /// </summary>
        private static readonly object _locker = new object();
        /// <summary>
        /// 获取DES实例
        /// </summary>
        public static DES Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (_locker)
                    {
                        if (null == _instance)
                        {
                            _instance = new DES();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
