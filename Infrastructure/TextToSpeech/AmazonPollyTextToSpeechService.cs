using Amazon.Polly;
using Amazon.Polly.Model;
using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TextToSpeech
{
    public sealed class AmazonPollyTextToSpeechService : ITextToSpeechService
    {
        private readonly IAmazonPolly _amazonPolly;
        public AmazonPollyTextToSpeechService(IAmazonPolly amazonPolly)
        {
            _amazonPolly = amazonPolly;
        }
        public async Task<Stream> ConvertTextToSpeechAsync(string text, CancellationToken cancellationToken)
        {
            var request = new SynthesizeSpeechRequest
            {
                Text = text,
                OutputFormat = OutputFormat.Mp3,
                VoiceId = VoiceId.Joanna,
                Engine = Engine.Standard
            };
            using var response = await _amazonPolly.SynthesizeSpeechAsync(request, cancellationToken);
            var memoryStream = new MemoryStream();
            await response.AudioStream.CopyToAsync(memoryStream, cancellationToken);
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}
