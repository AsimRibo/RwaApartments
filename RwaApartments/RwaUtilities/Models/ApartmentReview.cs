using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RwaUtilities.Models
{
    public class ApartmentReview
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public int ApartmentId { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Details { get; set; }

        public int Stars { get; set; }
    }
}
