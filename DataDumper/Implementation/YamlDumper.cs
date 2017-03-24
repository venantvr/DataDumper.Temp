using System;
using System.IO;
using System.Reflection;
using DataDumper.Interfaces;
using YamlDotNet.Serialization;

namespace DataDumper.Implementation
{
    public class YamlDumper : IDisposable, IDumper
    {
        private readonly Serializer _serializer = new Serializer();
        private readonly StreamWriter _sw;

        public YamlDumper(FileInfo file)
        {
            var location = Assembly.GetExecutingAssembly().Location;

            var path = $"{Path.GetDirectoryName(location)}\\{file.Name}";

            _sw = new StreamWriter(path) { AutoFlush = true };
        }

        public YamlDumper(Stream stream)
        {
            _sw = new StreamWriter(stream) { AutoFlush = true };
        }

        public void Dispose()
        {
            _sw.Close();
        }

        public void Dump<T>(string name, T value)
        {
            _serializer.Serialize(_sw, new { Name = name, Object = value });
        }

        public void Dump<T>(T value)
        {
            var name = typeof (T).Name;

            _serializer.Serialize(_sw, new { Name = name, Object = value });
        }
    }
}