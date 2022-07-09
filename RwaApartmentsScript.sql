USE RwaApartmani
GO

INSERT INTO AspNetRoles(Name) VALUES('Admin')
GO

--password Admin
INSERT INTO AspNetUsers(Guid, CreatedAt, Email, EmailConfirmed, PasswordHash, PhoneNumberConfirmed, LockoutEnabled, AccessFailedCount, UserName, Address)
VALUES(NEWID(), GETDATE(), 'admin@gmail.com', 1, '887375DAEC62A9F02D32A63C9E14C7641A9A8A42E4FA8F6590EB928D9744B57BB5057A1D227E4D40EF911AC030590BBCE2BFDB78103FF0B79094CEE8425601F5', 0, 0, 0, 'Admin', 'Somewhere 12')
GO

--setting password for all users to be Admin 
UPDATE AspNetUsers
SET PasswordHash = '887375DAEC62A9F02D32A63C9E14C7641A9A8A42E4FA8F6590EB928D9744B57BB5057A1D227E4D40EF911AC030590BBCE2BFDB78103FF0B79094CEE8425601F5'
WHERE Id != 249
GO

--setting basic role
INSERT INTO AspNetRoles(Name) VALUES('Basic')
GO

INSERT INTO AspNetUserRoles(UserId, RoleId)
SELECT u.Id, 2
FROM AspNetUsers AS u
WHERE u.Id = 249
GO

--connecting admin role to admin user
INSERT INTO AspNetUserRoles(RoleId, UserId) VALUES(1, 249)
GO

--Authentication procedure
CREATE PROCEDURE AuthenticateUser
	@username nvarchar(256),
	@password nvarchar(max)
AS
SELECT s.Id, s.Email, s.UserName, s.PasswordHash, s.CreatedAt, s.DeletedAt,  s.EmailConfirmed, s.PhoneNumber, s.PhoneNumberConfirmed, s.Address
FROM AspNetUsers AS s
WHERE s.UserName = @username AND s.PasswordHash = PasswordHash AND s.DeletedAt IS NULL
GO

--Get user roles procedure
CREATE PROCEDURE GetUserRoles
	@IdUser int
AS
SELECT r.Name
FROM AspNetUserRoles AS s
INNER JOIN AspNetRoles AS r
	ON r.Id = s.RoleId
WHERE s.UserId = @IdUser
GO

--Get all users
CREATE PROCEDURE GetAllUsers
AS
SELECT s.Id, s.Email, s.UserName, s.CreatedAt, s.DeletedAt,  s.EmailConfirmed, s.PhoneNumber, s.PhoneNumberConfirmed, s.Address
FROM AspNetUsers AS s
LEFT JOIN AspNetUserRoles AS r
	ON r.UserId = s.Id
WHERE r.RoleId != 1 OR r.RoleId IS NULL
GO

CREATE PROCEDURE GetMvcUsers
AS
SELECT s.Id, s.Email, s.UserName, s.PhoneNumber, s.CreatedAt AS CreatedTime, s.PasswordHash AS Password
FROM AspNetUsers AS s
WHERE s.id != 249
GO

--Get user by ID
CREATE PROCEDURE GetUser
	@IdUser int
AS
SELECT u.Id, u.Email, u.UserName, u.EmailConfirmed, u.CreatedAt, u.DeletedAt, u.PhoneNumber, u.PhoneNumberConfirmed, u.Address
FROM AspNetUsers AS u
WHERE u.Id = @IdUser
GO

--Get all tags and tag types
CREATE PROCEDURE GetAllTags
AS
SELECT t.Id, t.CreatedAt, t.Name, t.NameEng, tt.Id as TagTypeId, tt.Name as TagTypeName, tt.NameEng as TagTypeNameEng
FROM Tag AS t
INNER JOIN TagType AS tt
	ON t.TypeId = tt.Id
GO

--Get number of apartments using certain tag
CREATE PROCEDURE GetTagCount
	@Id int
AS
SELECT COUNT(*) AS TagCount
FROM Tag AS t
INNER JOIN TaggedApartment AS ta
	ON t.Id = ta.TagId
WHERE t.Id = @Id
GO

--Hard delete tag
CREATE PROCEDURE DeleteTag
	@Id int
