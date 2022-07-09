using Microsoft.AspNet.Identity.Owin;
using PublicPart.Models.Auth;
using PublicPart.Models.ViewModels;
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
            IList<Apartment> apartments = RepositoryFactory.GetRepository().GetAllVacantApartments();
            return View(apartments);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetFilteredApartments()
        {
            IList<Apartment> apartments = RepositoryFactory.GetRepository().GetAllVacantApartments();
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