using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tools.DataDumper.Implementation;
using Tools.DataDumper.Repository;
using Tools.DataDumper.Tests.Repository;

namespace Tools.DataDumper.Tests
{
    [TestClass]
    public class DataDumperTests : BaseDataDumperTests
    {
        [TestMethod]
        public void Test_01()
        {
            new YamlDumper(Console.OpenStandardOutput()).Dump("Client", Client);
        }

        [TestMethod]
        public void Test_02()
        {
            using (var stream = new MemoryStream())
            {
                new YamlDumper(stream).Dump("Client", Client);

                var buffer = stream.GetBuffer();

                Assert.IsTrue(CalculateMd5Hash(Encoding.UTF8.GetString(buffer, 0, buffer.Length)) == "3657502FFDEE3E151EEAEB1610B59555");
            }
        }

        [TestMethod]
        public void Test_03()
        {
            using (var stream = new MemoryStream())
            {
                new YamlDumper(stream).Dump(Client);

                var buffer = stream.GetBuffer();

                Assert.IsTrue(CalculateMd5Hash(Encoding.UTF8.GetString(buffer, 0, buffer.Length)) == "3657502FFDEE3E151EEAEB1610B59555");
            }
        }

        [TestMethod]
        public void Test_04()
        {
            var i = 0;
            var j = 0;
            var k = 0;

            using (var stream = new MemoryStream())
            {
                using (var repository = new FakeDumperRepository(stream))
                {
                    // ReSharper disable once AccessToModifiedClosure
                    repository.Add<int>("() => i + j", () => i + j);
                    // ReSharper disable once AccessToModifiedClosure
                    repository.Add<int>("() => i + k", () => i + k);
                    // ReSharper disable once AccessToModifiedClosure
                    repository.Add<int>("() => j + k", () => j + k);

                    i = 1;
                }

                var buffer = stream.GetBuffer();

                Assert.IsTrue(CalculateMd5Hash(Encoding.UTF8.GetString(buffer, 0, buffer.Length)) == "96CB9836AFAE7484F011FBB2EF507E7D");
            }
        }

        [TestMethod]
        public void Test_05()
        {
            var i = 0;
            var j = 0;
            var k = 0;

            using (var stream = new MemoryStream())
            {
                using (var repository = new DataDumperRepository(stream))
                {
                    // ReSharper disable once AccessToModifiedClosure
                    repository.Add<int>("() => i + j", () => i + j);
                    // ReSharper disable once AccessToModifiedClosure
                    repository.Add<int>("() => i + k", () => i + k);
                    // ReSharper disable once AccessToModifiedClosure
                    repository.Add<int>("() => j + k", () => j + k);

                    i = 1;
                }

                var buffer = stream.GetBuffer();

                Assert.IsTrue(CalculateMd5Hash(Encoding.UTF8.GetString(buffer, 0, buffer.Length)) == "96CB9836AFAE7484F011FBB2EF507E7D");
            }
        }

        [TestMethod]
        public void Test_06()
        {
            var i = 0;
            var j = 0;
            var k = 0;

            var stream = Console.OpenStandardOutput();

            using (var repository = new DataDumperRepository(stream))
            {
                // ReSharper disable once AccessToModifiedClosure
                repository.Add<int>("() => i + j", () => i + j);
                // ReSharper disable once AccessToModifiedClosure
                repository.Add<int>("() => i + k", () => i + k);
                // ReSharper disable once AccessToModifiedClosure
                repository.Add<int>("() => j + k", () => j + k);

                i = 1;
            }
        }
    }
}