create table Wishlist
(
	WishlistId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	ID INT NOT NULL
	FOREIGN KEY (ID) REFERENCES Usertbl(ID),
	BookId INT NOT NULL
	FOREIGN KEY (BookId) REFERENCES Books(BookId)	
);
SELECT * FROM Wishlist;
select *from Usertbl
Create PROC sp_CreateWishlist(
	@UserId INT,
	@BookId INT
	)
AS
BEGIN 
	IF EXISTS(SELECT * FROM Wishlist WHERE BookId = @BookId AND ID = @UserId)
		SELECT 1;
	ELSE
	BEGIN
		IF EXISTS(SELECT * FROM Books WHERE BookId = @BookId)
		BEGIN
			INSERT INTO Wishlist(ID,BookId)
			VALUES ( @UserId,@BookId)
		END
		ELSE
			SELECT 2;
	END
END;
CREATE PROCEDURE sp_DeleteWishlist
	@WishlistId INT
AS
BEGIN
		DELETE FROM Wishlist WHERE WishlistId = @WishlistId
END;
create PROCEDURE sp_ShowWishlistByUserId(
  @UserId int)
AS
BEGIN
	   select 
		Books.BookId,
		Books.BookName,
		Books.AuthorName,
		Books.DiscountPriceValue,
		Books.OriginalPriceValue,
		Books.BookDescription,
		Books.BookImage,
		Wishlist.WishlistId,
		Wishlist.ID,
		Wishlist.BookId
		FROM Books
		inner join Wishlist
		on Wishlist.BookId=Books.BookId where Wishlist.ID=@UserId
End