AS
IF NOT EXISTS(
	SELECT *
	FROM Tag AS t
	INNER JOIN TaggedApartment AS ta
		ON t.Id = ta.TagId
	WHERE t.Id = @Id) 
	BEGIN
		DELETE FROM Tag WHERE Tag.Id = @Id
	END
GO

--Get all apartments
CREATE PROCEDURE GetAllApartments
AS
SELECT a.Id, a.Name, a.NameEng, a.CreatedAt, a.DeletedAt, a.Price, a.MaxAdults, a.MaxChildren, a.TotalRooms, a.BeachDistance, ao.Id AS OwnerId, ao.CreatedAt AS OwnerCreatedAt, ao.Name AS OwnerName
	, c.Name AS City, d.NameEng AS Status
FROM Apartment AS a
INNER JOIN ApartmentOwner AS ao
	ON a.OwnerId = ao.Id
INNER JOIN City AS c
	ON c.Id = a.CityId
INNER JOIN ApartmentStatus AS d
	ON d.Id = a.StatusId
WHERE a.DeletedAt IS NULL
GO

CREATE PROCEDURE GetAllVacantApartments
AS
SELECT a.Id, a.Name, a.NameEng, a.CreatedAt, a.DeletedAt, a.Price, a.MaxAdults, a.MaxChildren, a.TotalRooms, a.BeachDistance, ao.Id AS OwnerId, ao.CreatedAt AS OwnerCreatedAt, ao.Name AS OwnerName
	, c.Name AS NameCity, c.Id AS IdCity, d.NameEng AS Status
FROM Apartment AS a
INNER JOIN ApartmentOwner AS ao
	ON a.OwnerId = ao.Id
INNER JOIN City AS c
	ON c.Id = a.CityId
INNER JOIN ApartmentStatus AS d
	ON d.Id = a.StatusId
WHERE a.DeletedAt IS NULL AND d.Id = 3
GO

--Soft delete apartment
CREATE PROCEDURE DeleteApartment
	@Id int
AS
UPDATE Apartment
SET DeletedAt = GETDATE()
WHERE Id = @Id
GO

--Get all tag types
CREATE PROCEDURE GetAllTagTypes
AS
SELECT Id, Name, NameEng
FROM TagType
GO

CREATE PROCEDURE AddNewUser
	@Username nvarchar(256),
	@Email nvarchar(256),
	@Address nvarchar(1000),
	@Phone nvarchar(max),
	@Password nvarchar(max)
AS
INSERT INTO AspNetUsers(Guid, CreatedAt, Email, EmailConfirmed, PasswordHash, PhoneNumber, PhoneNumberConfirmed, LockoutEnabled, AccessFailedCount, UserName, Address)
VALUES(NEWID(), GETDATE(), @Email, 1, @Password, @Phone, 1, 0, 0, @Username, @Address)
GO

--Add new tag
CREATE PROCEDURE AddTag
	@Name nvarchar(250),
	@NameEng nvarchar(250),
	@CreatedAt datetime,
	@TagTypeID int
AS
INSERT INTO Tag(Guid, CreatedAt, TypeId, Name, NameEng)
VALUES(NEWID(), @CreatedAt, @TagTypeID, @Name, @NameEng)
SELECT SCOPE_IDENTITY()
GO

--Get apartment by ID
CREATE PROCEDURE GetApartment
	@Id int
AS
SELECT a.Id, a.Name, a.NameEng, a.CreatedAt, a.Price, a.MaxAdults, a.MaxChildren, a.TotalRooms, a.BeachDistance, ao.Id AS OwnerId, ao.CreatedAt AS OwnerCreatedAt, ao.Name AS OwnerName
	, c.Name AS City, d.NameEng AS Status
FROM Apartment AS a
INNER JOIN ApartmentOwner AS ao
	ON a.OwnerId = ao.Id
INNER JOIN City AS c
	ON c.Id = a.CityId
INNER JOIN ApartmentStatus AS d
	ON d.Id = a.StatusId
WHERE a.DeletedAt IS NULL AND a.Id = @Id
GO

CREATE PROCEDURE GetApartmentReviews
	@Id int
AS
SELECT ar.Id, ar.CreatedAt, ar.ApartmentId, ar.UserId, u.UserName, ar.Details, ar.Stars
FROM ApartmentReview AS ar
INNER JOIN AspNetUsers AS u
	ON u.Id = ar.UserId
WHERE ar.ApartmentId = @Id
GO

