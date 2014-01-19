SET IDENTITY_INSERT [dbo].[BuildingRequests] ON
GO
PRINT 'Inserting values into [BuildingRequests]'
INSERT INTO [BuildingRequests] ([BuildingRequestId],[BuildingName],[ContactName],[UserName],[Phone],[Email],[WebSite],[HasRegisteredUser],[HasRegisteredBuilding],[Version])VALUES(1,N'The MED - гръцка и средиземноморска кухня',N'Петър Петров',N'peter.petrov',N'0877 721 271',N'petur@abv.bg',N'www.facebook.com%2Fgroups%2F18904358',1,1,DEFAULT)
INSERT INTO [BuildingRequests] ([BuildingRequestId],[BuildingName],[ContactName],[UserName],[Phone],[Email],[WebSite],[HasRegisteredUser],[HasRegisteredBuilding],[Version])VALUES(2,N'Табиет - Лозенец',N'Ангел Ангелов',N'angel.angelov',N'(02) 969 69 6',N'angel@abv.bg',N'www.tabiet.com',1,1,DEFAULT)
INSERT INTO [BuildingRequests] ([BuildingRequestId],[BuildingName],[ContactName],[UserName],[Phone],[Email],[WebSite],[HasRegisteredUser],[HasRegisteredBuilding],[Version])VALUES(3,N'Rock''nRolla Bar & Club',N'Иван Иванов',N'ivan.ivanov',N'0888 131 318',N'ivan@abv.bg',N'www.rocknrolla.bg',1,1,DEFAULT)
INSERT INTO [BuildingRequests] ([BuildingRequestId],[BuildingName],[ContactName],[UserName],[Phone],[Email],[WebSite],[HasRegisteredUser],[HasRegisteredBuilding],[Version])VALUES(4,N'ПЕРИОРЕКСЕОС бистро',N'Иван Ангелов',N'ivan.angelov',N'0877 343 499',N'ivan.angelov@abv.bg',NULL,1,1,DEFAULT)
INSERT INTO [BuildingRequests] ([BuildingRequestId],[BuildingName],[ContactName],[UserName],[Phone],[Email],[WebSite],[HasRegisteredUser],[HasRegisteredBuilding],[Version])VALUES(5,N'ELIDIS ground level cafe',N'Атанас Атанасов',N'atanas.atanasov',N'(02) 479 55 40',N'atanas@abv.bg',NULL,1,1,DEFAULT)
INSERT INTO [BuildingRequests] ([BuildingRequestId],[BuildingName],[ContactName],[UserName],[Phone],[Email],[WebSite],[HasRegisteredUser],[HasRegisteredBuilding],[Version])VALUES(6,N'Piano Bar Poison',N'Атанас Иванов',N'atanas.ivanov',N'0877 764 766',N'atanas.ivanov@abv.bg',NULL,1,1,DEFAULT)
INSERT INTO [BuildingRequests] ([BuildingRequestId],[BuildingName],[ContactName],[UserName],[Phone],[Email],[WebSite],[HasRegisteredUser],[HasRegisteredBuilding],[Version])VALUES(7,N'Intrigue Bar & Dinner',N'Николай Костов',N'nikolay.kostov',N'(02) 851 08 61',N'nikolay@abv.bg',N'facebook.com/intrigue.bg',1,1,DEFAULT)
SET IDENTITY_INSERT [dbo].[BuildingRequests] OFF
GO

-----------------------

