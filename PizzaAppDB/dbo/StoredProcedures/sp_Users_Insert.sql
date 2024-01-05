CREATE PROCEDURE [dbo].[sp_Users_Insert]
	@firstName nvarchar(50),
	@lastName nvarchar(50)
AS

BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE FirstName = @firstName AND LastName = @lastName)
	BEGIN
		insert into dbo.Users(FirstName, LastName)
		values (@firstName, @lastName);
	END

	SELECT TOP 1 [Id], [FirstName], [LastName]
	FROM dbo.Users
	WHERE FirstName = @firstName and LastName = @lastName
END
