print 'BuildingExtras'
GO 

CREATE TABLE [dbo].[BuildingExtras] (
    [BuildingId]     INT            NOT NULL,
    [ExtraId]     INT            NOT NULL,
    CONSTRAINT [PK_BuildingExtras] PRIMARY KEY CLUSTERED ([BuildingId] ASC, [ExtraId] ASC),
    CONSTRAINT [FK_BuildingExtras_Buildings] FOREIGN KEY ([BuildingId]) REFERENCES [dbo].[Buildings] ([BuildingId]),
    CONSTRAINT [FK_BuildingExtras_Extras] FOREIGN KEY ([ExtraId]) REFERENCES [dbo].[Extras] ([ExtraId])
);
GO