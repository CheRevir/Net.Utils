using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Utils.Tests
{
    [TestClass()]
    public class IniFileTests
    {
        [TestMethod()]
        public void IniFileTest()
        {
            IniFile ini = new IniFile(AppDomain.CurrentDomain.BaseDirectory + @"\Config", "abc.ini");
            Console.WriteLine("配置文件目录{0}，配置文件{1}", ini.FileDirectory, ini.FileName);
            Console.WriteLine(ini.FilePath);
            // Assert.IsTrue(File.Exists(ini.FilePath));
            ini.Write("int", "int", 1);
            //Assert.AreEqual(ini.ReadInt("int", "int", 0), 1);
            ini.Write("string", "string", "string");
            //Console.WriteLine(ini.ReadString("string", "string", "s"));
        }
    }
}