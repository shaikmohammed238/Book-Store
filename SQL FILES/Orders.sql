create table BookStoreOrdersTable
(
         OrderId int not null identity (1,1) primary key,
		 ID INT NOT NULL,
		 FOREIGN KEY (ID) REFERENCES Usertbl(ID),
		 AddressId int
		 FOREIGN KEY (AddressId) REFERENCES Addresstbl(AddressId),
	     BookId INT NOT NULL
		 FOREIGN KEY (BookId) REFERENCES Books(BookId),
		 TotalPrice int,
		 BookQuantity int,
		 OrderDate Date
);
create PROC sp_AddingOrders
	@UserId INT,
	@AddressId int,
	@BookId INT ,
	@BookQuantity int
AS
	Declare @TotPrice int
BEGIN
	Select @TotPrice=DiscountPriceValue from Books where BookId = @BookId;
	IF (EXISTS(SELECT * FROM Books WHERE BookId = @BookId))
	begin
		IF (EXISTS(SELECT * FROM Usertbl WHERE ID = @UserId))
		Begin
		Begin try
			Begin transaction			
				INSERT INTO BookStoreOrdersTable(ID,AddressId,BookId,TotalPrice,BookQuantity,OrderDate)
				VALUES ( @UserId,@AddressId,@BookId,@BookQuantity*@TotPrice,@BookQuantity,GETDATE())
				Update Books set BookQuantity=BookQuantity-@BookQuantity
				Delete from Cart where BookId = @BookId and ID = @UserId
			commit Transaction
		End try
		Begin catch
			Rollback transaction
		End catch
		end
		Else
		begin
			Select 1
		end
	end 
	Else
	begin
			Select 2
	end	
END


create PROC sp_GetAllOrders(
	@UserId INT)
AS
BEGIN
	select 
		Books.BookId,Books.BookName,Books.AuthorName,Books.DiscountPriceValue,Books.OriginalPriceValue,Books.BookDescription,Books.BookImage,BookStoreOrdersTable.OrderId,BookStoreOrdersTable.OrderDate
		FROM Books
		inner join BookStoreOrdersTable
		on BookStoreOrdersTable.BookId=Books.BookId where BookStoreOrdersTable.ID=@UserId
END
select * from BookStoreOrdersTable

select * from Books 