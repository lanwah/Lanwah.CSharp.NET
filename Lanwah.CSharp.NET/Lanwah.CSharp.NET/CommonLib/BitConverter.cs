using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ------------------------------------------------------------ //
// 版权所有：CopyRight (C) LanwahSoft
// 项目名称：Lanwah.CSharp.NET.sln
// 文件名称：BitConverter.cs
// 创 建 者：Lanwah
// 创建日期：2016-05-21
// 功能描述：扩展系统的BitConverter类
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
    /// 扩展系统的BitConverter类
    /// </summary>
    public static partial class BitConverter
    {
        /// <summary>
        /// 将指定的字节数组的每个元素的数值转换为它的等效十六进制字符串表示形式
        /// </summary>
        /// <param name="value">字节数组。（输入参数）</param>
        /// <returns>十六进制对构成的 System.String，其中每一对表示 value 中对应的元素；例如“7F2C4A”。</returns>
        public static string ToHexString(byte[] value)
        {
            string sHexString = System.BitConverter.ToString(value);
            return sHexString.Replace("-", "");
        }
    }
}
