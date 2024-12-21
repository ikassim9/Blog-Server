CREATE TABLE [dbo].[Post]
(
	[post_id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [title] VARCHAR(150) NOT NULL, 
    [description] TEXT NOT NULL, 
    [author_id] VARCHAR(50) NOT NULL, 
    [thumbnail] VARCHAR(250) NOT NULL
  
  
)
