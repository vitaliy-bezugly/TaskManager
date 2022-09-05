using System.Security.Cryptography;
using System.Text;

namespace Hashing;

public class Sha256Alghorithm
{
    public static string GenerateHash(string input)
    {
        if (input == null)
            throw new ArgumentNullException("Input can not be null");
        // Create a SHA256   
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // ComputeHash - returns byte array  
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Convert byte array to a string   
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
