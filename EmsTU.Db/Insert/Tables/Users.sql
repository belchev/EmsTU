print 'Insert Users'
GO 

SET IDENTITY_INSERT [Users] ON

INSERT INTO [Users]
    ([UserId],[RoleId],[Username],[PasswordHash]                                                         ,[PasswordSalt]             ,[Fullname]      ,[Notes],[IsActive],[Version])
VALUES
    (1       ,1       ,N'admin'  ,N'AGgM2/KAha8GD7XAbQbofcUDaDswtFsiev7B+9t139yx3S/43vXEksQ/l9skx6tnWw==',N'4zct3KzwOlcCjLr+uKiHtw==',N'Администратор',N''    ,1         ,DEFAULT  ) --admin/@admin

INSERT INTO [Users]
    ([UserId],[RoleId],[Username],[PasswordHash]                                                         ,[PasswordSalt]             ,[Fullname]      ,[Notes],[IsActive],[Version])
VALUES
    (2       ,2       ,N'peter'  ,N'AGgM2/KAha8GD7XAbQbofcUDaDswtFsiev7B+9t139yx3S/43vXEksQ/l9skx6tnWw==',N'4zct3KzwOlcCjLr+uKiHtw==',N'Петър Петров',N''    ,1         ,DEFAULT  ) --peter/@peter

	INSERT INTO [Users]
    ([UserId],[RoleId],[Username],[PasswordHash]                                                         ,[PasswordSalt]             ,[Fullname]      ,[Notes],[IsActive],[Version])
VALUES
    (3       ,3       ,N'systemUser'  ,N'AGgM2/KAha8GD7XAbQbofcUDaDswtFsiev7B+9t139yx3S/43vXEksQ/l9skx6tnWw==',N'4zct3KzwOlcCjLr+uKiHtw==',N'Системен потребител',N''    ,1         ,DEFAULT  ) --systemUser/@admin

SET IDENTITY_INSERT [Users] OFF
GO
