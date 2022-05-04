using System.Security.Cryptography;
using System.Text;

namespace Logic.PasswordHasher;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        var md5 = MD5.Create();
        var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));

        return Convert.ToBase64String(hash);
    }
}
