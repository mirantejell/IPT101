 CREATE PROCEDURE [dbo].[Update_Library]
    @BookId INT = NULL,
	@Title NVARCHAR(100) = NULL,
	@Author NVARCHAR(100) = NULL,
	@Category NVARCHAR(50) = NULL,
	@Availability NVARCHAR(10) = NULL
	
AS
	BEGIN
	UPDATE [dbo].[Books]
	SET 
	[Title] = @Title,
	[Author] = @Author,
	[Category] = @Category,
	[Availability] = @Availability

	WHERE [BookId] = @BookId;
	END