SET IDENTITY_INSERT [dbo].[Users] ON
GO
PRINT 'Inserting values into [Users]'
INSERT INTO [Users] ([UserId],[RoleId],[Username],[PasswordHash],[PasswordSalt],[Fullname],[Notes],[Email],[IsActive],[Version])VALUES(4,2,N'peter.petrov',N'AGbfHqJQlZIDdEtcqnAdwmZGr6UcKlA0lBGxCYXjaKj1HYLZojR8CXwZthdSezyn6A==',N'/YgVlGT6+DXE5XwpALgEzA==',N'Петър Петров',N'',N'petur@abv.bg',1,DEFAULT)
INSERT INTO [Users] ([UserId],[RoleId],[Username],[PasswordHash],[PasswordSalt],[Fullname],[Notes],[Email],[IsActive],[Version])VALUES(5,2,N'angel.angelov',N'ALCcudZ0fNz4pnzMBY4yxp9K3tcZNH+xSkHJxkVMFA7hbhK8kvd2YCIFq59ONSJVQw==',N'ub7e7XP0sI4va8p5QQd8eg==',N'Ангел Ангелов',N'',N'angel@abv.bg',1,DEFAULT)
INSERT INTO [Users] ([UserId],[RoleId],[Username],[PasswordHash],[PasswordSalt],[Fullname],[Notes],[Email],[IsActive],[Version])VALUES(6,2,N'ivan.ivanov',N'AP40Mk6juvt23M6FzG1EGExlY9gsShwWpWePGUsX1k03Bn2GU6yB/PqYFYvnLIoiEQ==',N'UMk50sSlo4onWroCauShfQ==',N'Иван Иванов',N'',N'ivan@abv.bg',1,DEFAULT)
INSERT INTO [Users] ([UserId],[RoleId],[Username],[PasswordHash],[PasswordSalt],[Fullname],[Notes],[Email],[IsActive],[Version])VALUES(7,2,N'ivan.angelov',N'AI2vqx5oQbT9lORFIhs/09mPUFTG3GxTmMUIrl2fn3KTMZmHzlzHqYxWWLuq/Hpb1g==',N'2YwImcht8IyXd/0zv/cb0Q==',N'Иван Ангелов',N'',N'ivan.angelov@abv.bg',1,DEFAULT)
INSERT INTO [Users] ([UserId],[RoleId],[Username],[PasswordHash],[PasswordSalt],[Fullname],[Notes],[Email],[IsActive],[Version])VALUES(8,2,N'atanas.atanasov',N'APBzt/Eoe5fzLZ3Ea7BjHtTixRDil8s2B7DTp46zJbfWukhBierG1ngxHyFKz7m6Tw==',N'1zoFE8TxfwHynZEIFY9d0A==',N'Атанас Атанасов',N'',N'atanas@abv.bg',1,DEFAULT)
INSERT INTO [Users] ([UserId],[RoleId],[Username],[PasswordHash],[PasswordSalt],[Fullname],[Notes],[Email],[IsActive],[Version])VALUES(9,2,N'atanas.ivanov',N'AGZqIEuXjmu7LDWQktV4Sdynzo3UHFZl6nTW47FOfmFdvreqnDN/to0VTiasy6RW0g==',N'EWPptBpkmyxlvs8NLueuSA==',N'Атанас Иванов',N'',N'atanas.ivanov@abv.bg',1,DEFAULT)
INSERT INTO [Users] ([UserId],[RoleId],[Username],[PasswordHash],[PasswordSalt],[Fullname],[Notes],[Email],[IsActive],[Version])VALUES(10,2,N'nikolay.kostov',N'ABrsA2vnTGRQ1U4e4MBniFsl7xQwC6W8xSrHo15oRoalJBGNHxO0OcaoZarUwdhbEQ==',N'iD99+BdoGtHKkwxDdu6mMg==',N'Николай Костов',N'',N'nikolay@abv.bg',1,DEFAULT)
SET IDENTITY_INSERT [dbo].[Users] OFF

--------------------------------------------

