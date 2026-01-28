using System.Text.Json.Serialization;

namespace EngExam.OptionsModels
{
    public class ExternalAuthOptions
    {
        public ExternalAuthTypes ExternalAuthTypes { get; set; } = ExternalAuthTypes.Google;
        public GoogleAuthOptions? GoogleAuthOptions { get; set; }
        //Có gì thêm các loại auth khác ở đây
    }
    [JsonConverter(typeof(JsonStringEnumConverter<ExternalAuthTypes>))]
    public enum ExternalAuthTypes
    {
        Google
    }
    public class GoogleAuthOptions
    {
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
    }
}
