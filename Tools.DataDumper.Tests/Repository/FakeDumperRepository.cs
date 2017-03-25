using System.IO;
using Tools.DataDumper.Repository;

namespace Tools.DataDumper.Tests.Repository
{
    public class FakeDumperRepository : AbstractDumperRepository
    {
        public FakeDumperRepository(Stream stream) : base(new YamlDumper(stream))
        {
        }
    }
}