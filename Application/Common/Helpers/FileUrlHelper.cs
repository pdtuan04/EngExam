using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Helpers
{
    public static class FileUrlHelper
    {
        private static string _baseUrl = string.Empty;
        public static void Initialize(string baseUrl)
        {
            _baseUrl = baseUrl;
        }
        public static string? GetFileUrl(this string? filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return null;
            }
            if (filePath.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                filePath.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                return filePath;
            }
            return $"{_baseUrl}/{Uri.EscapeDataString(filePath)}";
        }
    }
}
