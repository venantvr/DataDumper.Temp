using System;
using System.IO;
using DataDumper.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataDumper.Example
{
    [TestClass]
    public class IntegrationTests
    {
        private readonly Stream _stream = Console.OpenStandardOutput();
        private DataDumperRepository _repository;

        [TestInitialize]
        public void Initialize()
        {
            _repository = new DataDumperRepository(_stream);
        }

        [TestMethod]
        public void Business()
        {
        }

        [TestCleanup]
        public void Cleanup()
        {
            _repository.Dispose();
        }
    }
}