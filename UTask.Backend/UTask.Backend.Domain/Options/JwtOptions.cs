namespace UTask.Backend.Domain.Options
{
    public class JwtOptions
    {
        public JwtOptions()
        {
        }

        public JwtOptions(JwtOptions options)
        {
            Key = options.Key;
            Issuer = options.Issuer;
            Audience = options.Audience;
            ExpirationTime = options.ExpirationTime;
        }

        public string? Key { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public int ExpirationTime { get; set; }
    }
}