SET IDENTITY_INSERT [dbo].[Buildings] ON
GO
PRINT 'Inserting values into [Buildings]'
INSERT INTO [Buildings] ([BuildingId],[Name],[ImagePath],[Slogan],[WebSite],[ModifyUserId],[ModifyDate],[DistrictId],[MunicipalityId],[SettlementId],[Address],[ContactName],[ContactPhone],[Info],[WorkingTime],[BuildingPhone],[Price],[SeatsInside],[SeatsOutside],[IsActive],[IsDeleted],[Version])VALUES(1,N'The MED - гръцка и средиземноморска кухня',N'storage\images\thumbs\li5778py.jpg',N'ИСТИНСКИЯТ ГРЪЦКИ РЕСТОРАНТ В СОФИЯ',N'www.facebook.com%2Fgroups%2F18904358',4,'Jan 19 2014  6:07:47:533PM',22,205,4427,N'Мотописта, Бул. България 49А до ОМВ бензиностанция',N'Петър Петров',N'0877 721 271',N'На пристанището в Гърция терминът "парагадисиу", който с прости думи означава риба уловена тази вечер по начин подобен на ловенето с въдица е водещ за нас при избора на риби за нашите клиенти. Ако търсите уют и наистина вкусна храна, опитайте от магията на нашата кухня, насладете се на приятната обстановка и перфектното обслужване в една доста приветлива и незадължаваща обстановка. Работно време: Понеделник: 11:30 - 16:30ч. Вторник - Петък: 11:30 - 24:00ч. Събота и Неделя: 12:00 - 24:00ч.',N'пон.- 12-16, 12-24 всички останали дни',NULL,NULL,80,25,1,0,DEFAULT)
INSERT INTO [Buildings] ([BuildingId],[Name],[ImagePath],[Slogan],[WebSite],[ModifyUserId],[ModifyDate],[DistrictId],[MunicipalityId],[SettlementId],[Address],[ContactName],[ContactPhone],[Info],[WorkingTime],[BuildingPhone],[Price],[SeatsInside],[SeatsOutside],[IsActive],[IsDeleted],[Version])VALUES(2,N'Табиет - Лозенец',N'storage\images\thumbs\p476g8bs.jpg',N'ДОСТАВКА ПРЕЗ BGMENU.COM - 0700 10 400',N'www.tabiet.com',1,'Jan 19 2014  6:14:06:100PM',22,205,4427,N'Лозенец, бул. Джеймс Баучер 76',N'Ангел Ангелов',N'(02) 969 69 6',N'Ресторант градина „Табиет” е носител на престижната награда на Българска Хотелиерска и Ресторантьорска Асоциация БХРА- Ресторант на годината 2005 и на наградата – Ресторантьорска верига на годината 2006 заедно с първия ресторант ТАБИЕТ,който за съжаление прекрати своята дейност през лятото на 2012г. Ресторант Табиет предлага интернацинална кухня вече 10 години. Мотото му е : Добрата кухня винаги има значение. За толкова години е успял да се наложи с изключително разнообразното си меню и обичая да угажда на своите клиенти. Изградил е собствен стил, еднакво привлекателен за клиенти с различни вкусове и подходящ за провеждане на разнообразни мероприятия. Изобилието започва още със салатите – има всичко от обикновена шопска до рукола с френско козе сирене. Основните ястия са от различини видове месо. Предлага се богато рибно меню с пресни скариди, октопод, крака от раци и други - изложени в хладилна витрина, от която може сами да изберете. В Табиет',NULL,NULL,NULL,80,25,1,0,DEFAULT)
INSERT INTO [Buildings] ([BuildingId],[Name],[ImagePath],[Slogan],[WebSite],[ModifyUserId],[ModifyDate],[DistrictId],[MunicipalityId],[SettlementId],[Address],[ContactName],[ContactPhone],[Info],[WorkingTime],[BuildingPhone],[Price],[SeatsInside],[SeatsOutside],[IsActive],[IsDeleted],[Version])VALUES(3,N'Rock''nRolla Bar & Club',N'storage\images\thumbs\g2wt3as7.jpg',NULL,N'www.rocknrolla.bg',1,'Jan 19 2014  5:39:04:133PM',22,205,4427,N'Център, ул. Граф Игнатиев 1',N'Иван Иванов',N'0888 131 318',N'Преди три години в София отвори врати първият рок бар след дългогодишната инвазия на чалгите. За този период заведението не само се утвърди като едно от водещите в София, но и показа, че не всичко е чалга и че има и хора, които искат да слушат друга музика и да се забавляват като нормални хора. Липсата на позьорство, непринудената атмосфера и хубавата музика веднага бяха оценени и от чужденците, които станаха голяма част от редовните ни клиенти. Това е място за хората, които искат в уютна обстановка да изгледат концерта на любимия си изпълнител , мача на любимия си отбор, да потанцуват или просто да послушат хубава музика с бутилка бира без това да наруши сериозно ежедневния им бюджет. Ако обаче решат да изпробват и певческите си заложби то за целта е обособена специална зала за караоке, където скритите таланти могат пълноценно да се изявят. Много хора биха ни обвинили,че пускаме само хитове и музиката би им се сторила прекалено мека. Както обаче подсказва името на заведението ние с',NULL,NULL,NULL,80,25,1,0,DEFAULT)
INSERT INTO [Buildings] ([BuildingId],[Name],[ImagePath],[Slogan],[WebSite],[ModifyUserId],[ModifyDate],[DistrictId],[MunicipalityId],[SettlementId],[Address],[ContactName],[ContactPhone],[Info],[WorkingTime],[BuildingPhone],[Price],[SeatsInside],[SeatsOutside],[IsActive],[IsDeleted],[Version])VALUES(4,N'ПЕРИОРЕКСЕОС бистро',N'storage\images\thumbs\dtuvj45h.jpg',N'ГРЪЦКА СКАРА ГИРОС & СУВЛАКИ',NULL,1,'Jan 19 2014  5:40:28:467PM',22,205,4427,N'Студентски Град, 8-ми Декември 43',N'Иван Ангелов',N'0877 343 499',N'Periorexeos (Периорексос) е гръцко бистро за бързо похапване и доставка на висококачествени гироси и други подобни храни, както и място с предразполагащ светъл дървен интериор за срещи почти по всяко време на деня и нощта. В заведението може да консумирате и пълната гама продукти на Загорка.',N'10 am - 5 am',NULL,NULL,40,20,0,0,DEFAULT)
INSERT INTO [Buildings] ([BuildingId],[Name],[ImagePath],[Slogan],[WebSite],[ModifyUserId],[ModifyDate],[DistrictId],[MunicipalityId],[SettlementId],[Address],[ContactName],[ContactPhone],[Info],[WorkingTime],[BuildingPhone],[Price],[SeatsInside],[SeatsOutside],[IsActive],[IsDeleted],[Version])VALUES(5,N'ELIDIS ground level cafe',N'storage\images\thumbs\3myb2ynt.jpg',N'С НОВА ЗИМНА ГРАДИНА',NULL,1,'Jan 19 2014  5:41:54:703PM',22,205,4427,N'Център, бул. Скобелев 62',N'Атанас Атанасов',N'(02) 479 55 40',N'Страхотно кафе, паркинг, климатик, приятна музика, вкуснотийки, идеално място за частни партита и бизнес срещи.',N'09.00-24.00',NULL,NULL,40,30,0,0,DEFAULT)
INSERT INTO [Buildings] ([BuildingId],[Name],[ImagePath],[Slogan],[WebSite],[ModifyUserId],[ModifyDate],[DistrictId],[MunicipalityId],[SettlementId],[Address],[ContactName],[ContactPhone],[Info],[WorkingTime],[BuildingPhone],[Price],[SeatsInside],[SeatsOutside],[IsActive],[IsDeleted],[Version])VALUES(6,N'Piano Bar Poison',N'storage\images\thumbs\wn4zctnr.jpg',NULL,NULL,1,'Jan 19 2014  5:43:35:487PM',22,205,4427,N'Център, Аксаков 11А',N'Атанас Иванов',N'0877 764 766',N'Пиано бар/клуб, където не само може да се насладите на хубава музика и атмосфера, no и на суши. Място където да хапнете с приятели и да продължите да се забавлявате с изпълненията на професионални изпълнители.Изпълнител този уикенд:Катина и нейната група Darts.',NULL,NULL,NULL,150,NULL,0,0,DEFAULT)
INSERT INTO [Buildings] ([BuildingId],[Name],[ImagePath],[Slogan],[WebSite],[ModifyUserId],[ModifyDate],[DistrictId],[MunicipalityId],[SettlementId],[Address],[ContactName],[ContactPhone],[Info],[WorkingTime],[BuildingPhone],[Price],[SeatsInside],[SeatsOutside],[IsActive],[IsDeleted],[Version])VALUES(7,N'Intrigue Bar & Dinner',N'storage\images\thumbs\twldkhut.jpg',NULL,N'facebook.com/intrigue.bg',1,'Jan 19 2014  5:45:15:400PM',22,205,4427,N'Център, бул. Витоша 102',N'Николай Костов',N'(02) 851 08 61',N'Ние екипът на Intrigue Bar & Dinner ще опитаме да направим за вас една приятна атмосфера с класически италиански ястия и пица на пещ ,добре подбрани вина за всеки вкус,еспресо ристрето както и тирамису по автентична рецепта.',N'10:00 до 01:00',NULL,NULL,60,90,0,0,DEFAULT)
SET IDENTITY_INSERT [dbo].[Buildings] OFF

