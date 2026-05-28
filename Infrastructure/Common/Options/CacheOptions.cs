using System.Text.Json.Serialization;

namespace Infrastructure.Common.Options
{
    public class CacheOptions
    {
        public CacheType CacheType { get; set; } = CacheType.Memory;
        public CacheRedisOptions? RedisOptions { get; set; }
    }
    [JsonConverter(typeof(JsonStringEnumConverter<CacheType>))]
    public enum CacheType
    {
        Memory,
        Redis
    }
    public class CacheRedisOptions
    {
        public string ConnectionStringName { get; set; } = "ConnectionStrings";
    }
}
