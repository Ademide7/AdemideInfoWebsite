namespace AdemideInfoWebsite.SharedKernel;

public static class Utilities
{
    //password hashing method with salt.
    public static string HashPassword(string password, string salt)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var saltedPassword = password + salt;
        var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(saltedPassword));
        return Convert.ToBase64String(hashedBytes);
    }

    // GenerateRandomPassword method with count parameter.
    public static string GenerateRandomPassword(int length)
    {
        const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        var random = new Random();
        var password = new char[length];
        for (int i = 0; i < length; i++)
        {
            password[i] = validChars[random.Next(validChars.Length)];
        }
        return new string(password);
    }
}
