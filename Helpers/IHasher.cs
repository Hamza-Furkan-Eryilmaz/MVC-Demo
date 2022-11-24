namespace MvcDemo.Helpers
{
    public interface IHasher
    {
        string MD5SaltAndHash(string s);
    }
}