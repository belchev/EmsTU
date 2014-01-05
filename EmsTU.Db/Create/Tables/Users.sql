print 'Users'
GO 

CREATE TABLE [dbo].[Users] (
    [UserId]                INT                 NOT NULL IDENTITY(1,1),
	[RoleId]                INT                 NOT NULL,
    [Username]              NVARCHAR (200)      NOT NULL UNIQUE,
    [PasswordHash]          NVARCHAR (200)      NOT NULL,
    [PasswordSalt]          NVARCHAR (200)      NOT NULL,
    [Fullname]              NVARCHAR (200)      NULL,
    [Notes]                 NVARCHAR (MAX)      NULL,
    [Email]					NVARCHAR (200)		NULL,
    [IsActive]              BIT                 NOT NULL,
    [Version]               TIMESTAMP          NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserId] ASC)
);
GO

ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO

exec spDescTable  N'Users', N'Потребители.'
exec spDescColumn N'Users', N'UserId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Users', N'RoleId', N'Тип Роля.'
exec spDescColumn N'Users', N'Username', N'Потребителско име.'
exec spDescColumn N'Users', N'PasswordHash', N'Криптирана парола.'
exec spDescColumn N'Users', N'PasswordSalt', N'SALT за криптираната парола.'
exec spDescColumn N'Users', N'Fullname', N'Пълно име.'
exec spDescColumn N'Users', N'Notes', N'Бележки.'
exec spDescColumn N'Users', N'Email', 'Електронна поща'
exec spDescColumn N'Users', N'IsActive', N'Маркер за активност.'
exec spDescColumn N'Users', N'Version', N'Версия.'
GO