CREATE PROCEDURE AddApartmentReview
	@UserId int,
	@ApartmentId int,
	@Stars int,
	@Details nvarchar(1000)
AS
INSERT INTO ApartmentReview(Guid, CreatedAt, ApartmentId, UserId, Details, Stars)
VALUES(NEWID(), GETDATE(), @ApartmentId, @UserId, @Details, @Stars)
GO

--Get name of person who made reservation
--temporary solution for showcase
CREATE PROCEDURE GetReservationUsername
	@Id int
AS
IF NOT EXISTS(SELECT ApartmentReservation.UserId FROM ApartmentReservation)
	BEGIN
		SELECT TOP 1 ApartmentReservation.UserName AS Username FROM ApartmentReservation
	END
ELSE
	BEGIN
		SELECT TOP 1 u.UserName AS Username
		FROM ApartmentReservation AS ar
		INNER JOIN AspNetUsers as u
			ON u.Id = ar.Id
		WHERE ar.Id = @Id
	END
GO

--Get apartment tags
CREATE PROCEDURE GetApartmentTags
	@Id int
AS
SELECT t.Id, t.Name, t.NameEng, t.CreatedAt
FROM TaggedApartment AS ta
INNER JOIN Tag AS t
	ON t.Id = ta.TagId
WHERE ta.ApartmentId = @Id
GO

--Get apartment pictures by apartment id
CREATE PROCEDURE GetApartmentImages
	@Id int
AS
SELECT ap.Id, ap.Guid, ap.CreatedAt, ap.Path, ap.Name, ap.IsRepresentative
FROM ApartmentPicture AS ap
WHERE ap.ApartmentId = @Id AND ap.DeletedAt IS NULL
GO

--Get all cities
CREATE PROCEDURE GetAllCities
AS
SELECT Id AS IdCity, Name AS NameCity
FROM City
GO

--Get all apartment owners
CREATE PROCEDURE GetAllOwners
AS
SELECT Id AS OwnerId, CreatedAt AS OwnerCreatedAt, Name AS OwnerName
FROM ApartmentOwner
GO

--Insert new apartment
CREATE PROCEDURE AddApartment
	@CreatedAt datetime,
	@OwnerId int,
	@CityId int,
	@Name nvarchar(250),
	@NameEng nvarchar(250),
	@Price money,
	@MaxAdults int,
	@MaxChildren int,
	@TotalRooms int,
	@BeachDistance int
AS
INSERT INTO Apartment(Guid, CreatedAt, OwnerId, TypeId, StatusId, CityId, Name, NameEng, Price, MaxAdults, MaxChildren, TotalRooms, BeachDistance)
VALUES(NEWID(), @CreatedAt, @OwnerId, 999, 3, @CityId, @Name, @NameEng, @Price, @MaxAdults, @MaxChildren, @TotalRooms, @BeachDistance)
SELECT SCOPE_IDENTITY()
GO

--Insert tag for an apartment
CREATE PROCEDURE AddTagForApartment
	@ApartmentId int,
	@TagId int
AS
INSERT INTO TaggedApartment(Guid, ApartmentId, TagId)
VALUES(NEWID(), @ApartmentId, @TagId)
GO

--Insert apartment picture
CREATE PROCEDURE AddApartmentImage
	@Guid uniqueidentifier,
	@CreatedAt datetime,
	@ApartmentId int,
	@Path nvarchar(250),
	@Name nvarchar(250),
	@IsRepresentative bit
AS
INSERT INTO ApartmentPicture(Guid, CreatedAt, ApartmentId, Path, Name, IsRepresentative)
VALUES(@Guid, @CreatedAt, @ApartmentId, @Path, @Name, @IsRepresentative)
GO

--Soft delete picture
CREATE PROCEDURE DeleteApartmentImage
	@Id int
AS
UPDATE ApartmentPicture
SET DeletedAt = GETDATE()
	, IsRepresentative = 0
WHERE Id = @Id
GO

--Update apartment picture
CREATE PROCEDURE UpdateApartmentImage
	@IdPicture int,
	@Name nvarchar(250),
	@IsRepresentative bit
AS
UPDATE ApartmentPicture
SET Name = @Name
	, IsRepresentative = @IsRepresentative
WHERE ApartmentPicture.Id = @IdPicture
GO

--Delete tag from TaggedApartment table
CREATE PROCEDURE DeleteTagForApartment
	@IdTag int,
	@IdApartment int
