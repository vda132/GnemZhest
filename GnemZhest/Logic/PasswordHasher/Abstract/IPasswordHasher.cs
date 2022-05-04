namespace Logic.PasswordHasher;

public interface IPasswordHasher
{
    string HashPassword(string password);
}
