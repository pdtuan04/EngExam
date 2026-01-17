using System.Text.Json.Serialization;

namespace EngExam.OptionsModels
{
    public class AIOptions
    {
        public AIModel ModelType { get; set; } = AIModel.OpenAI;
        public OpenAIOption OpenAIOptions { get; set; }
        public GeminiOption GeminiOptions { get; set; }


    }
    [JsonConverter(typeof(JsonStringEnumConverter<AIModel>))]
    public enum AIModel
    {
        OpenAI,
        Gemini
    }
    public class OpenAIOption
    {
        public required string ModelOptions { get; set; }
        public required string API_Key { get; set; } = "AI_API_Key";
    }
    public class GeminiOption
    {
        public required string ModelOptions { get; set; }
        public required string API_Key { get; set; } = "AI_API_Key";
    }
}
