using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanwah.CSharp.NET.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rdm = new Random();

            byte[] bb = new byte[10];
            rdm.NextBytes(bb);
            string s = CommonLib.BitConverter.ToHexString(bb);
            Console.WriteLine(s);
            Console.WriteLine(BitConverter.ToString(bb));

            //// 3DES加密解密调试
            //SecurityLib.DES.TripleDESTest.RunTest();
            // // DES加密解密调试
            //SecurityLib.DES.DESTest.RunTest();
            Console.ReadKey();
        }
    }
}
