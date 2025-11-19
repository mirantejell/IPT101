CREATE PROCEDURE [dbo].[Get_Library]
	@BookId NVARCHAR(100) = NULL
AS
BEGIN
	SELECT * FROM [dbo].[Books] AS a WHERE a.[BookId] = @BookId;
END