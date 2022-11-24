using NETCore.Encrypt.Extensions;

namespace MvcDemo.Helpers
{
    public class Hasher : IHasher
    {
        private readonly IConfiguration _configuration;

        public Hasher(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Hasher()
        {

        }
        public string MD5SaltAndHash(string s)
        {
            string md5Salt = _configuration.GetValue<string>("AppSettings:Md5Salt");

            string salted = s + md5Salt;
            string hashed = salted.MD5();
            return hashed;
        }
    }
}
