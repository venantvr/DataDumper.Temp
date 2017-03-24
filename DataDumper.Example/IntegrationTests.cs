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
        private NorthwindContextBase _ctx;

        private DataDumperRepository _repository;
        private TransactionScope _transaction;

        [TestInitialize]
        public void Initialize()
        {
            _repository = new DataDumperRepository(_stream);
            _ctx = new NorthwindContextBase();
            _transaction = new TransactionScope();
        }

        [TestMethod]
        public void Business()
        {
        }

        [TestCleanup]
        public void Cleanup()
        {
            _transaction.Dispose();
            _ctx.Dispose();
            _repository.Dispose();
        }
    }
}