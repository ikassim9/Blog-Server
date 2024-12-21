CREATE PROCEDURE [dbo].[spPost_Insert]
	 @UserId varchar(50),
	 @title varchar(50),
	 @description text,
	 @thumbnail varchar(250)
AS
begin
	insert into dbo.[Post] (author_id, title, description, thumbnail)
	values (@UserId, @title, @description, @thumbnail)
end

