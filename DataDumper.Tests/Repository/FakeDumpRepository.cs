using System.IO;
using DataDumper.Implementation;
using DataDumper.Repository;

namespace DataDumper.Tests.Repository
{
    public class FakeDumpRepository : AbstractDumpRepository
    {
        public FakeDumpRepository(Stream stream) : base(new YamlDumper(stream))
        {
        }
    }
}