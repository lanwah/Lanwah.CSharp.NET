using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ------------------------------------------------------------ //
// 版权所有：CopyRight (C) LanwahSoft
// 项目名称：Lanwah.CSharp.NET.sln
// 文件名称：XMLSignature.cs
// 创 建 者：Lanwah
// 创建日期：2016-05-21
// 功能描述：XML数字签名
// 调用依赖：
// -------------------------------------------------------------
// 修 改 者：
// 修改时间：
// 修改原因：
// 修改描述：
// ------------------------------------------------------------ //

using System.Xml;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

namespace Lanwah.CSharp.NET.SecurityLib
{
    /// <summary>
    /// XML 数字签名
    /// </summary>
    public static partial class XMLSignature
    {
        /// <summary>
        /// 表示 System.Security.Cryptography.CspParameters 的密钥容器名称。
        /// </summary>
        public const string DefaultKeyContainerName = "XML_DSIG_RSA_KEY";

        /// <summary>
        /// 对XML文件内容进行数字签名
        /// </summary>
        /// <param name="document">XML文档对象（输入参数）</param>
        /// <param name="key">RSA算法（输入参数）</param>
        public static void SignXml(XmlDocument document, RSA key)
        {
            // 参数验证
            if (document == null)
            {
                throw new ArgumentException("document");
            }
            if (key == null)
            {
                throw new ArgumentException("key");
            }

            SignedXml XML = new SignedXml(document);
            XML.SigningKey = key;
            System.Security.Cryptography.Xml.Reference reference = new System.Security.Cryptography.Xml.Reference();
            reference.Uri = "";
            XmlDsigEnvelopedSignatureTransform transform = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(transform);
            XML.AddReference(reference);
            XML.ComputeSignature();
            XmlElement node = XML.GetXml();
            document.DocumentElement.AppendChild(document.ImportNode(node, true));
        }
        /// <summary>
        /// 验证对XML的数字签名是否正确
        /// </summary>
        /// <param name="document">XML文档对象（输入参数）</param>
        /// <param name="key">RSA算法（输入参数）</param>
        /// <returns>true： 签名验证成功；false： 签名验证失败</returns>
        public static bool VerifyXml(XmlDocument document, RSA key)
        {
            // 参数验证
            if (document == null)
            {
                throw new ArgumentException("document");
            }
            if (key == null)
            {
                throw new ArgumentException("key");
            }

            SignedXml XML = new SignedXml(document);
            XmlNodeList Element = document.GetElementsByTagName("Signature");
            if (Element.Count <= 0)
            {
                throw new CryptographicException("Verification failed: No Signature was found in the document.");
            }
            if (Element.Count >= 2)
            {
                throw new CryptographicException("Verification failed: More that one signature was found for the document.");
            }

            XML.LoadXml((XmlElement)Element[0]);
            return XML.CheckSignature(key);
        }
        /// <summary>
        /// 对XML文件内容进行数字签名
        /// </summary>
        /// <param name="document">XML文档对象（输入参数）</param>
        public static void SignXml(XmlDocument document)
        {
            CspParameters Param = new CspParameters();
            Param.KeyContainerName = DefaultKeyContainerName;
            RSACryptoServiceProvider Key = new RSACryptoServiceProvider(Param);

            SignXml(document, Key);
        }
        /// <summary>
        /// 验证对XML的数字签名是否正确
        /// </summary>
        /// <param name="document">XML文档对象（输入参数）</param>
        /// <returns>true： 签名验证成功；false： 签名验证失败</returns>
        public static bool VerifyXml(XmlDocument document)
        {
            CspParameters Param = new CspParameters();
            Param.KeyContainerName = DefaultKeyContainerName;
            RSACryptoServiceProvider Key = new RSACryptoServiceProvider(Param);

            return VerifyXml(document, Key);
        }
        /// <summary>
        /// 对XML文件内容进行数字签名
        /// </summary>
        /// <param name="xml">XML文件内容（引用参数）</param>
        /// <returns>"1" ： 成功；其他： 失败</returns>
        public static string SignXml(ref string xml)
        {
            // 参数验证
            if (true == string.IsNullOrEmpty(xml))
            {
                throw new ArgumentNullException("xmlContent");
            }

            try
            {
                XmlDocument document = new XmlDocument();
                document.PreserveWhitespace = true;
                document.LoadXml(xml);
                SignXml(document);
                xml = document.InnerXml;
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 验证对XML的数字签名是否正确
        /// </summary>
        /// <param name="xml">XML文件内容（引用参数）</param>
        /// <returns>"1" ： 成功；其他： 失败</returns>
        public static string VerifyXml(ref string xml)
        {
            // 参数验证
            if (true == string.IsNullOrEmpty(xml))
            {
                throw new ArgumentNullException("xmlContent");
            }

            try
            {
                XmlDocument document = new XmlDocument();
                document.PreserveWhitespace = true;
                document.LoadXml(xml);
                if (true == VerifyXml(document))
                {
                    // "The XML signature is valid(数字签名有效).";
                    xml = document.InnerXml;
                    return "1";
                }
                else
                {
                    // "The XML signature is not valid(数字签名无效).";
                    return "The XML signature is not valid(数字签名无效).";
                }
            }
            catch (CryptographicException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 对XML文件内容进行数字签名
        /// </summary>
        /// <param name="filePath">待签名的XML文件完整路径（输入参数）</param>
        /// <returns>"1" ： 成功；其他： 失败</returns>
        public static string SignXml(string filePath)
        {
            // 参数验证
            if (true == string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath");
            }
            if (false == System.IO.File.Exists(filePath))
            {
                // 文件不存在
                throw new System.IO.FileNotFoundException();
            }

            try
            {
                XmlDocument document = new XmlDocument();
                document.PreserveWhitespace = true;
                document.Load(filePath);
                SignXml(document);
                document.Save(filePath);
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 验证对XML的数字签名是否正确
        /// </summary>
        /// <param name="filePath">待验证的XML文件完整路径（输入参数）</param>
        /// <returns>"1" ： 成功；其他： 失败</returns>
        public static string VerifyXml(string filePath)
        {
            // 参数验证
            if (true == string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath");
            }
            if (false == System.IO.File.Exists(filePath))
            {
                // 文件不存在
                throw new System.IO.FileNotFoundException();
            }

            try
            {
                XmlDocument document = new XmlDocument();
                document.PreserveWhitespace = true;
                document.Load(filePath);
                if (true == VerifyXml(document))
                {
                    // "The XML signature is valid(数字签名有效).";
                    return "1";
                }
                else
                {
                    // "The XML signature is not valid(数字签名无效).";
                    return "The XML signature is not valid(数字签名无效).";
                }
            }
            catch (CryptographicException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
