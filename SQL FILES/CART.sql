CREATE TABLE Cart
(
	CartID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	ID INT NOT NULL
	FOREIGN KEY (ID) REFERENCES Usertbl(ID),
	BookId INT NOT NULL
	FOREIGN KEY (BookId) REFERENCES Books(BookId),	
	OrderQuantity INT default 1
);
select * from cart;
Create PROCEDURE sp_AddingCart(
	@UserId INT,
	@BookId INT,
	@OrderQuantity INT)
AS
BEGIN
	IF (EXISTS(SELECT * FROM Books WHERE BookId=@BookId))		
	begin
		INSERT INTO Cart(ID,BookId,OrderQuantity)
		VALUES (@UserId,@BookId,@OrderQuantity)
	end
	else
	begin 
		select 2
	end
END;
CREATE PROCEDURE sp_DeleteCartDetails
	@CartID INT
AS
BEGIN
	IF EXISTS(SELECT * FROM Cart WHERE CartID = @CartID)
	BEGIN
		DELETE FROM Cart WHERE CartID = @CartID
	END
	ELSE
	BEGIN
		select 1
	END
END
CREATE PROCEDURE sp_GetCartDetails
	
AS
BEGIN
	SELECT
		Cart.CartID,
		Cart.ID,
		Cart.BookId,
		Cart.OrderQuantity,	
		Books.BookName,
		Books.AuthorName,
		Books.DiscountPriceValue,
		Books.OriginalPriceValue, 
		Books.BookDescription ,
		Books.TotalRating,
		Books.RatingCount,
		Books.BookImage,
		Books.BookQuantity
	FROM Cart
	Inner JOIN Books ON Cart.BookId = Books.BookId
	
END
CREATE PROC sp_UpdateQuantityCart
	@CartID INT,
	@OrderQuantity INT
AS
BEGIN
	IF Exists(SELECT * FROM Cart WHERE CartID = @CartID)
	BEGIN
			UPDATE Cart
			SET
				OrderQuantity = @OrderQuantity
			WHERE
				CartID = @CartID;
		END
		ELSE
		BEGIN
			Select 1;
		END
END
select *from  Usertbl
select * from  Books
select * from Cart
select * from Addresstbl
