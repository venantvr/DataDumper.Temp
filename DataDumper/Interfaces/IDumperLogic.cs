using System;

namespace DataDumper.Interfaces
{
    public interface IDumperLogic
    {
        Func<object> Expression { get; }
    }
}