using DataDumper.Repository;

namespace DataDumper.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var i = 0;
            var j = 0;
            var k = 0;

            var stream = System.Console.OpenStandardOutput();

            using (var repository = new DataDumperRepository(stream))
            {
                // ReSharper disable once AccessToModifiedClosure
                repository.Add<int>("() => i + j", () => i + j);
                // ReSharper disable once AccessToModifiedClosure
                repository.Add<int>("() => i + k", () => i + k);
                // ReSharper disable once AccessToModifiedClosure
                repository.Add<int>("() => j + k", () => j + k);

                i = 1;
            }

            System.Console.ReadKey();
        }
    }
}