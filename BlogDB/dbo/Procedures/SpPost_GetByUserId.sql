CREATE PROCEDURE [dbo].[SpPost_GetByUserId]
@Id varchar(50)
AS
begin
	SELECT post_id as Id, title, description, thumbnail
	from dbo.[Post]
	Where author_id = @Id

end