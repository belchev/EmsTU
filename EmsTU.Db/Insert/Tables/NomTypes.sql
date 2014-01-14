print 'Insert NomTypes'
GO 

SET IDENTITY_INSERT NomTypes ON

INSERT INTO NomTypes(NomTypeId, Name, Alias, IsActive) VALUES (1,N'Тип заведение',N'BuildingTypes',1  )
INSERT INTO NomTypes(NomTypeId, Name, Alias, IsActive) VALUES (2,N'Тип кухня',N'KitchenTypes',1  )
INSERT INTO NomTypes(NomTypeId, Name, Alias, IsActive) VALUES (3,N'Тип музика',N'MusicTypes',1  )
INSERT INTO NomTypes(NomTypeId, Name, Alias, IsActive) VALUES (4,N'Повод',N'OccasionTypes',1  )
INSERT INTO NomTypes(NomTypeId, Name, Alias, IsActive) VALUES (5,N'Екстри',N'Extras',1  )
INSERT INTO NomTypes(NomTypeId, Name, Alias, IsActive) VALUES (6,N'Начин на плащане',N'PaymentTypes',1  )

SET IDENTITY_INSERT NomTypes OFF
GO 