using System.IO;
using DataDumper.Implementation;

namespace DataDumper.Repository
{
    public class DumpRepository<TStream> : BaseDumpRepository
        where TStream : Stream, new()
    {
        public DumpRepository() : base(new YamlDumper(new TStream()))
        {
        }

        public DumpRepository(TStream stream) : base(new YamlDumper(stream))
        {
        }
    }
}