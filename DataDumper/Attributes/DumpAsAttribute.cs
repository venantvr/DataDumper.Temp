using System;

namespace DataDumper.Attributes
{
    public class DumpAsAttribute : Attribute
    {
        public DumpAsAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}