---------------------

SET IDENTITY_INSERT [dbo].[MenuCategories] ON
GO
PRINT 'Inserting values into [MenuCategories]'
INSERT INTO [MenuCategories] ([MenuCategoryId],[BuildingId],[Name],[IsActive],[Version])VALUES(1,1,N'Предястия',1,DEFAULT)
INSERT INTO [MenuCategories] ([MenuCategoryId],[BuildingId],[Name],[IsActive],[Version])VALUES(2,1,N'Салати',1,DEFAULT)
INSERT INTO [MenuCategories] ([MenuCategoryId],[BuildingId],[Name],[IsActive],[Version])VALUES(3,1,N'Морски ястия',1,DEFAULT)
INSERT INTO [MenuCategories] ([MenuCategoryId],[BuildingId],[Name],[IsActive],[Version])VALUES(4,2,N'Предястия',1,DEFAULT)
INSERT INTO [MenuCategories] ([MenuCategoryId],[BuildingId],[Name],[IsActive],[Version])VALUES(5,2,N'Основни ястия',1,DEFAULT)
INSERT INTO [MenuCategories] ([MenuCategoryId],[BuildingId],[Name],[IsActive],[Version])VALUES(6,2,N'Десерти',1,DEFAULT)
INSERT INTO [MenuCategories] ([MenuCategoryId],[BuildingId],[Name],[IsActive],[Version])VALUES(7,3,N'Предястия',1,DEFAULT)
INSERT INTO [MenuCategories] ([MenuCategoryId],[BuildingId],[Name],[IsActive],[Version])VALUES(8,3,N'Основни ястия',1,DEFAULT)
INSERT INTO [MenuCategories] ([MenuCategoryId],[BuildingId],[Name],[IsActive],[Version])VALUES(9,3,N'Десерти',1,DEFAULT)
INSERT INTO [MenuCategories] ([MenuCategoryId],[BuildingId],[Name],[IsActive],[Version])VALUES(10,4,N'Предястия',1,DEFAULT)
INSERT INTO [MenuCategories] ([MenuCategoryId],[BuildingId],[Name],[IsActive],[Version])VALUES(11,4,N'Основни ястия',1,DEFAULT)
INSERT INTO [MenuCategories] ([MenuCategoryId],[BuildingId],[Name],[IsActive],[Version])VALUES(12,4,N'Десерти',1,DEFAULT)
INSERT INTO [MenuCategories] ([MenuCategoryId],[BuildingId],[Name],[IsActive],[Version])VALUES(13,5,N'Предястия',1,DEFAULT)
INSERT INTO [MenuCategories] ([MenuCategoryId],[BuildingId],[Name],[IsActive],[Version])VALUES(14,5,N'Основни ястия',1,DEFAULT)
INSERT INTO [MenuCategories] ([MenuCategoryId],[BuildingId],[Name],[IsActive],[Version])VALUES(15,5,N'Десерти',1,DEFAULT)
INSERT INTO [MenuCategories] ([MenuCategoryId],[BuildingId],[Name],[IsActive],[Version])VALUES(16,6,N'Предястия',1,DEFAULT)
INSERT INTO [MenuCategories] ([MenuCategoryId],[BuildingId],[Name],[IsActive],[Version])VALUES(17,6,N'Основни ястия',1,DEFAULT)
INSERT INTO [MenuCategories] ([MenuCategoryId],[BuildingId],[Name],[IsActive],[Version])VALUES(18,6,N'Десерти',1,DEFAULT)
INSERT INTO [MenuCategories] ([MenuCategoryId],[BuildingId],[Name],[IsActive],[Version])VALUES(19,7,N'Предястия',1,DEFAULT)
INSERT INTO [MenuCategories] ([MenuCategoryId],[BuildingId],[Name],[IsActive],[Version])VALUES(20,7,N'Основни ястия',1,DEFAULT)
INSERT INTO [MenuCategories] ([MenuCategoryId],[BuildingId],[Name],[IsActive],[Version])VALUES(21,7,N'Десерти',1,DEFAULT)
SET IDENTITY_INSERT [dbo].[MenuCategories] OFF
GO

