CREATE DATABASE BOOKSTORE_db
CREATE TABLE Usertbl(ID int identity primary key,FullName VARCHAR(100) NOT NULL,EmailId VARCHAR(250) NOT NULL,[Password] varchar(30) NOT NULL,PhoneNumber BIGINT NOT NULL);
SELECT *FROM Usertbl;
-----STORE PROCEDURE FOR REGISTER USER--------
CREATE PROC sp_Adduser(@FullName VARCHAR(100),@EmailId VARCHAR(250),@Password varchar(30),@PhoneNumber bigint)
AS
BEGIN INSERT INTO Usertbl
values(@FullName,@EmailId,@Password,@PhoneNumber)
END;
------STORE PROC FOR LOGIN USER-----------
CREATE PROC sp_LoginUser(@EmailId Varchar(250), @Password VARCHAR(30))
AS
BEGIN 
SELECT EmailId,[Password] FROM Usertbl
WHERE EmailId=@EmailId AND [Password]=@Password
END;
-----------PROC FOR FORGOT PASSWORD USER-----
CREATE PROCEDURE sp_ForgotPassword(@EmailId Varchar(250))
AS
BEGIN
SELECT ID, EmailId FROM Usertbl
WHERE EmailId=@EmailId
END;
----------PROCEDURE RESET PASWORD ---
CREATE PROC sp_ResetPassword(@EmailId Varchar(250),@Password VARCHAR(100))
AS
BEGIN
 UPDATE Usertbl
 SET Password=@Password WHERE
	 EmailId=@EmailId
	 SELECT*FROM Usertbl WHERE EmailId=@EmailId;
END;