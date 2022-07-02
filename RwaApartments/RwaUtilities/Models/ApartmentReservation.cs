using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RwaUtilities.Models
{
    public class ApartmentReservation
    {
        public int Id { get; set; }

        public string Details { get; set; }

        public int ApartmentId { get; set; }

        public string Username { get; set; }

        public User User { get; set; }

        public string UserAddress { get; set; }

        public string UserPhone { get; set; }

        public string UserEmail { get; set; }
    }
}
