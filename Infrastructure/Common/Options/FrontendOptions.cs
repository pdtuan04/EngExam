using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Options
{
    public sealed class FrontendOptions
    {
        public string BaseUrl { get; set; } = "http://localhost:3000";
        public string ResetPasswordPath { get; set; } = "/reset-password";
    }
}
