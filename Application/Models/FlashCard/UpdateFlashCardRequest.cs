using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.FlashCard
{
       public  record UpdateFlashCardRequest(
        Guid Id,
        string Title,
        string? Description);
}
