using RwaUtilities.Models;
using RwaUtilities.Models.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RwaUtilities.DAL
{
    public interface IRepository
    {
        User AuthenticateUser(string username, string password);

        IList<string> GetUserRoles(int id);

        IList<User> GetAllUsers();

        User GetUser(int id);

        IList<Tag> GetAllTags();

        int GetTagCount(int id);

        void DeleteTag(int id);

        IList<Apartment> GetAllApartments();

        void DeleteApartment(int id);

        IList<TagType> GetAllTagTypes();

        int AddTag(Tag tag);

        Apartment GetApartment(int id);

        string GetReservationUsername(int id);

        IList<Tag> GetApartmentTags(int id);

        IList<ApartmentImage> GetApartmentImages(int id);

        IList<City> GetAllCities();

        IList<ApartmentOwner> GetAllOwners();

        int AddApartment(Apartment apartment, int cityId, int ownerId);

        void AddTagForApartment(int tag, int apartmentId);

        void AddApartmentImage(ApartmentImage image, int apartmentId);

        void DeleteTagForApartment(int idTag, int idApartment);

        void DeleteApartmentImage(int idImage);

        void UpdateApartmentImage(int idPicture, string name, bool isRepresentative);

        void UpdateApartment(Apartment apartment, int cityId, int ownerId);

        void AddReservationById(ApartmentReservation reservation, int userId);

        void AddReservation(ApartmentReservation reservation);

        void AddApartmentReview(ApartmentReview review);

        IList<Models.MVCUser.User> GetMvcUsers();

        IList<Apartment> GetAllVacantApartments();

        IList<ApartmentReview> GetApartmentReviews(int id);
    }
}
