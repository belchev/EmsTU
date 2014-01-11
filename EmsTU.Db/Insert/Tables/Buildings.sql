print 'Insert Buildings'
GO 

SET IDENTITY_INSERT [Buildings] ON

INSERT INTO Buildings(BuildingId, Name, ImagePath, Slogan, WebSite, ModifyDate, DistrictId, MunicipalityId, SettlementId, Address, ContactName, ContactPhone, Info, WorkingTime, BuildingPhone, Price, SeatsInside, SeatsOutside, IsActive) 
VALUES (1, N'DEA restaurant & grill', N'storage\images\aaa.jpg', N'Y', N'Z', NULL, NULL, NULL, NULL, N'Драгалевци, ул. Нарцис 34', NULL, NULL, NULL, N'11:00 - 24:00', NULL, NULL, NULL, NULL, 1)

INSERT INTO Buildings(BuildingId, Name, ImagePath, Slogan, WebSite, ModifyDate, DistrictId, MunicipalityId, SettlementId, Address, ContactName, ContactPhone, Info, WorkingTime, BuildingPhone, Price, SeatsInside, SeatsOutside, IsActive) 
VALUES (2, N'ЮНИОР', N'storage\images\aab.jpg', N'B', N'C', NULL, NULL, NULL, NULL, N'Център, ул. Кракра 2А', NULL, NULL, NULL, N'18:00 - докато има клиенти', NULL, NULL, NULL, NULL, 1)

INSERT INTO Buildings(BuildingId, Name, ImagePath, Slogan, WebSite, ModifyDate, DistrictId, MunicipalityId, SettlementId, Address, ContactName, ContactPhone, Info, WorkingTime, BuildingPhone, Price, SeatsInside, SeatsOutside, IsActive) 
VALUES (3, N'The MED- гръцка и средиземноморска кухня', N'storage\images\aac.jpg', N'W', N'G', NULL, NULL, NULL, NULL, N'Мотописта, Бул. България 49А до ОМВ бензиностанция', NULL, NULL, NULL, N'пон.- 12-16, 12-24 всички останали дни', NULL, NULL, NULL, NULL, 1)

SET IDENTITY_INSERT [Buildings] OFF
GO
