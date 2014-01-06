print 'BuildingUsers'
GO 

CREATE TABLE [dbo].[BuildingUsers] (
    [BuildingId]     INT            NOT NULL,
    [UserId]     INT            NOT NULL,
    CONSTRAINT [PK_BuildingUsers] PRIMARY KEY CLUSTERED ([BuildingId] ASC, [UserId] ASC),
    CONSTRAINT [FK_BuildingUsers_Buildings] FOREIGN KEY ([BuildingId]) REFERENCES [dbo].[Buildings] ([BuildingId]),
    CONSTRAINT [FK_BuildingUsers_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId])
);
GO