namespace DataDumper.Interfaces
{
    public interface IDumper
    {
        void Dispose();

        void Dump<T>(string name, T value);
    }
}