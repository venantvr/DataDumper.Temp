using System.Security.Cryptography;
using System.Text;
using DataDumper.Tests.Models;

namespace DataDumper.Tests
{
    public class BaseDataDumperTests
    {
        protected Client Client => new Client { LastName = "AAAA", FirstName = "BBBB" };

        protected string CalculateMd5Hash(string input)
        {
            var md5 = MD5.Create();

            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();

            foreach (var t in hash)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}