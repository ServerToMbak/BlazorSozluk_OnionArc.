using System.Security.Cryptography;
using System.Text;

namespace BlazorSozluk.Common.Infrastructure;

public class PassworEncrypter
{
    public static string Encrpt(string Password)
    {
        using var md5 = MD5.Create();
        byte[] inputByte = Encoding.ASCII.GetBytes(Password);
        byte[] hashBytes = md5.ComputeHash(inputByte);

        return Convert.ToHexString(hashBytes);
    }
}
