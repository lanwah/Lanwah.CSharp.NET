using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ------------------------------------------------------------ //
// 版权所有：CopyRight (C) LanwahSoft
// 项目名称：Lanwah.CSharp.NET.sln
// 文件名称：Path.cs
// 创 建 者：Lanwah
// 创建日期：2016-05-21
// 功能描述：扩展系统的System.IO.Path类
// 调用依赖：
// -------------------------------------------------------------
// 修 改 者：
// 修改时间：
// 修改原因：
// 修改描述：
// ------------------------------------------------------------ //

namespace Lanwah.CSharp.NET.CommonLib
{
    /// <summary>
    /// 扩展系统的System.IO.Path类
    /// </summary>
    public static partial class Path
    {
        /// <summary>
        /// 判断目录是否存在
        /// </summary>
        /// <param name="path">要测试的路径（输入参数）</param>
        /// <returns>true： 目录存在；false： 目录不存在</returns>
        public static bool IsDirectoryExists(string path)
        {
            return System.IO.Directory.Exists(path);
        }
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="filePath">文件的完整路径（输入参数）</param>
        /// <returns>true： 文件存在；false： 文件不存在</returns>
        public static bool IsFileExists(string filePath)
        {
            return System.IO.File.Exists(filePath);
        }
        /// <summary>
        /// 判断文件是否有扩展名
        /// </summary>
        /// <param name="filePath">文件完整路径（输入参数）</param>
        /// <returns>true： 文件有扩展名；false： 文件没有扩展名</returns>
        public static bool HasExtension(string filePath)
        {
            return System.IO.Path.HasExtension(filePath);
        }

        /// <summary>
        /// 格式化目录路径
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <returns>路径格式（D:\Program Files\Git\Lanwah.CSharp.NET\）注意尾部字符</returns>
        public static string DirectoryFormate(string path)
        {
            if (true == string.IsNullOrEmpty(path))
            {
                return path;
            }

            char DirectorySeparatorChar = System.IO.Path.DirectorySeparatorChar;
            path = path.TrimEnd(DirectorySeparatorChar) + DirectorySeparatorChar;
            return System.IO.Path.GetDirectoryName(path) + DirectorySeparatorChar.ToString();
        }
        /// <summary>
        /// 根据文件目录和文件名获取文件完整路径
        /// </summary>
        /// <param name="path">文件目录（输入参数）</param>
        /// <param name="fileName">文件名（输入参数）</param>
        /// <returns>文件完整路径</returns>
        public static string Combine(string path, string fileName)
        {
            // 参数判断
            if (true == string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }
            if (true == string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            path = DirectoryFormate(path);
            fileName = System.IO.Path.GetFileName(fileName);
            return (path + fileName);
        }
    }
}
