﻿CREATE PROCEDURE [dbo].[spPost_Get]
@Id int
AS
begin
	SELECT post_id as Id, author_id, title, description, thumbnail
	from dbo.[Post]
	Where post_id = @Id

end