using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Security.Cryptography;

// ------------------------------------------------------------ //
// 版权所有：CopyRight (C) LanwahSoft
// 项目名称：Lanwah.CSharp.NET.sln
// 文件名称：ImageCode.cs
// 创 建 者：Lanwah
// 创建日期：2016-05-29
// 功能描述：图片验证码
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
    /// 图片验证码
    /// </summary>
    public sealed partial class ImageCode
    {
        #region // ============== Fields ============== //
        /// <summary>
        /// 验证码内容
        /// </summary>
        private string _text;
        #endregion

        #region // ============= Property ============= //
        /// <summary>
        /// 获取或设置验证码内容
        /// </summary>
        public string Text
        {
            get
            {
                if (true == string.IsNullOrEmpty(this._text))
                {
                    this._text = GetRandomNumber(4);
                }
                return this._text;
            }
            private set { this._text = value; }
        }
        /// <summary>
        /// 星型点缀个数
        /// </summary>
        private int PixelStarCount = 70;
        /// <summary>
        /// 线条的数量
        /// </summary>
        private int LineCount = 4;
        /// <summary>
        /// 字体大小
        /// </summary>
        private int FontSize = 14;
        /// <summary>
        /// 单个字体的宽度范围
        /// </summary>
        private int Width = 17;
        /// <summary>
        /// 单个字体的高度范围
        /// </summary>
        private int Height = 25;
        /// <summary>
        /// 字体列表
        /// </summary>
        private Font[] Fonts = new Font[4];
        /// <summary>
        /// PI
        /// </summary>
        private const double PI = 6.283185307179586476925286766559;
        /// <summary>
        /// 加密随机数生成器
        /// </summary>
        private static RNGCryptoServiceProvider RngRandom = new RNGCryptoServiceProvider();
        /// <summary>
        /// 验证码图片
        /// </summary>
        public Image CodeImage
        {
            get
            {
                return this.CreateImage();
            }
        }
        #endregion

        #region // ======= Constructors Methods ======= //
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="text">验证码内容</param>
        /// <param name="width">单个验证码宽度</param>
        /// <param name="heigth">当个验证码高度</param>
        /// <param name="lineCount">直线条数</param>
        /// <param name="pixelStarCount">彩色星个数</param>
        /// <param name="fontSize">字体</param>
        public ImageCode(string text, int width = 17, int heigth = 25, int lineCount = 4, int pixelStarCount = 70, int fontSize = 14)
        {
            this.Text = text;
            this.Width = width;
            this.Height = heigth;
            this.LineCount = lineCount;
            this.PixelStarCount = pixelStarCount;
            this.FontSize = fontSize;

            // 字体样式
            Fonts[0] = new Font(new FontFamily("Times New Roman"), FontSize, System.Drawing.FontStyle.Regular);
            Fonts[1] = new Font(new FontFamily("Georgia"), FontSize, System.Drawing.FontStyle.Regular);
            Fonts[2] = new Font(new FontFamily("Arial"), FontSize, System.Drawing.FontStyle.Regular);
            Fonts[3] = new Font(new FontFamily("Comic Sans MS"), FontSize, System.Drawing.FontStyle.Regular);
        }
        #endregion

        #region // =========== Class Methods ========== //
        /// <summary>
        /// 生成0-9之间的随机数字
        /// </summary>
        /// <param name="length">生成长度</param>
        private static string GetRandomNumber(int length)
        {
            string sRandomNum = string.Empty;
            System.Random Random = new Random();

            for (int i = 0; i < length; i++)
            {
                sRandomNum += Random.Next(0, 10).ToString();
            }
            return sRandomNum;
        }
        /// <summary>
        /// 获得一个无符号随机整数
        /// </summary>
        /// <param name="maxValue">最大值</param>
        private static int Next(int maxValue)
        {
            byte[] Buffer = new byte[4];
            RngRandom.GetBytes(Buffer);
            int nValue = BitConverter.ToInt32(Buffer, 0);
            nValue = nValue % (maxValue + 1);
            nValue = Math.Abs(nValue);

            return nValue;
        }
        /// <summary>
        /// 获得一个介于最小值与最大值之间的无符号随机整数
        /// </summary>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        private static int Next(int minValue, int maxValue)
        {
            return Next(maxValue - minValue) + minValue;
        }
        /// <summary>
        /// 字体随机颜色
        /// </summary>
        private Color GetRandomColor()
        {
            Random Random = new Random((int)DateTime.Now.Ticks);
            System.Threading.Thread.Sleep(Random.Next(1, 10));
            int nRed = Random.Next(0, 180);
            System.Threading.Thread.Sleep(Random.Next(1, 10));
            int nGreen = Random.Next(0, 180);
            System.Threading.Thread.Sleep(Random.Next(1, 10));
            int nBlue = (nRed + nGreen > 300) ? 0 : 400 - nRed - nGreen;
            nBlue = (nBlue > 255) ? 255 : nBlue;
            return Color.FromArgb(nRed, nGreen, nBlue);
        }
        /// <summary>
        /// 正弦曲线Wave扭曲图片
        /// </summary>
        /// <param name="sourceImage">图片</param>
        /// <param name="isTwist">是否扭曲图片</param>
        /// <param name="muitipleWave">波形的幅度倍数，越大扭曲的程度越高,一般为3</param>
        /// <param name="phase">波形的起始相位,取值区间[0-2*PI)</param>
        private System.Drawing.Bitmap TwistImage(Bitmap sourceImage, bool isTwist, double muitipleWave, double phase)
        {
            // 变量定义
            double dBaseAxisLen = 0;
            double dX = 0;
            double dY = 0;
            int nX = 0;
            int nY = 0;

            Bitmap DestinationImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            Graphics AGraphics = Graphics.FromImage(DestinationImage);
            AGraphics.FillRectangle(new SolidBrush(Color.White), 0, 0, DestinationImage.Width, DestinationImage.Height);
            AGraphics.Dispose();
            dBaseAxisLen = isTwist ? (double)DestinationImage.Height : (double)DestinationImage.Width;
            for (int i = 0; i < DestinationImage.Width; i++)
            {
                for (int j = 0; j < DestinationImage.Height; j++)
                {
                    dX = isTwist ? (PI * (double)j) / dBaseAxisLen : (PI * (double)i) / dBaseAxisLen;
                    dX += phase;
                    dY = Math.Sin(dX);
                    nX = isTwist ? i + (int)(dY * muitipleWave) : i;
                    nY = isTwist ? j : j + (int)(dY * muitipleWave);

                    Color color = sourceImage.GetPixel(i, j);
                    if (nX >= 0 && nX < DestinationImage.Width
                     && nY >= 0 && nY < DestinationImage.Height)
                    {
                        DestinationImage.SetPixel(nX, nY, color);
                    }
                }
            }
            sourceImage.Dispose();

            return DestinationImage;
        }


        /// <summary>
        /// 绘制验证码
        /// </summary>
        /// <returns></returns>
        public Image CreateImage()
        {
            int nImageWidth = this.Text.Length * Width;
            Bitmap Image = new Bitmap(nImageWidth, Height);
            Graphics AGraphics = Graphics.FromImage(Image);
            AGraphics.Clear(Color.White);
            // 绘制两条线条
            for (int i = 0; i < this.LineCount; i++)
            {
                int nX1 = Next(Image.Width - 2);
                int nX2 = Next(Image.Width - 2);
                int nY1 = Next(Image.Height - 2);
                int nY2 = Next(Image.Height - 2);
                AGraphics.DrawLine(new Pen(Color.Silver), nX1, nY1, nX2, nY2);
            }
            int nX = -12, nY = 0;
            for (int i = 0; i < this.Text.Length; i++)
            {
                nX += Next(12, FontSize);
                nY = Next(-2, 2);
                string sCharacter = this.Text.Substring(i, 1);
                sCharacter = Next(1) == 1 ? sCharacter.ToLower() : sCharacter.ToUpper();
                Brush newBrush = new SolidBrush(GetRandomColor());
                Point thePos = new Point(nX, nY);
                AGraphics.DrawString(sCharacter, Fonts[Next(Fonts.Length - 1)], newBrush, thePos);
            }
            for (int i = 0; i < this.PixelStarCount; i++)
            {
                nX = Next(Image.Width - 1);
                nY = Next(Image.Height - 1);
                Image.SetPixel(nX, nY, Color.FromArgb(Next(0, 255), Next(0, 255), Next(0, 255)));
            }
            Image = TwistImage(Image, true, Next(1, 3), Next(0, 6));
            AGraphics.DrawRectangle(new Pen(Color.LightGray, 1), 0, 0, nImageWidth - 1, (Height - 1));

            return Image;
        }
        /// <summary>
        /// 生成指定位数的随机数
        /// </summary>
        /// <param name="length">随机数的长度（输入参数）</param>
        /// <returns></returns>
        public static string GetRandom(int length)
        {
            string sRet = string.Empty;
            Random Rand = new Random();
            for (int i = 0; i < length; i++)
            {
                sRet += Rand.Next(0, 10);
            }
            return sRet;
        }
        #endregion
    }
}
