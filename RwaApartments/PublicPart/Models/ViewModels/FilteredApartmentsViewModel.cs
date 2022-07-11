using RwaUtilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicPart.Models.ViewModels
{
    public class FilteredApartmentsViewModel
    {
        public IList<Apartment> Apartments { get; set; }

        public int Rooms { get; set; }

        public int Adults { get; set; }

        public int Children { get; set; }

        public IList<City> Cities { get; set; }
    }
}