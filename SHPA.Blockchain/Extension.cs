using System.Linq;
using System.Text;

namespace SHPA.Blockchain
{
    public static class Extension
    {
        public static string SHA256(this string data)
        {
            var sb = new StringBuilder();
            using var hash = System.Security.Cryptography.SHA256.Create();
            byte[] result = hash.ComputeHash(Encoding.UTF8.GetBytes(data));

            foreach (var b in result)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        public static string Multiply(this string input, int count)
        {
            return string.Concat(Enumerable.Repeat(input, count));
        }
    }
}