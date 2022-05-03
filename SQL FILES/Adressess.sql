CREATE TABLE AddressType(AddressTypeId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	AddressType varchar(50)
);
INSERT INTO AddressType (AddressType) VALUES ('Home')
INSERT INTO AddressType (AddressType) VALUES ('Work')
INSERT INTO AddressType (AddressType) VALUES ('Other')
select *from Addresstbl
create table Addresstbl
(
    AddressId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	ID INT NOT NULL
	FOREIGN KEY (ID) REFERENCES Usertbl(ID),
	[Address] varchar(max) not null,
	City varchar(100),
	[State] varchar(100),
	AddressTypeId int
	FOREIGN KEY (AddressTypeId) REFERENCES AddressType(AddressTypeId)
);
create procedure Sp_AddAddress(
		@UserId int,
        @Address varchar(600),
		@City varchar(50),
		@State varchar(50),
		@AddressTypeId int	)		
As 
Begin
	IF (EXISTS(SELECT * FROM Usertbl WHERE @UserId = @UserId))
	Begin
	Insert into Addresstbl(ID,Address,City,State,AddressTypeId )
		values (@UserId,@Address,@City,@State,@AddressTypeId);
	End
	Else
	Begin
		Select 1
	End
End

create PROCEDURE sp_UpdateAddress
(
@AddressId int,
@Address varchar(max),
@City varchar(100),
@State varchar(100),
@AddressTypeId int	)

AS
BEGIN
       If (exists(Select * from Addresstbl where AddressId=@AddressId))
		begin
			UPDATE Addresstbl
			SET 
			Address= @Address, 
			City = @City,
			State=@State,
			AddressTypeId=@AddressTypeId 
				WHERE AddressId=@AddressId;
		 end
		 else
		 begin
		 select 1;
		 end
END 


create PROCEDURE sp_GetAllAddresses
AS
BEGIN
	 begin
	   SELECT * FROM Addresstbl ;
   	 end
End

Exec sp_GetAllAddresses



create PROCEDURE sp_GetAddressbyUserid
  (
  @UserId int
  )
AS
BEGIN
	   SELECT * FROM Addresstbl WHERE ID=@UserId;
	
END

Create proc sp_RemoveAddress(@UserId int)
As
begin
   IF EXISTS(SELECT * FROM Addresstbl WHERE ID=@UserId)
	begin
	DELETE FROM Addresstbl WHERE ID=@UserId;
	END
ELSE
BEGIN
SELECT 1;
END;
End;