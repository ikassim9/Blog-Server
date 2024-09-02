CREATE PROCEDURE [dbo].[spPost_GetAll]
AS
begin
	SELECT post_id as Id, title, description, author_id as userId

	from dbo.[Post]

end