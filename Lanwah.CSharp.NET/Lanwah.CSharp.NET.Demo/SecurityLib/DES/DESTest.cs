using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Lanwah.CSharp.NET.Demo.SecurityLib.DES
{
    /// <summary>
    /// DES测试
    /// </summary>
    public sealed partial class DESTest
    {
        /// <summary>
        /// 测试代码段
        /// </summary>
        public static void RunTest()
        {
            Random random = new Random();

            // 密钥
            byte[] Key = new byte[8];
            // 明文数据
            byte[] EncryptData = new byte[8];
            // 密文数据
            byte[] EncryptedData = new byte[8];

            random.NextBytes(Key);
            random.NextBytes(EncryptData);

            // 加密
            Console.WriteLine(BitConverter.ToString(Key));
            Console.WriteLine(BitConverter.ToString(EncryptData));
            EncryptedData = Lanwah.CSharp.NET.SecurityLib.DES.Instance.Encrypt(Key, EncryptData);
            Console.WriteLine(BitConverter.ToString(EncryptedData));
            // 解密
            EncryptData = Lanwah.CSharp.NET.SecurityLib.DES.Instance.Decrypt(Key, EncryptedData);
            Console.WriteLine(BitConverter.ToString(EncryptData));

            string sKey = "11000000";
            string sIV = "00000000";
            string sEncryptData = "888888";

            Console.WriteLine(sKey);
            Console.WriteLine(sIV);
            // 加密
            string sEncryptedData = Convert.ToBase64String(Lanwah.CSharp.NET.SecurityLib.DES.Instance.Encrypt(CipherMode.CBC, PaddingMode.PKCS7, Encoding.UTF8.GetBytes(sKey), Encoding.UTF8.GetBytes(sIV), Encoding.UTF8.GetBytes(sEncryptData)));
            Console.WriteLine(sEncryptedData);
            // 解密
            sEncryptData = Encoding.UTF8.GetString(Lanwah.CSharp.NET.SecurityLib.DES.Instance.Decrypt(CipherMode.CBC, PaddingMode.PKCS7, Encoding.UTF8.GetBytes(sKey), Encoding.UTF8.GetBytes(sIV), Convert.FromBase64String(sEncryptedData)));
            Console.WriteLine(sEncryptData);
        }
    }
}
