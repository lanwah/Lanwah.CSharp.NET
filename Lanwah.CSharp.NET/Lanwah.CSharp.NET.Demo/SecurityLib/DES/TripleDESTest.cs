using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Lanwah.CSharp.NET.Demo.SecurityLib.DES
{
    /// <summary>
    /// TripleDES测试
    /// </summary>
    public sealed partial class TripleDESTest
    {
        /// <summary>
        /// 测试
        /// </summary>
        public static void RunTest()
        {
            Random random = new Random();

            // 密钥
            byte[] Key = new byte[16];
            // 明文数据
            byte[] EncryptData = new byte[8];
            // 密文数据
            byte[] EncryptedData = new byte[8];

            random.NextBytes(Key);
            random.NextBytes(EncryptData);

            // 加密
            Console.WriteLine(BitConverter.ToString(Key));
            Console.WriteLine(BitConverter.ToString(EncryptData));
            EncryptedData = Lanwah.CSharp.NET.SecurityLib.TripleDES.Instance.Encrypt(Key, EncryptData);
            Console.WriteLine(BitConverter.ToString(EncryptedData));
            // 解密
            EncryptData = Lanwah.CSharp.NET.SecurityLib.TripleDES.Instance.Decrypt(Key, EncryptedData);
            Console.WriteLine(BitConverter.ToString(EncryptData));

            // 支付宝的3DES加密解密
            string sKey = Convert.ToBase64String(Key);
            string sEncryptData = Convert.ToBase64String(EncryptData);

            Console.WriteLine(sKey);
            Console.WriteLine(sEncryptData);
            // 加密
            string sEncryptedData = Convert.ToBase64String(Lanwah.CSharp.NET.SecurityLib.TripleDES.Instance.Encrypt(CipherMode.CBC, PaddingMode.PKCS7, Convert.FromBase64String(sKey), new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 }, EncryptData));
            Console.WriteLine(sEncryptedData);
            // 解密
            sEncryptData = Convert.ToBase64String(Lanwah.CSharp.NET.SecurityLib.TripleDES.Instance.Decrypt(CipherMode.CBC, PaddingMode.PKCS7, Convert.FromBase64String(sKey), new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 }, Convert.FromBase64String(sEncryptedData)));
            Console.WriteLine(sEncryptData);
        }
    }
}
