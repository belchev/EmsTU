print 'Finalize'
GO

print 'Set admin users to admin role'
INSERT INTO UserRoles (UserId, RoleId)
select UserId, 1 from users
	where Username in ('admin', 'peter', 'systemUser')
GO

