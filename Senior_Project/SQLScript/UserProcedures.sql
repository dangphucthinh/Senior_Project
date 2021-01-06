USE UserDatabase;
CREATE PROC GetRoles
@username varchar(256)
AS 
BEGIN
	SELECT dbo.[Role].Name FROM dbo.UserRole
	INNER JOIN dbo.[User] ON [User].Id = UserRole.UserId
	INNER JOIN dbo.[Role] ON [Role].Id = UserRole.RoleId
	WHERE dbo.[User].UserName = @username;
END

SELECT dbo.[Role].Name FROM dbo.UserRole
INNER JOIN dbo.[User] ON [User].Id = UserRole.UserId
INNER JOIN dbo.[Role] ON [Role].Id = UserRole.RoleId
WHERE dbo.[User].UserName = N'admin';


EXEC GetRoles @username = N'admin';