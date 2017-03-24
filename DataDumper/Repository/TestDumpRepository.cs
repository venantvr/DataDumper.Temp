using System.IO;
using DataDumper.Implementation;

namespace DataDumper.Repository
{
    public class TestDumpRepository : AbstractDumpRepository
    {
        public TestDumpRepository(Stream stream) : base(new YamlDumper(stream))
        {
        }
    }
}