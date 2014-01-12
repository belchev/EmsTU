print 'BuildingOccasionTypes'
GO 

CREATE TABLE [dbo].[BuildingOccasionTypes] (
    [BuildingId]     INT            NOT NULL,
    [OccasionTypeId]     INT            NOT NULL,
    CONSTRAINT [PK_BuildingOccasionTypes] PRIMARY KEY CLUSTERED ([BuildingId] ASC, [OccasionTypeId] ASC),
    CONSTRAINT [FK_BuildingOccasionTypes_Buildings] FOREIGN KEY ([BuildingId]) REFERENCES [dbo].[Buildings] ([BuildingId]),
    CONSTRAINT [FK_BuildingOccasionTypes_OccasionTypes] FOREIGN KEY ([OccasionTypeId]) REFERENCES [dbo].[OccasionTypes] ([OccasionTypeId])
);
GO