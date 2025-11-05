CREATE PROCEDURE [dbo].[Delete_Library]
	@BookId NVARCHAR(100) = NULL
AS
BEGIN
	DELETE FROM [dbo].[Books] WHERE BookId = @BookId
end
