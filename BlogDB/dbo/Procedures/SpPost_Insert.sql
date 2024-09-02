CREATE PROCEDURE [dbo].[spPost_Insert]
	 @UserId varchar(50),
	 @title varchar(50),
	 @description varchar(50)
AS
begin
	insert into dbo.[Post] (author_id, title, description)
	values (@UserId, @title, @description)
end

