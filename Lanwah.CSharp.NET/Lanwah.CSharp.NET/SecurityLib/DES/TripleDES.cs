using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ------------------------------------------------------------ //
// 版权所有：CopyRight (C) LanwahSoft
// 项目名称：Lanwah.CSharp.NET.sln
// 文件名称：TripleDES.cs
// 创 建 者：Lanwah
// 创建日期：2016-04-09
// 功能描述：TripleDES加密解密类
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
    /// TripleDES加密加密类
    /// </summary>
    public sealed partial class TripleDES : IDES
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
        public byte[] Encrypt(CipherMode cipherMode, PaddingMode paddingMode, byte[] key, byte[] IV, byte[] encryptBuffer)
        {
            // 参数检查
            if (null == key)
            {
                throw new ArgumentNullException("key");
            }
            if ((16 != key.Length) && (24 != key.Length))
            {
                throw new ArgumentException("密钥key的长度不合法，有效数据长度为16或24字节。");
            }
            if ((CipherMode.ECB != cipherMode) && (null == IV))
            {
                throw new ArgumentNullException("IV");
            }
            if ((CipherMode.ECB != cipherMode) && (8 != IV.Length))
            {
                throw new ArgumentException("方向向量IV的长度不合法，有效数据长度为8字节。");
            }
            if ((null == encryptBuffer) || (0 == encryptBuffer.Length))
            {
                throw new ArgumentNullException("encryptBuffer");
            }
            if ((PaddingMode.None == paddingMode) && (encryptBuffer.Length % 8 != 0))
            {
                throw new ArgumentException("需加密数据内容的长度不合法，当填充模式为PaddingMode.None时加密数据的长度必须为8字节的整数倍。");
            }
            // 加密处理
            // Create a MemoryStream.  
            MemoryStream msStream = new MemoryStream();
            TripleDESCryptoServiceProvider Provider = new TripleDESCryptoServiceProvider();
            Provider.Mode = cipherMode;             // 默认值  CipherMode.CBC
            Provider.Padding = paddingMode;         // 默认值  PaddingMode.PKCS7
            // Create a CryptoStream using the MemoryStream and the passed Key and initialization vector (IV).
            CryptoStream csStream = new CryptoStream(msStream, Provider.CreateEncryptor(key, IV), CryptoStreamMode.Write);
            // Write the byte array to the crypto stream and flush it.  
            csStream.Write(encryptBuffer, 0, encryptBuffer.Length);
            csStream.FlushFinalBlock();
            // Get an array of bytes from the MemoryStream that holds the encrypted data.  
            byte[] EncryptedBuffer = msStream.ToArray();
            // Close the streams.  
            csStream.Close();
            msStream.Close();
            // Return the encrypted buffer.  
            return EncryptedBuffer;
        }
        /// <summary>
        /// 解密接口类
        /// </summary>
        /// <param name="cipherMode">获取或设置对称算法的运算模式。（输入参数）</param>
        /// <param name="paddingMode">获取或设置对称算法中使用的填充模式。（输入参数）</param>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="IV">用于对称算法的初始化向量。（输入参数）</param>
        /// <param name="decryptBuffer">密文的需要解密的数据。（输入参数）</param>
        /// <returns>返回解密后明文数据</returns>
        public byte[] Decrypt(CipherMode cipherMode, PaddingMode paddingMode, byte[] key, byte[] IV, byte[] decryptBuffer)
        {
            // 参数检查
            if (null == key)
            {
                throw new ArgumentNullException("key");
            }
            if ((16 != key.Length) && (24 != key.Length))
            {
                throw new ArgumentException("密钥key的长度不合法，有效数据长度为16或24字节。");
            }
            if ((CipherMode.ECB != cipherMode) && (null == IV))
            {
                throw new ArgumentNullException("IV");
            }
            if ((CipherMode.ECB != cipherMode) && (8 != IV.Length))
            {
                throw new ArgumentException("方向向量IV的长度不合法，有效数据长度为8字节。");
            }
            if ((null == decryptBuffer) || (0 == decryptBuffer.Length))
            {
                throw new ArgumentNullException("decryptBuffer");
            }
            if ((PaddingMode.None == paddingMode) && (decryptBuffer.Length % 8 != 0))
            {
                throw new ArgumentException("需解密数据内容的长度不合法，当填充模式为PaddingMode.None时解密数据的长度必须为8的整数倍。");
            }
            // 解密处理
            // Create a new MemoryStream using the passed array of encrypted data.  
            MemoryStream msStream = new MemoryStream(decryptBuffer);
            TripleDESCryptoServiceProvider Provider = new TripleDESCryptoServiceProvider();
            Provider.Mode = cipherMode;
            Provider.Padding = paddingMode;
            // Create a CryptoStream using the MemoryStream and the passed Key and initialization vector (IV).  
            CryptoStream csDecrypt = new CryptoStream(msStream, Provider.CreateDecryptor(key, IV), CryptoStreamMode.Read);
            // Create buffer to hold the decrypted data.  
            byte[] DecryptedBuffer = new byte[decryptBuffer.Length];
            // Read the decrypted data out of the crypto stream and place it into the temporary buffer.  
            csDecrypt.Read(DecryptedBuffer, 0, DecryptedBuffer.Length);
            //Convert the buffer into a string and return it.  
            return DecryptedBuffer;
        }
        /// <summary>
        /// 加密接口类
        /// </summary>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="IV">用于对称算法的初始化向量。（输入参数）</param>
        /// <param name="encryptBuffer">明文的需要加密的数据。（输入参数）</param>
        /// <returns>返回加密后密文数据</returns>
        public byte[] Encrypt(byte[] key, byte[] IV, byte[] encryptBuffer)
        {
            // 返回加密后的密文数据
            return this.Encrypt(CipherMode.ECB, PaddingMode.None, key, IV, encryptBuffer);
        }
        /// <summary>
        /// 解密接口类
        /// </summary>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="IV">用于对称算法的初始化向量。（输入参数）</param>
        /// <param name="decryptBuffer">密文的需要解密的数据。（输入参数）</param>
        /// <returns>返回解密后明文数据</returns>
        public byte[] Decrypt(byte[] key, byte[] IV, byte[] decryptBuffer)
        {
            // 返回解密后的明文数据
            return this.Decrypt(CipherMode.ECB, PaddingMode.None, key, IV, decryptBuffer);
        }
        /// <summary>
        /// 加密接口类
        /// </summary>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="encryptBuffer">明文的需要加密的数据。（输入参数）</param>
        /// <returns>返回加密后密文数据</returns>
        public byte[] Encrypt(byte[] key, byte[] encryptBuffer)
        {
            // 返回加密后的密文数据
            return this.Encrypt(CipherMode.ECB, PaddingMode.None, key, null, encryptBuffer);
        }
        /// <summary>
        /// 解密接口类
        /// </summary>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="decryptBuffer">密文的需要解密的数据。（输入参数）</param>
        /// <returns>返回解密后明文数据</returns>
        public byte[] Decrypt(byte[] key, byte[] decryptBuffer)
        {
            // 返回解密后的明文数据
            return this.Decrypt(CipherMode.ECB, PaddingMode.None, key, null, decryptBuffer);
        }
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
        public string Encrypt(CipherMode cipherMode, PaddingMode paddingMode, string key, string IV, string encryptString, string webName = "utf-8")
        {
            // 参数检查
            if (true == string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            if ((CipherMode.ECB != cipherMode) && (true == string.IsNullOrEmpty(IV)))
            {
                throw new ArgumentNullException("IV");
            }
            if (true == string.IsNullOrEmpty(encryptString))
            {
                throw new ArgumentNullException("encryptString");
            }

            // 根据编码名称获取编码
            Encoding encoding = Encoding.GetEncoding(webName);
            byte[] KeyBuffer = encoding.GetBytes(key);
            byte[] IVBuffer = ((true == string.IsNullOrEmpty(IV)) ? null : encoding.GetBytes(IV));
            byte[] EncryptBuffer = encoding.GetBytes(encryptString);

            // 返回加密后的密文数据
            return encoding.GetString(this.Encrypt(cipherMode, paddingMode, KeyBuffer, IVBuffer, EncryptBuffer));
        }
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
        public string Decrypt(CipherMode cipherMode, PaddingMode paddingMode, string key, string IV, string decryptString, string webName = "utf-8")
        {
            // 参数检查
            if (true == string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            if ((CipherMode.ECB != cipherMode) && (true == string.IsNullOrEmpty(IV)))
            {
                throw new ArgumentNullException("IV");
            }
            if (true == string.IsNullOrEmpty(decryptString))
            {
                throw new ArgumentNullException("decryptString");
            }

            // 根据编码名称获取编码
            Encoding encoding = Encoding.GetEncoding(webName);
            byte[] KeyBuffer = encoding.GetBytes(key);
            byte[] IVBuffer = ((true == string.IsNullOrEmpty(IV)) ? null : encoding.GetBytes(IV));
            byte[] DecryptBuffer = encoding.GetBytes(decryptString);

            // 返回解密后的密文数据
            return encoding.GetString(this.Decrypt(cipherMode, paddingMode, KeyBuffer, IVBuffer, DecryptBuffer));
        }
        /// <summary>
        /// 加密接口类
        /// </summary>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="IV">用于对称算法的初始化向量。（输入参数）</param>
        /// <param name="encryptString">明文的需要加密的数据。（输入参数）</param>
        /// <param name="webName">当前 System.Text.Encoding 的 IANA 名称。（输入参数）</param>
        /// <returns>返回加密后密文数据</returns>
        public string Encrypt(string key, string IV, string encryptString, string webName = "utf-8")
        {
            // 返回加密后的密文数据
            return this.Encrypt(CipherMode.ECB, PaddingMode.None, key, IV, encryptString, webName);
        }
        /// <summary>
        /// 解密接口类
        /// </summary>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="IV">用于对称算法的初始化向量。（输入参数）</param>
        /// <param name="decryptString">密文的需要解密的数据。（输入参数）</param>
        /// <param name="webName">当前 System.Text.Encoding 的 IANA 名称。（输入参数）</param>
        /// <returns>返回解密后明文数据</returns>
        public string Decrypt(string key, string IV, string decryptString, string webName = "utf-8")
        {
            // 返回解密后的密文数据
            return this.Decrypt(CipherMode.ECB, PaddingMode.None, key, IV, decryptString, webName);
        }
        /// <summary>
        /// 加密接口类
        /// </summary>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="encryptString">明文的需要加密的数据。（输入参数）</param>
        /// <param name="webName">当前 System.Text.Encoding 的 IANA 名称。（输入参数）</param>
        /// <returns>返回加密后密文数据</returns>
        public string Encrypt(string key, string encryptString, string webName = "utf-8")
        {
            // 返回加密后的密文数据
            return this.Encrypt(CipherMode.ECB, PaddingMode.None, key, null, encryptString, webName);
        }
        /// <summary>
        /// 解密接口类
        /// </summary>
        /// <param name="key">用于对称算法的密钥。（输入参数）</param>
        /// <param name="decryptString">密文的需要解密的数据。（输入参数）</param>
        /// <param name="webName">当前 System.Text.Encoding 的 IANA 名称。（输入参数）</param>
        /// <returns>返回解密后明文数据</returns>
        public string Decrypt(string key, string decryptString, string webName = "utf-8")
        {
            // 返回解密后的密文数据
            return this.Decrypt(CipherMode.ECB, PaddingMode.None, key, null, decryptString, webName);
        }

        // 单例模式获取实例

        /// <summary>
        /// TripleDES实例
        /// </summary>
        private static volatile TripleDES _instance;
        /// <summary>
        /// 共享锁
        /// </summary>
        private static readonly object _locker = new object();
        /// <summary>
        /// 获取TripleDES实例
        /// </summary>
        public static TripleDES Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (_locker)
                    {
                        if (null == _instance)
                        {
                            _instance = new TripleDES();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
