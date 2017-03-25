using System;

namespace Tools.DataDumper.Interfaces
{
    public interface IDumperLogic
    {
        Func<object> Expression { get; }
    }
}