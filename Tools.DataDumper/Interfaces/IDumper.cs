using System.IO;

namespace Tools.DataDumper.Interfaces
{
    public interface IDumper
    {
        Stream BaseStream { get; }

        void Dispose();

        void Dump<T>(string name, T value);
    }
}