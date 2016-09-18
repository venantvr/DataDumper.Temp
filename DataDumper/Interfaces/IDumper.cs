namespace DataDumper.Interfaces
{
    public interface IDumper
    {
        void Dispose();

        void Dump(string name, object value);
    }
}