AS
DELETE FROM TaggedApartment
WHERE TaggedApartment.TagId = @IdTag AND TaggedApartment.ApartmentId = @IdApartment
GO

--Add reservation by userId
CREATE PROCEDURE AddReservationById
	@IdUser int,
	@IdApartment int,
	@Details nvarchar(1000)
AS
INSERT INTO ApartmentReservation(Guid, CreatedAt, ApartmentId, Details, UserId)
VALUES(NEWID(), GETDATE(), @IdApartment, @Details, @IdUser)
GO

CREATE PROCEDURE AddReservation
	@IdApartment int,
	@Details nvarchar(1000),
	@UserName nvarchar(250),
	@UserEmail nvarchar(250),
	@UserAddress nvarchar(1000),
	@UserPhone nchar(20)
AS
INSERT INTO ApartmentReservation(Guid, CreatedAt, ApartmentId, Details, UserName, UserEmail, UserAddress, UserPhone)
VALUES(NEWID(), GETDATE(), @IdApartment, @Details, @UserName, @UserEmail, @UserAddress, @UserPhone)
GO

--Update apartment info
CREATE PROCEDURE UpdateApartment
	@IdApartment int,
	@OwnerId int,
	@CityId int,
	@Name nvarchar(250),
	@NameEng nvarchar(250),
	@Price money,
	@MaxAdults int,
	@MaxChildren int,
	@TotalRooms int,
	@BeachDistance int
AS
UPDATE Apartment
SET OwnerId = @OwnerId
	, CityId = @CityId
	, Name = @Name
	, NameEng = @NameEng
	, Price = @Price
	, MaxAdults = @MaxAdults
	, MaxChildren = @MaxChildren
	, TotalRooms = @TotalRooms
	, BeachDistance = @BeachDistance
WHERE Apartment.Id = @IdApartment
GO

--Disabling some of the pictures
UPDATE ApartmentPicture
SET DeletedAt = GETDATE()
WHERE Id IN (6, 7, 8, 9, 10, 12, 13, 14, 19, 20, 21, 22, 24, 25, 27, 28, 30, 31, 32, 35, 36)

--Setting picture paths
UPDATE ApartmentPicture
SET Path = 'Images\D33CDE78-B19B-478D-99B8-037936B4A64C.jpg'
WHERE Id = 2
GO

UPDATE ApartmentPicture
SET Path = 'Images\3F46F64C-605E-4B4C-9292-683AE3984091.jpg'
WHERE Id = 4
GO

UPDATE ApartmentPicture
SET Path = 'Images\31294E1F-B813-4DCB-92F4-5180D22B55F0.jpg'
WHERE Id = 5
GO

UPDATE ApartmentPicture
SET Path = 'Images\EEB17E09-39F2-4854-B289-9E06E51B503C.jpg'
WHERE Id = 11
GO

UPDATE ApartmentPicture
SET Path = 'Images\1F99FA36-8865-49D1-8EDC-9A8C4A7EBE68.jpg'
WHERE Id = 15
GO

UPDATE ApartmentPicture
SET Path = 'Images\90D05427-3F84-43C1-9736-A2F9CC4F246F.jpg'
WHERE Id = 16
GO

UPDATE ApartmentPicture
SET Path = 'Images\6AAE0268-BBFF-41A5-BDF6-E0B042A071E3.jpg'
WHERE Id = 17
GO

UPDATE ApartmentPicture
SET Path = 'Images\3CC42A12-800C-45CB-BB3D-60A5D86E6305.jpg'
WHERE Id = 18
GO

UPDATE ApartmentPicture
SET Path = 'Images\42258C7E-DE09-45E4-9751-DBDD7F75E435.jpg'
WHERE Id = 23
GO

UPDATE ApartmentPicture
SET Path = 'Images\2D50BC81-CEB1-4255-84FD-7AAFD6349CE9.jpg'
WHERE Id = 26
GO

UPDATE ApartmentPicture
SET Path = 'Images\46532854-8330-43BF-B32E-4852B6249711.jpg'
WHERE Id = 29
GO

UPDATE ApartmentPicture
SET Path = 'Images\4FAD7681-05DB-4ECD-96DF-055A24E0FC45.jpg'
WHERE Id = 33
GO

UPDATE ApartmentPicture
SET Path = 'Images\EF31F15C-5A8A-4EBF-8B7F-156365852016.jpg'
WHERE Id = 34
GO