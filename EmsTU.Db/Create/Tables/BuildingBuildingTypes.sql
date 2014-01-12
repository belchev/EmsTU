print 'BuildingBuildingTypes'
GO 

CREATE TABLE [dbo].[BuildingBuildingTypes] (
    [BuildingId]     INT            NOT NULL,
    [BuildingTypeId]     INT            NOT NULL,
    CONSTRAINT [PK_BuildingBuildingTypes] PRIMARY KEY CLUSTERED ([BuildingId] ASC, [BuildingTypeId] ASC),
    CONSTRAINT [FK_BuildingBuildingTypes_Buildings] FOREIGN KEY ([BuildingId]) REFERENCES [dbo].[Buildings] ([BuildingId]),
    CONSTRAINT [FK_BuildingBuildingTypes_BuildingTypes] FOREIGN KEY ([BuildingTypeId]) REFERENCES [dbo].[BuildingTypes] ([BuildingTypeId])
);
GO