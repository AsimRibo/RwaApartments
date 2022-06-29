using RwaUtilities.Models.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RwaUtilities.Models
{
    public class Apartment
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NameEng { get; set; }

        public string City { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public ApartmentOwner Owner { get; set; }

        public ApartmentStatus Status { get; set; }

        public IList<Tag> Tags { get; set; }

        public IList<string> PictureNames { get; set; }

        public IList<ApartmentImage> Images { get; set; }

        public decimal Price { get; set; }

        public int MaxAdults { get; set; }

        public int MaxChildren { get; set; }

        public int TotalRooms { get; set; }

        public int BeachDistance { get; set; }
    }

    public enum ApartmentStatus
    {
        Occupied,
        Reserved,
        Vacant
    }
}
