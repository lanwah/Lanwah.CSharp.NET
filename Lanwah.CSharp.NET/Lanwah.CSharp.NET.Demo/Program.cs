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
            // 3DES加密解密调试
            SecurityLib.DES.TripleDESTest.RunTest();
            Console.ReadKey();
        }
    }
}
