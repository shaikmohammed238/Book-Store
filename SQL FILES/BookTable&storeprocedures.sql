CREATE TABLE Books
(
	BookId int Identity(1,1) PRIMARY KEY ,
	BookName varchar(100) not null,
	AuthorName varchar(70) not null,
    DiscountPriceValue money not null,   
	OriginalPriceValue  money not null,            
    BookDescription nvarchar(max)not null,
    TotalRating INT,
	RatingCount INT,
    BookImage varchar(max)not null,
	BookQuantity int
	)
select * from Books
select * from Usertbl
Create proc sp_AddBooks(
	@BookName varchar(100) ,
	@AuthorName varchar(70) ,
    @DiscountPriceValue money ,   
	@OriginalPriceValue  money ,            
    @BookDescription nvarchar(max),
    @TotalRating INT,
	@RatingCount INT,
    @BookImage varchar(max),
	@BookQuantity int)
	AS
	begin INSERT INTO Books
	VALUES(@BookName,@AuthorName,@DiscountPriceValue,@OriginalPriceValue,@BookDescription,@TotalRating,@RatingCount,@BookImage,@BookQuantity)
	END;
EXEC sp_AddBooks 'Hello','FirstName',55222,85221,'hey guys how are u this is my first test',5,61,'jpghj',45
 CREATE PROC sp_UpdateBooks(
 @BookId INT,
 @BookName varchar(100) ,
	@AuthorName varchar(70) ,
    @DiscountPriceValue money ,   
	@OriginalPriceValue  money ,            
    @BookDescription nvarchar(max),
    @TotalRating INT,
	@RatingCount INT,
    @BookImage varchar(max),
	@BookQuantity int)
	AS
	Begin 
	IF EXISTS(SELECT * FROM Books WHERE BookId=@BookId)
	begin
	Update Books SET
	BookName=@BookName,
	AuthorName=@AuthorName,
	DiscountPriceValue=@DiscountPriceValue,
	OriginalPriceValue=@OriginalPriceValue,
	BookDescription=@BookDescription,
	TotalRating=@TotalRating,
	RatingCount=@RatingCount,
	BookImage=@BookImage,
	BookQuantity=@BookQuantity
	WHERE BookId=@BookId;
	END
	ELSE
	begin
	 select 1;
	 END
	 END
EXEC sp_UpdateBooks 1,'helloupdate','SecondUpdate',55222,85788,'hey guys how are u this mu second update',2,61,'jkpj',54
CREATE PROC sp_DeleteBook(@BookId Int)
AS
BEGIN
IF EXISTS(SELECT * FROM Books WHERE BookId=@BookId)
	begin
	DELETE FROM Books WHERE BookId=@BookId;
	END
ELSE
BEGIN
SELECT 1;
END
END
EXEC sp_DeleteBook 5
CREATE PROC sp_GetBook(@BookId int)
AS
BEGIN
IF EXISTS(SELECT * FROM Books WHERE BookId=@BookId)
	begin
	SELECT * FROM Books WHERE BookId=@BookId;
	END

END
EXEC sp_GetBook 1
CREATE PROC sp_GetAllBooks
AS
BEGIN
SELECT * FROM Books;
END
EXEC sp_GetAllBooks