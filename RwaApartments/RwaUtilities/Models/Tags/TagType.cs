using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RwaUtilities.Models.Tags
{
    public class TagType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }

        public override string ToString() => $"{NameEng}";
    }
}
