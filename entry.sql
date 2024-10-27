-- Create the BlogDb database
CREATE DATABASE BlogDb;
GO

USE [BlogDB]
GO
/****** Object:  Table [dbo].[Post]    Script Date: 10/24/2024 10:24:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[post_id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](50) NOT NULL,
	[description] [varchar](50) NOT NULL,
	[author_id] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[post_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 10/24/2024 10:24:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [varchar](50) NOT NULL,
	[name] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[spPost_GetAll]    Script Date: 10/24/2024 10:24:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spPost_GetAll]
AS
begin
	SELECT post_id as Id, title, description

	from dbo.[Post]

end
GO
/****** Object:  StoredProcedure [dbo].[spPost_Insert]    Script Date: 10/24/2024 10:24:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spPost_Insert]
	 @UserId varchar(50),
	 @title varchar(50),
	 @description varchar(50)
AS
begin
	insert into dbo.[Post] (author_id, title, description)
	values (@UserId, @title, @description)
end
GO
/****** Object:  StoredProcedure [dbo].[spUser_Get]    Script Date: 10/24/2024 10:24:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUser_Get]
@Id varchar(50)
AS
begin
	Select name 
	from dbo.[User]
	where user_id = @Id

end
GO
/****** Object:  StoredProcedure [dbo].[spUser_Insert]    Script Date: 10/24/2024 10:24:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUser_Insert]
	 @UserId varchar(50),
	 @Name varchar(50)
AS
begin
	insert into dbo.[User] (user_id, name)
	values (@UserId, @Name)


end
GO
