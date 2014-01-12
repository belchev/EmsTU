print 'BuildingKitchenTypes'
GO 

CREATE TABLE [dbo].[BuildingKitchenTypes] (
    [BuildingId]     INT            NOT NULL,
    [KitchenTypeId]     INT            NOT NULL,
    CONSTRAINT [PK_BuildingKitchenTypes] PRIMARY KEY CLUSTERED ([BuildingId] ASC, [KitchenTypeId] ASC),
    CONSTRAINT [FK_BuildingKitchenTypes_Buildings] FOREIGN KEY ([BuildingId]) REFERENCES [dbo].[Buildings] ([BuildingId]),
    CONSTRAINT [FK_BuildingKitchenTypes_KitchenTypes] FOREIGN KEY ([KitchenTypeId]) REFERENCES [dbo].[KitchenTypes] ([KitchenTypeId])
);
GO