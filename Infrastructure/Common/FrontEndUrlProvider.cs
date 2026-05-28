using Application.Common.Interfaces;
using Infrastructure.Common.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public sealed class FrontEndUrlProvider : IFrontEndUrlProvider
    {
        private readonly FrontendOptions _options;
        public FrontEndUrlProvider(IOptions<FrontendOptions> options)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
        }
        public string GetFrontEndUrlAsync()
        {
            return _options.BaseUrl;
        }
        public string GetResetPasswordUrlAsync()
        {
            return $"{_options.BaseUrl}{_options.ResetPasswordPath}";
        }
    }
}