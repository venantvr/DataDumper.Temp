using System;
using System.IO;
using System.Transactions;
using DataDumper.NorthWind;
using DataDumper.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataDumper.Example
{
    [TestClass]
    public class IntegrationTests
    {
        private readonly Stream _stream = Console.OpenStandardOutput();
        private NorthWindContextBase _ctx;

        private DataDumperRepository _repository;
        private TransactionScope _transaction;

        [TestInitialize]
        public void Initialize()
        {
            _ctx = new NorthWindContextBase();
            _transaction = new TransactionScope();
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
            _transaction.Dispose();
            _ctx.Dispose();
        }
    }
}