------------------------

SET IDENTITY_INSERT [dbo].[Menus] ON
GO
PRINT 'Inserting values into [Menus]'
INSERT INTO [Menus] ([MenuId],[MenuCategoryId],[Name],[Info],[Size],[Price],[ImagePath],[IsActive],[Version])VALUES(1,1,N'Дзадзики',N'гръцка млечна салата',150,3.800,N'storage\images\thumbs\bxfshizr.jpg',1,DEFAULT)
INSERT INTO [Menus] ([MenuId],[MenuCategoryId],[Name],[Info],[Size],[Price],[ImagePath],[IsActive],[Version])VALUES(2,1,N'Хтипити',N'салата от сирене',150,3.800,N'storage\images\thumbs\4r8zt18f.jpg',1,DEFAULT)
INSERT INTO [Menus] ([MenuId],[MenuCategoryId],[Name],[Info],[Size],[Price],[ImagePath],[IsActive],[Version])VALUES(3,1,N'Пържени тиквички',N'Хрупкави тиквички с млечен сос',300,5.900,N'storage\images\thumbs\z7a3ca4s.jpg',1,DEFAULT)
INSERT INTO [Menus] ([MenuId],[MenuCategoryId],[Name],[Info],[Size],[Price],[ImagePath],[IsActive],[Version])VALUES(4,2,N'Гръцка салата',N'домати, краставици, червен лук, маслини, сирене фета, риган',600,7.500,N'storage\images\thumbs\b3l7bu6d.jpg',1,DEFAULT)
INSERT INTO [Menus] ([MenuId],[MenuCategoryId],[Name],[Info],[Size],[Price],[ImagePath],[IsActive],[Version])VALUES(5,2,N'Салата раци',N'раци, селъри, майонеза, зелена салата, авокадо',500,9.000,N'storage\images\thumbs\gdi7te57.jpg',1,DEFAULT)
INSERT INTO [Menus] ([MenuId],[MenuCategoryId],[Name],[Info],[Size],[Price],[ImagePath],[IsActive],[Version])VALUES(6,2,N'Рока Пармезан',N'рукола, пармезан, чесън, балсамова заливка',600,11.900,N'storage\images\thumbs\uv4v25he.jpg',1,DEFAULT)
INSERT INTO [Menus] ([MenuId],[MenuCategoryId],[Name],[Info],[Size],[Price],[ImagePath],[IsActive],[Version])VALUES(7,3,N'Пиян октопод',N'октопод приготвен с вино',300,25.900,N'storage\images\thumbs\izajsj22.jpg',1,DEFAULT)
INSERT INTO [Menus] ([MenuId],[MenuCategoryId],[Name],[Info],[Size],[Price],[ImagePath],[IsActive],[Version])VALUES(8,3,N'Октопод на скара',N'',200,24.900,N'storage\images\thumbs\m3qrgkg4.jpg',1,DEFAULT)
INSERT INTO [Menus] ([MenuId],[MenuCategoryId],[Name],[Info],[Size],[Price],[ImagePath],[IsActive],[Version])VALUES(9,3,N'Скариди на скара /пържени',N'',200,21.900,N'storage\images\thumbs\hqgjdy65.jpg',1,DEFAULT)
SET IDENTITY_INSERT [dbo].[Menus] OFF
GO

