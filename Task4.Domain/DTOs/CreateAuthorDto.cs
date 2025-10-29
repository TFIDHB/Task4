using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4.Domain.DTOs
{
    public class CreateAuthorDto
    {  
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
    }
}
