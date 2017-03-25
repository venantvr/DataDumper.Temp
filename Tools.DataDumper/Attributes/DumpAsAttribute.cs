using System;

namespace Tools.DataDumper.Attributes
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