-----------------------------

SET IDENTITY_INSERT [dbo].[Events] ON
GO
PRINT 'Inserting values into [Events]'
INSERT INTO [Events] ([EventId],[BuildingId],[Date],[Name],[Info],[ImagePath],[ImageThumbPath],[IsActive],[Version])VALUES(1,1,'Jan 22 2014 12:00:00:000AM',N'15% ВСЯКА СРЯДА!',N'Намалението важи за поръчки от основното меню и изключва поръчки чрез БГ Меню, както и промоционалното обедно меню.',N'storage\images\36rbsmse.jpg',N'storage\images\thumbs\36rbsmse.jpg',1,DEFAULT)
INSERT INTO [Events] ([EventId],[BuildingId],[Date],[Name],[Info],[ImagePath],[ImageThumbPath],[IsActive],[Version])VALUES(2,1,NULL,N'Бузуки трио',N'Бузуки парти с участието на гръцки певец с уникален глас! Елате да се насладите на по-добрата гръцка музика!',N'storage\images\tra1zghj.jpg',N'storage\images\thumbs\tra1zghj.jpg',1,DEFAULT)
SET IDENTITY_INSERT [dbo].[Events] OFF
GO

------------------------------

SET IDENTITY_INSERT [dbo].[Comments] ON
GO
PRINT 'Inserting values into [Comments]'
INSERT INTO [Comments] ([CommentId],[BuildingId],[Name],[Comment],[Date],[Version])VALUES(1,1,N'Alexandros',N'Iskame da blagodarime nash priateli-klienti ce idvat ot nova i da kaza che ot nova seasona ste ima mnogo priatni iznenadi.Ochakvame vi petak-sabato vof nash Gretska vecer sas ziva musika.','Jan 19 2014  5:45:15:400PM',DEFAULT)
INSERT INTO [Comments] ([CommentId],[BuildingId],[Name],[Comment],[Date],[Version])VALUES(2,1,N'Управител - управител',N'Честито Рождество Христово! Днес отново работим за Вас, можете да ни намерите на познатия адрес!','Jan 12 2014  5:45:15:400PM',DEFAULT)
INSERT INTO [Comments] ([CommentId],[BuildingId],[Name],[Comment],[Date],[Version])VALUES(3,1,N'Управител - управител',N'Информираме нашите клиенти, че в Понеделник ресторант The MED ще работи с нормално работно време и ще бъде отложен санитарния полуден за друг ден. Очакваме ви , за да празнуваме заедно Йорданов-ден! тел. за резервации +359877721271.','Jan 20 2014  5:45:15:400PM',DEFAULT)
SET IDENTITY_INSERT [dbo].[Comments] OFF
GO

--------------------------------

PRINT 'Inserting values into [Buildingusers]'
INSERT INTO [Buildingusers] ([BuildingId],[UserId])VALUES(1,4)
INSERT INTO [Buildingusers] ([BuildingId],[UserId])VALUES(2,5)
INSERT INTO [Buildingusers] ([BuildingId],[UserId])VALUES(3,6)
INSERT INTO [Buildingusers] ([BuildingId],[UserId])VALUES(4,7)
INSERT INTO [Buildingusers] ([BuildingId],[UserId])VALUES(5,8)
INSERT INTO [Buildingusers] ([BuildingId],[UserId])VALUES(6,9)
INSERT INTO [Buildingusers] ([BuildingId],[UserId])VALUES(7,10)

---------------------------------------

