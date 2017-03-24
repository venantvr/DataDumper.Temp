using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using DataDumper.Implementation;
using DataDumper.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataDumper.Tests
{
    [TestClass]
    public class DataDumperTests
    {
        private string CalculateMd5Hash(string input)
        {
            var md5 = MD5.Create();

            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();

            foreach (var t in hash)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString();
        }

        [TestMethod]
        public void Test_01()
        {
            var client = new Client { LastName = "AAAA", FirstName = "BBBB" };

            new YamlDumper(Console.OpenStandardOutput()).Dump("client", client);
        }

        [TestMethod]
        public void Test_02()
        {
            var stream = new MemoryStream();
            var client = new Client { LastName = "AAAA", FirstName = "BBBB" };

            new YamlDumper(stream).Dump("Client", client);

            var buffer = stream.GetBuffer();

            Assert.IsTrue(CalculateMd5Hash(Encoding.UTF8.GetString(buffer, 0, buffer.Length)) == "3657502FFDEE3E151EEAEB1610B59555");
        }

        [TestMethod]
        public void Test_03()
        {
            var stream = new MemoryStream();
            var client = new Client { LastName = "AAAA", FirstName = "BBBB" };

            new YamlDumper(stream).Dump(client);

            var buffer = stream.GetBuffer();

            Assert.IsTrue(CalculateMd5Hash(Encoding.UTF8.GetString(buffer, 0, buffer.Length)) == "3657502FFDEE3E151EEAEB1610B59555");
        }
    }
}