CREATE PROCEDURE [dbo].[SpPost_Update]
	 @postId varchar(50),
	 @title varchar(50),
	 @description text,
	 @thumbnail varchar(250)
AS
begin
	UPDATE dbo.[Post]
	SET title = @title, description = @description, thumbnail = @thumbnail
	WHERE post_id = @postId
 end
