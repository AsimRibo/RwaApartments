using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RwaUtilities.Models.Tags
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public DateTime CreatedAt { get; set; }
        public TagType TagType { get; set; }
        public int TagCount { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Tag tag &&
                   Id == tag.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }

        public override string ToString()
        => $"{NameEng}";
    }
}
