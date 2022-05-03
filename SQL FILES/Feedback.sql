create Table Feedbacktbl
(
	FeedbackId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Comment varchar(max) not null,
	Rating int not null,
	BookId int not null 
	FOREIGN KEY (BookId) REFERENCES Books(BookId),
	ID INT NOT NULL
	FOREIGN KEY (ID) REFERENCES Usertbl(ID),
);
select * from Feedback

create Proc sp_AddFeedback
(
	@Comment varchar(max),
	@Rating int,
	@BookId int,
	@UserId int
)
as
Declare @AverageRating int;
BEGIN
	IF (EXISTS(SELECT * FROM Feedback WHERE BookId = @BookId and ID=@UserId))
		select 1;
	Else
	Begin
		IF (EXISTS(SELECT * FROM Books WHERE BookId = @BookId))
		Begin  select * from Feedback
			Begin try
				Begin transaction
					Insert into Feedback(Comment, Rating, bookId, ID) values(@Comment, @Rating, @BookId, @UserId);		
					set @AverageRating = (Select AVG(Rating) from Feedback where BookId = @BookId);
					Update Books set RatingCount = @AverageRating,  TotalRating = totalRating + 1 
								 where  BookId = @BookId;
				Commit Transaction
			End Try
			Begin catch
				Rollback transaction
			End catch
		End
		Else
		Begin
			Select 2; 
		End
	End
END;

-- Procedure to Delete Feedback ---
create  proc DeleteFeedback
(
	@FeedbackId int,
	@UserId int
)
as
BEGIN
	Delete Feedback
		where
			FeedbackId = @FeedbackId
			and
			ID = @UserId;
END;

-- Procedure to Get All Feedback ---
create  Proc GetAllFeedback
(
	@BookId int
)
as
BEGIN
	Select FeedbackId, Comment, Rating, BookId, u.FullName
	From Usertbl u
	Inner Join Feedback f
	on f.ID = u.ID
	where
	 BookId = @BookId;
END;

-- Procedure to Update Feedback ---
create proc UpdateFeedback
(
	@Comment varchar(max),
	@Rating int,
	@BookId int,
	@FeedbackId int,
	@UserId int
)
as
Declare @AverageRating int;
BEGIN
	IF (EXISTS(SELECT * FROM Feedback WHERE FeedbackId = @FeedbackId))
		Begin
			Begin try
				Begin transaction
					Update Feedback set Comment = @Comment, Rating = @Rating, ID = @UserId, BookId = @BookId
									where FeedbackId = @FeedbackId;	
					set @AverageRating = (select AVG(Rating) from Feedback
									where BookId = @BookId);
					Update Books set RatingCount = @AverageRating,  TotalRating = totalRating+1 
								    where BookId = @BookId;
				Commit Transaction
			End Try
			Begin catch
				Rollback transaction
			End catch
		End
	Else
		Begin
			Select 2; 
		End
END;

