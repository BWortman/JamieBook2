CREATE TABLE [dbo].[User](
	[UserId] BIGINT  IDENTITY (1, 1) NOT NULL,
	[Firstname] [nvarchar](50) NOT NULL,
	[Lastname] [nvarchar](50) NOT NULL,
	[ts] [rowversion] NOT NULL, 
    [Username] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_User] PRIMARY KEY ([UserId])
)
go

--alter table dbo.[User]
--	add constraint fk_User_aspnet_Users foreign key (UserId)
	--references dbo.aspnet_Users (UserId)
--go
