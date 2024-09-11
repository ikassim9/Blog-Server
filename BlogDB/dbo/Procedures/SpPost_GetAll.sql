CREATE PROCEDURE [dbo].[spPost_GetAll]
AS
begin
	SELECT post_id as Id, title, description

	from dbo.[Post]

end