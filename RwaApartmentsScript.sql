USE RwaApartmani
GO

INSERT INTO AspNetRoles(Name) VALUES('Admin')
GO

--password Admin
INSERT INTO AspNetUsers(Guid, CreatedAt, Email, EmailConfirmed, PasswordHash, PhoneNumberConfirmed, LockoutEnabled, AccessFailedCount, UserName, Address)
VALUES(NEWID(), GETDATE(), 'admin@gmail.com', 1, '887375DAEC62A9F02D32A63C9E14C7641A9A8A42E4FA8F6590EB928D9744B57BB5057A1D227E4D40EF911AC030590BBCE2BFDB78103FF0B79094CEE8425601F5', 0, 0, 0, 'Admin', 'Somewhere 12')
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

--Soft delete apartment
CREATE PROCEDURE DeleteApartment
	@Id int
AS
UPDATE Apartment
SET DeletedAt = GETDATE()
GO

--Get all tag types
CREATE PROCEDURE GetAllTagTypes
AS
SELECT Id, Name, NameEng
FROM TagType
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


--Disabling some of the pictures
UPDATE ApartmentPicture
SET DeletedAt = GETDATE()
WHERE Id IN (6, 7, 8, 9, 10, 12, 13, 14, 19, 20, 21, 22, 24, 25, 27, 28, 30, 31, 32, 35, 36)