PRINT 'Inserting values into [Buildingnoms]'
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,1)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,20)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,25)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,28)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,31)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,41)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,42)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,60)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,61)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,64)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,68)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,94)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,95)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,99)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,102)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,109)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,132)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,136)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,137)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,144)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,146)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,147)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,148)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,150)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,156)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,158)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,159)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,161)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,166)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,173)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,174)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,181)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,186)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,198)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,201)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,210)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,221)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,222)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,224)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,228)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(1,229)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(2,1)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(2,30)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(2,40)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(2,46)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(2,49)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(2,52)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(2,64)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(2,68)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(2,95)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(2,97)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(2,103)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(2,112)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(2,139)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(2,142)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(2,148)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(2,162)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(2,167)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(2,186)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(2,187)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(2,210)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(2,221)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(2,224)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(2,228)
INSERT INTO [Buildingnoms] ([BuildingId],[NomId])VALUES(2,229)

---------------------------

SET IDENTITY_INSERT [dbo].[Albums] ON
GO
PRINT 'Inserting values into [Albums]'
INSERT INTO [Albums] ([AlbumId],[BuildingId],[Name],[IsActive],[Version])VALUES(1,1,N'Главен албум',1,DEFAULT)
INSERT INTO [Albums] ([AlbumId],[BuildingId],[Name],[IsActive],[Version])VALUES(2,1,N'Меню',1,DEFAULT)
INSERT INTO [Albums] ([AlbumId],[BuildingId],[Name],[IsActive],[Version])VALUES(3,1,N'Интериор',1,DEFAULT)
INSERT INTO [Albums] ([AlbumId],[BuildingId],[Name],[IsActive],[Version])VALUES(4,2,N'Главен албум',1,DEFAULT)
INSERT INTO [Albums] ([AlbumId],[BuildingId],[Name],[IsActive],[Version])VALUES(5,2,N'Меню',1,DEFAULT)
INSERT INTO [Albums] ([AlbumId],[BuildingId],[Name],[IsActive],[Version])VALUES(6,2,N'Интериор',1,DEFAULT)
INSERT INTO [Albums] ([AlbumId],[BuildingId],[Name],[IsActive],[Version])VALUES(7,3,N'Главен албум',1,DEFAULT)
INSERT INTO [Albums] ([AlbumId],[BuildingId],[Name],[IsActive],[Version])VALUES(8,3,N'Меню',1,DEFAULT)
INSERT INTO [Albums] ([AlbumId],[BuildingId],[Name],[IsActive],[Version])VALUES(9,3,N'Интериор',1,DEFAULT)
INSERT INTO [Albums] ([AlbumId],[BuildingId],[Name],[IsActive],[Version])VALUES(10,4,N'Главен албум',1,DEFAULT)
INSERT INTO [Albums] ([AlbumId],[BuildingId],[Name],[IsActive],[Version])VALUES(11,4,N'Меню',1,DEFAULT)
INSERT INTO [Albums] ([AlbumId],[BuildingId],[Name],[IsActive],[Version])VALUES(12,4,N'Интериор',1,DEFAULT)
INSERT INTO [Albums] ([AlbumId],[BuildingId],[Name],[IsActive],[Version])VALUES(13,5,N'Главен албум',1,DEFAULT)
INSERT INTO [Albums] ([AlbumId],[BuildingId],[Name],[IsActive],[Version])VALUES(14,5,N'Меню',1,DEFAULT)
INSERT INTO [Albums] ([AlbumId],[BuildingId],[Name],[IsActive],[Version])VALUES(15,5,N'Интериор',1,DEFAULT)
INSERT INTO [Albums] ([AlbumId],[BuildingId],[Name],[IsActive],[Version])VALUES(16,6,N'Главен албум',1,DEFAULT)
INSERT INTO [Albums] ([AlbumId],[BuildingId],[Name],[IsActive],[Version])VALUES(17,6,N'Меню',1,DEFAULT)
INSERT INTO [Albums] ([AlbumId],[BuildingId],[Name],[IsActive],[Version])VALUES(18,6,N'Интериор',1,DEFAULT)
INSERT INTO [Albums] ([AlbumId],[BuildingId],[Name],[IsActive],[Version])VALUES(19,7,N'Главен албум',1,DEFAULT)
INSERT INTO [Albums] ([AlbumId],[BuildingId],[Name],[IsActive],[Version])VALUES(20,7,N'Меню',1,DEFAULT)
INSERT INTO [Albums] ([AlbumId],[BuildingId],[Name],[IsActive],[Version])VALUES(21,7,N'Интериор',1,DEFAULT)
SET IDENTITY_INSERT [dbo].[Albums] OFF
GO

------------------------------------

