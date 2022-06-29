using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RwaUtilities.Models
{
    public class ApartmentOwner
    {
        public int OwnerId { get; set; }

        public DateTime OwnerCreatedAt { get; set; }

        public string OwnerName { get; set; }
    }
}
