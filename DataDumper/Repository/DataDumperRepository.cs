using System.IO;
using DataDumper.Implementation;

namespace DataDumper.Repository
{
    public class DataDumperRepository : AbstractDumperRepository
    {
        public DataDumperRepository(Stream stream) : base(new YamlDumper(stream))
        {
        }
    }
}