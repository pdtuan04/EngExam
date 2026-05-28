namespace Infrastructure.Common.Options
{
    public sealed class MessageBrokerOptions
    {
        public string Host { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
