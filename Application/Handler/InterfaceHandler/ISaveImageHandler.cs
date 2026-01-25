using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.InterfaceHandler
{
    public interface ISaveImageHandler
    {
        Task<string> SaveImageAsync(Stream stream, string fileName);
    }
}
