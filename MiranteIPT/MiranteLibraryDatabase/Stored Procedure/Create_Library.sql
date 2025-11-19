CREATE PROCEDURE [dbo].[Create_Library]
@BookID NVARCHAR (100)= NULL,
@Title NVARCHAR (100) = NULL,
@Author INT  = NULL,
@Category INT = NULL,
@Availability INT = NULL
AS
 BEGIN 
    INSERT INTO Books (BookId, Title, Author, Category)
      Values (@BookId, @Title, @Author, @Category);
End