using Microsoft.AspNet.Identity.Owin;
using PublicPart.Models.Auth;
using PublicPart.Models.ViewModels;
using Recaptcha.Web.Mvc;
using RwaUtilities.DAL;
using RwaUtilities.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PublicPart.Controllers
{
    [Authorize]
    public class ApartmentsController : Controller
    {
        private UserManager _authManager;

        public UserManager AuthManager
        {
            get
            {
                return _authManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager>();
            }
            set
            {
                _authManager = value;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            FilteredApartmentsViewModel viewModel = new FilteredApartmentsViewModel();
            IList<City> cities = RepositoryFactory.GetRepository().GetAllCities();
            HttpCookie cookie = Request.Cookies["filters"];
            if (cookie != null)
            {
                viewModel.Adults = int.Parse(cookie.Values["adults"]);
                viewModel.Children = int.Parse(cookie.Values["children"]);
                viewModel.Rooms = int.Parse(cookie.Values["rooms"]);
                viewModel.SelectedCity = int.Parse(cookie.Values["cityId"]);
            }

            viewModel.Cities = cities;

            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetFilteredApartments(int rooms, int adults, int children, string city)
        {
            IList<Apartment> apartments = RepositoryFactory.GetRepository().GetAllVacantApartments();

            HttpCookie cookie = new HttpCookie("filters");
            cookie["adults"] = adults.ToString();
            cookie["children"] = children.ToString();
            cookie["rooms"] = rooms.ToString();

            cookie.Expires = DateTime.Now.AddDays(10);

            if (city != "-")
            {

                cookie["cityId"] = RepositoryFactory.GetRepository().GetAllCities().First(c => c.NameCity == city).IdCity.ToString();
                apartments = apartments.Where(a => a.City.NameCity == city).ToList();
            }
            else
            {
                cookie["cityId"] = 0.ToString();
            }

            apartments = apartments.Where(a => a.TotalRooms >= rooms)
                .Where(a => a.MaxAdults >= adults)
                .Where(a => a.MaxChildren >= children)
                .ToList();
            Response.Cookies.Add(cookie);

            return PartialView("_ApartmentsList", apartments);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            RwaUtilities.Models.MVCUser.User user = await AuthManager.FindByNameAsync(User.Identity.Name);

            Apartment apartment = RepositoryFactory.GetRepository().GetApartment(id);
            apartment.Images = RepositoryFactory.GetRepository().GetApartmentImages(id);
            apartment.Tags = RepositoryFactory.GetRepository().GetApartmentTags(id);

            DetailsViewModel detailsViewModel = new DetailsViewModel
            {
                Apartment = apartment,
                Reviews = RepositoryFactory.GetRepository().GetApartmentReviews(id),
            };

            if (user != null)
            {
                detailsViewModel.IdUser = user.Id;
                detailsViewModel.Name = user.FullName;
                detailsViewModel.PhoneNumber = user.PhoneNumber;
                detailsViewModel.Email = user.Email;
            }
            return View(detailsViewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetReviews(int id)
        {
            IList<ApartmentReview> model = RepositoryFactory.GetRepository().GetApartmentReviews(id);
            return PartialView("_ReviewList", model);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Picture(string path)
        {
            if (path == null || string.IsNullOrEmpty(path))
                return Content(content: "File missing");
            var javnoRoot = Server.MapPath("~");
            var adminRoot = Path.Combine(javnoRoot, "../AdministrationPart");
            var picturePath = Path.Combine(adminRoot, path);
            string mimeType = MimeMapping.GetMimeMapping(path);
            return new FilePathResult(picturePath, mimeType);
        }

        [HttpPost]
        public ActionResult AddReview(int apartmentId, string details, int stars, string idUser)
        {
            if (idUser != "0")
            {
                RepositoryFactory.GetRepository().AddApartmentReview(new ApartmentReview { ApartmentId = apartmentId, UserId = int.Parse(idUser), Stars = stars, Details = details });
            }
            return new EmptyResult();
        }

    }
}