using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RwaUtilities.Models
{
    public class ApartmentImage
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; } = null;
        public string Path { get; set; }
        public string Name { get; set; }
        public bool IsRepresentative { get; set; }

    }
}
