CREATE PROCEDURE [dbo].[spPost_Get]
@Id int
AS
begin
	SELECT post_id as Id, author_id as UserId, title, description, thumbnail
	from dbo.[Post]
	Where post_id = @Id

end