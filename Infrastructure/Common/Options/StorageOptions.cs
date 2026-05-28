using Infrastructure.File;
using Infrastructure.FileServices;
using System.Text.Json.Serialization;

namespace Infrastructure.Common.Options
{
    public sealed class StorageOptions
    {
        public StorageType StorageType { get; set; } = StorageType.Local;
        public LocalStorageOptions? LocalStorageOptions { get; set; }
        public S3Options? S3Options { get; set; }
    }
    [JsonConverter(typeof(JsonStringEnumConverter<StorageType>))]
    public enum StorageType
    {
        Local,
        S3
    }
}
