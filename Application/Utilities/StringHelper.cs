using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication.Utilities
{
    public static class StringHelper
    {
        public static string NormalizePage(string page)
        {
            if (string.IsNullOrEmpty(page)) return "/";
            page.TrimEnd('/');
            if (string.IsNullOrEmpty(page) )
                return "/";
            return page.ToLowerInvariant();
        }
    }
}
