using Microsoft.ApplicationBlocks.Data;
using RwaUtilities.Models;
using RwaUtilities.Models.Tags;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RwaUtilities.DAL
{
    class DBRepository : IRepository
    {
        private static string Cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public int AddApartment(Apartment apartment, int cityId, int ownerId)
        {
            var id = SqlHelper.ExecuteScalar(Cs, nameof(AddApartment), apartment.CreatedAt, ownerId, cityId, apartment.Name, apartment.NameEng, apartment.Price, apartment.MaxAdults, apartment.MaxChildren, apartment.TotalRooms, apartment.BeachDistance);
            return int.Parse(id.ToString());
        }

        public void AddApartmentImage(ApartmentImage image, int apartmentId)
        {
            SqlHelper.ExecuteNonQuery(Cs, nameof(AddApartmentImage), image.Guid, image.CreatedAt, apartmentId, image.Path, image.Name, image.IsRepresentative);
        }

        public void AddApartmentReview(ApartmentReview review)
        {
            SqlHelper.ExecuteNonQuery(Cs, nameof(AddApartmentReview), review.UserId, review.ApartmentId, review.Stars, review.Details);
        }

        public void AddNewUser(string name, string email, string address, string phone, string password)
        {
            SqlHelper.ExecuteNonQuery(Cs, nameof(AddNewUser), name, email, address, phone, password);
        }

        public void AddReservation(ApartmentReservation reservation)
        {
            SqlHelper.ExecuteNonQuery(Cs, nameof(AddReservation), reservation.ApartmentId, reservation.Details, reservation.Username, reservation.UserEmail, reservation.UserAddress, reservation.UserPhone);
        }

        public void AddReservationById(ApartmentReservation reservation, int userId)
        {
            SqlHelper.ExecuteNonQuery(Cs, nameof(AddReservationById), userId, reservation.ApartmentId, reservation.Details);
        }

        public int AddTag(Tag tag)
        {
            var id = SqlHelper.ExecuteScalar(Cs, nameof(AddTag), tag.Name, tag.NameEng, tag.CreatedAt, tag.TagType.Id);
            return int.Parse(id.ToString());
        }

        public void AddTagForApartment(int tag, int apartmentId)
        {
            SqlHelper.ExecuteNonQuery(Cs, nameof(AddTagForApartment), apartmentId, tag);
        }

        public User AuthenticateUser(string username, string password)
        {
            DataTable userTable = SqlHelper.ExecuteDataset(Cs, nameof(AuthenticateUser), username, password).Tables[0];

            if (userTable.Rows.Count == 0) return null;

            return new User
            {
                Id = (int)userTable.Rows[0][nameof(User.Id)],
                Email = userTable.Rows[0][nameof(User.Email)].ToString(),
                PasswordHash = userTable.Rows[0][nameof(User.PasswordHash)].ToString(),
                UserName = userTable.Rows[0][nameof(User.UserName)].ToString(),
                Address = userTable.Rows[0][nameof(User.Address)].ToString(),
                PhoneNumber = userTable.Rows[0][nameof(User.PhoneNumber)].ToString(),
                CreatedAt = DateTime.Parse(userTable.Rows[0][nameof(User.CreatedAt)].ToString()),
                DeletedAt = DateTime.TryParse(userTable.Rows[0][nameof(User.DeletedAt)].ToString(), out _) ? (DateTime?)DateTime.Parse(userTable.Rows[0][nameof(User.DeletedAt)].ToString()) : null,
                EmailConfirmed = (bool)userTable.Rows[0][nameof(User.EmailConfirmed)],
                PhoneNumberConfirmed = (bool)userTable.Rows[0][nameof(User.PhoneNumberConfirmed)]
            };
        }

        public void DeleteApartment(int id)
        {
            SqlHelper.ExecuteNonQuery(Cs, nameof(DeleteApartment), id);
        }

        public void DeleteApartmentImage(int idImage)
        {
            SqlHelper.ExecuteNonQuery(Cs, nameof(DeleteApartmentImage), idImage);
        }

        public void DeleteTag(int id)
        {
            SqlHelper.ExecuteNonQuery(Cs, nameof(DeleteTag), id);
        }

        public void DeleteTagForApartment(int idTag, int idApartment)
        {
            SqlHelper.ExecuteNonQuery(Cs, nameof(DeleteTagForApartment), idTag, idApartment);
        }

        public IList<Apartment> GetAllApartments()
        {
            IList<Apartment> apartments = new List<Apartment>();

            DataTable apartmentsTable = SqlHelper.ExecuteDataset(Cs, nameof(GetAllApartments)).Tables[0];

            foreach (DataRow row in apartmentsTable.Rows)
            {
                ApartmentOwner owner = new ApartmentOwner
                {
                    OwnerId = (int)row[nameof(ApartmentOwner.OwnerId)],
                    OwnerCreatedAt = DateTime.Parse(row[nameof(ApartmentOwner.OwnerCreatedAt)].ToString()),
                    OwnerName = row[nameof(ApartmentOwner.OwnerName)].ToString(),
                };

                City city = new City
                {
                    IdCity = (int)row[nameof(City.IdCity)],
                    NameCity = row[nameof(City.NameCity)].ToString()
                };

                apartments.Add(new Apartment
                {
                    Id = (int)row[nameof(Apartment.Id)],
                    Name = row[nameof(Apartment.Name)].ToString(),
                    NameEng = row[nameof(Apartment.NameEng)].ToString(),
                    CreatedAt = DateTime.Parse(row[nameof(Apartment.CreatedAt)].ToString()),
                    DeletedAt = null,
                    Price = (decimal)row[nameof(Apartment.Price)],
                    MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                    MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                    TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                    BeachDistance = (int)row[nameof(Apartment.BeachDistance)],
                    Owner = owner,
                    City = city,
                    Status = (ApartmentStatus)Enum.Parse(typeof(ApartmentStatus), row[nameof(Apartment.Status)].ToString())
                });
            }

            return apartments;

        }

        public IList<City> GetAllCities()
        {
            IList<City> cities = new List<City>();

            DataTable citiesTable = SqlHelper.ExecuteDataset(Cs, nameof(GetAllCities)).Tables[0];


            foreach (DataRow row in citiesTable.Rows)
            {
                cities.Add(new City
                {
                    IdCity = (int)row[nameof(City.IdCity)],
                    NameCity = row[nameof(City.NameCity)].ToString(),
                });
            }

            return cities;
        }

        public IList<ApartmentOwner> GetAllOwners()
        {
            IList<ApartmentOwner> owners = new List<ApartmentOwner>();

            DataTable ownersTable = SqlHelper.ExecuteDataset(Cs, nameof(GetAllOwners)).Tables[0];


            foreach (DataRow row in ownersTable.Rows)
            {
                owners.Add(new ApartmentOwner
                {
                    OwnerId = (int)row[nameof(ApartmentOwner.OwnerId)],
                    OwnerCreatedAt = DateTime.Parse(row[nameof(ApartmentOwner.OwnerCreatedAt)].ToString()),
                    OwnerName = row[nameof(ApartmentOwner.OwnerName)].ToString(),
                });
            }

            return owners;
        }

        public IList<Tag> GetAllTags()
        {
            IList<Tag> tags = new List<Tag>();

            DataTable tagsTable = SqlHelper.ExecuteDataset(Cs, nameof(GetAllTags)).Tables[0];


            foreach (DataRow row in tagsTable.Rows)
            {
                TagType type = new TagType
                {
                    Id = (int)row["TagTypeId"],
                    Name = row["TagTypeName"].ToString(),
                    NameEng = row["TagTypeNameEng"].ToString(),
                };

                tags.Add(new Tag
                {
                    Id = (int)row[nameof(Tag.Id)],
                    Name = row[nameof(Tag.Name)].ToString(),
                    NameEng = row[nameof(Tag.NameEng)].ToString(),
                    CreatedAt = DateTime.Parse(row[nameof(Tag.CreatedAt)].ToString()),
                    TagType = type,
                    TagCount = GetTagCount((int)row[nameof(Tag.Id)])
                });
            }

            return tags;
        }

        public IList<TagType> GetAllTagTypes()
        {
            IList<TagType> types = new List<TagType>();

            DataTable typesTable = SqlHelper.ExecuteDataset(Cs, nameof(GetAllTagTypes)).Tables[0];

            foreach (DataRow row in typesTable.Rows)
            {
                types.Add(new TagType
                {
                    Id = (int)row[nameof(TagType.Id)],
                    Name = row[nameof(TagType.Name)].ToString(),
                    NameEng = row[nameof(TagType.NameEng)].ToString()
                });
            }

            return types;
        }

        public IList<User> GetAllUsers()
        {
            IList<User> users = new List<User>();

            DataTable usersTable = SqlHelper.ExecuteDataset(Cs, nameof(GetAllUsers)).Tables[0];

            foreach (DataRow row in usersTable.Rows)
            {
                users.Add(new User
                {
                    Id = (int)row[nameof(User.Id)],
                    Email = row[nameof(User.Email)].ToString(),
                    UserName = row[nameof(User.UserName)].ToString(),
                    Address = row[nameof(User.Address)].ToString(),
                    PhoneNumber = row[nameof(User.PhoneNumber)].ToString(),
                    CreatedAt = DateTime.Parse(row[nameof(User.CreatedAt)].ToString()),
                    DeletedAt = DateTime.TryParse(row[nameof(User.DeletedAt)].ToString(), out _) ? (DateTime?)DateTime.Parse(row[nameof(User.DeletedAt)].ToString()) : null,
                    EmailConfirmed = (bool)row[nameof(User.EmailConfirmed)],
                    PhoneNumberConfirmed = (bool)row[nameof(User.PhoneNumberConfirmed)]
                });
            }

            return users;
        }

        public IList<Apartment> GetAllVacantApartments()
        {
            IList<Apartment> apartments = new List<Apartment>();

            DataTable apartmentsTable = SqlHelper.ExecuteDataset(Cs, nameof(GetAllVacantApartments)).Tables[0];

            foreach (DataRow row in apartmentsTable.Rows)
            {
                ApartmentOwner owner = new ApartmentOwner
                {
                    OwnerId = (int)row[nameof(ApartmentOwner.OwnerId)],
                    OwnerCreatedAt = DateTime.Parse(row[nameof(ApartmentOwner.OwnerCreatedAt)].ToString()),
                    OwnerName = row[nameof(ApartmentOwner.OwnerName)].ToString(),
                };

                City city = new City
                {
                    IdCity = (int)row[nameof(City.IdCity)],
                    NameCity = row[nameof(City.NameCity)].ToString()
                };

                apartments.Add(new Apartment
                {
                    Id = (int)row[nameof(Apartment.Id)],
                    Name = row[nameof(Apartment.Name)].ToString(),
                    NameEng = row[nameof(Apartment.NameEng)].ToString(),
                    CreatedAt = DateTime.Parse(row[nameof(Apartment.CreatedAt)].ToString()),
                    DeletedAt = null,
                    Price = (decimal)row[nameof(Apartment.Price)],
                    MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                    MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                    TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                    BeachDistance = (int)row[nameof(Apartment.BeachDistance)],
                    Owner = owner,
                    City = city,
                    Images = GetApartmentImages((int)row[nameof(Apartment.Id)]),
                    Tags = GetApartmentTags((int)row[nameof(Apartment.Id)]),
                    Status = (ApartmentStatus)Enum.Parse(typeof(ApartmentStatus), row[nameof(Apartment.Status)].ToString())
                });


            }

            return apartments;
        }

        public Apartment GetApartment(int id)
        {
            DataTable apartmentTable = SqlHelper.ExecuteDataset(Cs, nameof(GetApartment), id).Tables[0];

            if (apartmentTable.Rows.Count == 0) return null;

            ApartmentOwner owner = new ApartmentOwner
            {
                OwnerId = (int)apartmentTable.Rows[0][nameof(ApartmentOwner.OwnerId)],
                OwnerCreatedAt = DateTime.Parse(apartmentTable.Rows[0][nameof(ApartmentOwner.OwnerCreatedAt)].ToString()),
                OwnerName = apartmentTable.Rows[0][nameof(ApartmentOwner.OwnerName)].ToString(),
            };

            City city = new City
            {
                IdCity = (int)apartmentTable.Rows[0][nameof(City.IdCity)],
                NameCity = apartmentTable.Rows[0][nameof(City.NameCity)].ToString()
            };

            return new Apartment
            {
                Id = (int)apartmentTable.Rows[0][nameof(Apartment.Id)],
                Name = apartmentTable.Rows[0][nameof(Apartment.Name)].ToString(),
                NameEng = apartmentTable.Rows[0][nameof(Apartment.NameEng)].ToString(),
                CreatedAt = DateTime.Parse(apartmentTable.Rows[0][nameof(Apartment.CreatedAt)].ToString()),
                DeletedAt = null,
                Price = (decimal)apartmentTable.Rows[0][nameof(Apartment.Price)],
                MaxAdults = (int)apartmentTable.Rows[0][nameof(Apartment.MaxAdults)],
                MaxChildren = (int)apartmentTable.Rows[0][nameof(Apartment.MaxChildren)],
                TotalRooms = (int)apartmentTable.Rows[0][nameof(Apartment.TotalRooms)],
                BeachDistance = (int)apartmentTable.Rows[0][nameof(Apartment.BeachDistance)],
                Owner = owner,
                City = city,
                Status = (ApartmentStatus)Enum.Parse(typeof(ApartmentStatus), apartmentTable.Rows[0][nameof(Apartment.Status)].ToString())
            };
        }

        public IList<ApartmentImage> GetApartmentImages(int id)
        {
            IList<ApartmentImage> images = new List<ApartmentImage>();

            DataTable imagesTable = SqlHelper.ExecuteDataset(Cs, nameof(GetApartmentImages), id).Tables[0];

            foreach (DataRow row in imagesTable.Rows)
            {
                images.Add(new ApartmentImage
                {
                    Id = (int)row[nameof(ApartmentImage.Id)],
                    Name = row[nameof(ApartmentImage.Name)].ToString(),
                    CreatedAt = DateTime.Parse(row[nameof(ApartmentImage.CreatedAt)].ToString()),
                    Path = row[nameof(ApartmentImage.Path)].ToString(),
                    IsRepresentative = (bool)row[nameof(ApartmentImage.IsRepresentative)],
                    Guid = (Guid)row[nameof(ApartmentImage.Guid)]
                });
            }

            return images;
        }

        public IList<ApartmentReview> GetApartmentReviews(int id)
        {
            IList<ApartmentReview> reviews = new List<ApartmentReview>();

            DataTable reviewsTable = SqlHelper.ExecuteDataset(Cs, nameof(GetApartmentReviews), id).Tables[0];


            foreach (DataRow row in reviewsTable.Rows)
            {

                reviews.Add(new ApartmentReview
                {
                    Id = (int)row[nameof(ApartmentReview.Id)],
                    CreatedAt = DateTime.Parse(row[nameof(ApartmentReview.CreatedAt)].ToString()),
                    ApartmentId = (int)row[nameof(ApartmentReview.ApartmentId)],
                    Details = row[nameof(ApartmentReview.Details)].ToString(),
                    Stars = (int)row[nameof(ApartmentReview.Stars)],
                    UserId = (int)row[nameof(ApartmentReview.UserId)],
                    UserName = (string)row[nameof(ApartmentReview.UserName)],
                });
            }

            return reviews;
        }

        public IList<Tag> GetApartmentTags(int id)
        {
            IList<Tag> tags = new List<Tag>();

            DataTable tagsTable = SqlHelper.ExecuteDataset(Cs, nameof(GetApartmentTags), id).Tables[0];


            foreach (DataRow row in tagsTable.Rows)
            {

                tags.Add(new Tag
                {
                    Id = (int)row[nameof(Tag.Id)],
                    Name = row[nameof(Tag.Name)].ToString(),
                    NameEng = row[nameof(Tag.NameEng)].ToString(),
                    CreatedAt = DateTime.Parse(row[nameof(Tag.CreatedAt)].ToString()),
                });
            }

            return tags;
        }

        public IList<Models.MVCUser.User> GetMvcUsers()
        {
            IList<Models.MVCUser.User> users = new List<Models.MVCUser.User>();

            DataTable usersTable = SqlHelper.ExecuteDataset(Cs, nameof(GetMvcUsers)).Tables[0];

            foreach (DataRow row in usersTable.Rows)
            {
                Models.MVCUser.User user = new Models.MVCUser.User
                {
                    Id = row[nameof(Models.MVCUser.User.Id)].ToString(),
                    Email = row[nameof(Models.MVCUser.User.Email)].ToString(),
                    UserName = row[nameof(Models.MVCUser.User.Email)].ToString(),
                    Password = row[nameof(Models.MVCUser.User.Password)].ToString(),
                    FullName = row[nameof(Models.MVCUser.User.UserName)].ToString(),
                    PhoneNumber = row[nameof(Models.MVCUser.User.PhoneNumber)].ToString(),
                    CreatedTime = DateTime.Parse(row[nameof(Models.MVCUser.User.CreatedTime)].ToString())
                };

                user.Roles = GetUserRoles(int.Parse(user.Id));

                users.Add(user);
            }

            return users;
        }

        public string GetReservationUsername(int id)
        {
            DataTable countTable = SqlHelper.ExecuteDataset(Cs, nameof(GetReservationUsername), id).Tables[0];
            return countTable.Rows[0]["Username"].ToString();
        }

        public int GetTagCount(int id)
        {
            DataTable countTable = SqlHelper.ExecuteDataset(Cs, nameof(GetTagCount), id).Tables[0];
            return (int)countTable.Rows[0][nameof(Tag.TagCount)];

        }

        public User GetUser(int id)
        {
            DataTable userTable = SqlHelper.ExecuteDataset(Cs, nameof(GetUser), id).Tables[0];

            if (userTable.Rows.Count == 0) return null;

            return new User
            {
                Id = (int)userTable.Rows[0][nameof(User.Id)],
                Email = userTable.Rows[0][nameof(User.Email)].ToString(),
                UserName = userTable.Rows[0][nameof(User.UserName)].ToString(),
                Address = userTable.Rows[0][nameof(User.Address)].ToString(),
                PhoneNumber = userTable.Rows[0][nameof(User.PhoneNumber)].ToString(),
                CreatedAt = DateTime.Parse(userTable.Rows[0][nameof(User.CreatedAt)].ToString()),
                DeletedAt = DateTime.TryParse(userTable.Rows[0][nameof(User.DeletedAt)].ToString(), out _) ? (DateTime?)DateTime.Parse(userTable.Rows[0][nameof(User.DeletedAt)].ToString()) : null,
                EmailConfirmed = (bool)userTable.Rows[0][nameof(User.EmailConfirmed)],
                PhoneNumberConfirmed = (bool)userTable.Rows[0][nameof(User.PhoneNumberConfirmed)]
            };
        }

        public IList<string> GetUserRoles(int id)
        {
            IList<string> roles = new List<string>();

            DataTable rolesTable = SqlHelper.ExecuteDataset(Cs, nameof(GetUserRoles), id).Tables[0];
            foreach (DataRow row in rolesTable.Rows)
            {
                roles.Add(row["Name"].ToString());
            }

            return roles;
        }

        public void UpdateApartment(Apartment apartment, int cityId, int ownerId)
        {
            SqlHelper.ExecuteNonQuery(Cs, nameof(UpdateApartment), apartment.Id, ownerId, cityId, apartment.Name, apartment.NameEng, apartment.Price, apartment.MaxAdults, apartment.MaxChildren, apartment.TotalRooms, apartment.BeachDistance);
        }

        public void UpdateApartmentImage(int idPicture, string name, bool isRepresentative)
        {
            SqlHelper.ExecuteNonQuery(Cs, nameof(UpdateApartmentImage), idPicture, name, isRepresentative);
        }
    }
}
