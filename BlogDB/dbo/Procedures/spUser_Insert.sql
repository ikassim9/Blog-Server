CREATE PROCEDURE [dbo].[spUser_Insert]
	 @UserId varchar(50),
	 @Name varchar(50)
AS
begin
	insert into dbo.[User] (user_id, name)
	values (@UserId, @Name)


end

