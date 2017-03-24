using System.IO;
using DataDumper.Implementation;
using DataDumper.Repository;

namespace DataDumper.Tests.Repository
{
    public class FakeDumperRepository : AbstractDumperRepository
    {
        public FakeDumperRepository(Stream stream) : base(new YamlDumper(stream))
        {
        }
    }
}