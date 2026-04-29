using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer_Read.DataContext
{
    public class Word
    {
        public required Guid Id { get; set; }
        public required string Text { get; set; }
        public required string Meaning { get; set; }

    }
}
