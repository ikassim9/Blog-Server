CREATE PROCEDURE [dbo].[SpPost_Delete]
	 @postId varchar(50)
AS
begin
	Delete dbo.[Post]
	WHERE post_id = @postId
 end
