using System.Text.Json.Serialization;

namespace Infrastructure.Common.Options
{
    public class RepositoryOptions
    {
        public RepositoryType Type { get; set; } = RepositoryType.SQLServer;
    }
    [JsonConverter(typeof(JsonStringEnumConverter<RepositoryType>))]
    public enum RepositoryType
    {
        SQLServer,
        InMemory
    }
}
