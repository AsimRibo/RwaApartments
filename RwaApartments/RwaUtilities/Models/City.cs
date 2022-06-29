using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RwaUtilities.Models
{
    public class City
    {
        public int IdCity { get; set; }
        public string NameCity { get; set; }

        public override string ToString()
        => $"{NameCity}";
    }
}
