using RwaUtilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace PublicPart.Models.ViewModels
{
    public class FilteredApartmentsViewModel
    {

        public int Rooms { get; set; }

        public int Adults { get; set; }

        public int Children { get; set; }

        public IList<City> Cities { get; set; }
    }
}