SET IDENTITY_INSERT [dbo].[AlbumPhotos] ON
GO
PRINT 'Inserting values into [AlbumPhotos]'
INSERT INTO [AlbumPhotos] ([AlbumPhotoId],[AlbumId],[ImagePath],[ImageThumbPath],[Version])VALUES(1,1,N'storage\images\2wykqgrs.jpg',N'storage\images\thumbs\2wykqgrs.jpg',DEFAULT)
INSERT INTO [AlbumPhotos] ([AlbumPhotoId],[AlbumId],[ImagePath],[ImageThumbPath],[Version])VALUES(2,1,N'storage\images\xf32hbmc.jpg',N'storage\images\thumbs\xf32hbmc.jpg',DEFAULT)
INSERT INTO [AlbumPhotos] ([AlbumPhotoId],[AlbumId],[ImagePath],[ImageThumbPath],[Version])VALUES(3,1,N'storage\images\64tvdgsp.jpg',N'storage\images\thumbs\64tvdgsp.jpg',DEFAULT)
INSERT INTO [AlbumPhotos] ([AlbumPhotoId],[AlbumId],[ImagePath],[ImageThumbPath],[Version])VALUES(4,1,N'storage\images\s1mxmblz.jpg',N'storage\images\thumbs\s1mxmblz.jpg',DEFAULT)
INSERT INTO [AlbumPhotos] ([AlbumPhotoId],[AlbumId],[ImagePath],[ImageThumbPath],[Version])VALUES(5,1,N'storage\images\w4dg1t3g.jpg',N'storage\images\thumbs\w4dg1t3g.jpg',DEFAULT)
INSERT INTO [AlbumPhotos] ([AlbumPhotoId],[AlbumId],[ImagePath],[ImageThumbPath],[Version])VALUES(6,2,N'storage\images\87v2jugu.jpg',N'storage\images\thumbs\87v2jugu.jpg',DEFAULT)
INSERT INTO [AlbumPhotos] ([AlbumPhotoId],[AlbumId],[ImagePath],[ImageThumbPath],[Version])VALUES(7,2,N'storage\images\wrqknbkb.jpg',N'storage\images\thumbs\wrqknbkb.jpg',DEFAULT)
INSERT INTO [AlbumPhotos] ([AlbumPhotoId],[AlbumId],[ImagePath],[ImageThumbPath],[Version])VALUES(8,2,N'storage\images\pzehglp7.jpg',N'storage\images\thumbs\pzehglp7.jpg',DEFAULT)
INSERT INTO [AlbumPhotos] ([AlbumPhotoId],[AlbumId],[ImagePath],[ImageThumbPath],[Version])VALUES(9,2,N'storage\images\1dbqbtme.jpg',N'storage\images\thumbs\1dbqbtme.jpg',DEFAULT)
INSERT INTO [AlbumPhotos] ([AlbumPhotoId],[AlbumId],[ImagePath],[ImageThumbPath],[Version])VALUES(10,2,N'storage\images\jt82pyap.jpg',N'storage\images\thumbs\jt82pyap.jpg',DEFAULT)
INSERT INTO [AlbumPhotos] ([AlbumPhotoId],[AlbumId],[ImagePath],[ImageThumbPath],[Version])VALUES(11,2,N'storage\images\15kp3xbm.jpg',N'storage\images\thumbs\15kp3xbm.jpg',DEFAULT)
INSERT INTO [AlbumPhotos] ([AlbumPhotoId],[AlbumId],[ImagePath],[ImageThumbPath],[Version])VALUES(12,3,N'storage\images\m644utbc.jpg',N'storage\images\thumbs\m644utbc.jpg',DEFAULT)
INSERT INTO [AlbumPhotos] ([AlbumPhotoId],[AlbumId],[ImagePath],[ImageThumbPath],[Version])VALUES(13,3,N'storage\images\iucex4jc.jpg',N'storage\images\thumbs\iucex4jc.jpg',DEFAULT)
INSERT INTO [AlbumPhotos] ([AlbumPhotoId],[AlbumId],[ImagePath],[ImageThumbPath],[Version])VALUES(14,3,N'storage\images\mxqzmx2p.jpg',N'storage\images\thumbs\mxqzmx2p.jpg',DEFAULT)
INSERT INTO [AlbumPhotos] ([AlbumPhotoId],[AlbumId],[ImagePath],[ImageThumbPath],[Version])VALUES(15,3,N'storage\images\fdicm55r.jpg',N'storage\images\thumbs\fdicm55r.jpg',DEFAULT)
INSERT INTO [AlbumPhotos] ([AlbumPhotoId],[AlbumId],[ImagePath],[ImageThumbPath],[Version])VALUES(16,3,N'storage\images\ntknhj3v.jpg',N'storage\images\thumbs\ntknhj3v.jpg',DEFAULT)
SET IDENTITY_INSERT [dbo].[AlbumPhotos] OFF
GO