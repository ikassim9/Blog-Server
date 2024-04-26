CREATE PROCEDURE [dbo].[spUser_Get]
@Id varchar(50)
AS
begin
	Select name 
	from dbo.[User]
	where user_id = @Id

end