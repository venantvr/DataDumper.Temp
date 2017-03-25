using System.IO;
using Tools.DataDumper.Implementation;

namespace Tools.DataDumper.Repository
{
    public class DataDumperRepository : AbstractDumperRepository
    {
        public DataDumperRepository(Stream stream) : base(new YamlDumper(stream))
        {
        }
    }
}