using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmsSolution.Domain.Interfaces
{
    public interface ICreatable
    {
        DateTime CreatedAt { get; set; }
    }
}
