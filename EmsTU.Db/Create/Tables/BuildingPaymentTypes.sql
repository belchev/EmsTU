print 'BuildingPaymentTypes'
GO 

CREATE TABLE [dbo].[BuildingPaymentTypes] (
    [BuildingId]     INT            NOT NULL,
    [PaymentTypeId]     INT            NOT NULL,
    CONSTRAINT [PK_BuildingPaymentTypes] PRIMARY KEY CLUSTERED ([BuildingId] ASC, [PaymentTypeId] ASC),
    CONSTRAINT [FK_BuildingPaymentTypes_Buildings] FOREIGN KEY ([BuildingId]) REFERENCES [dbo].[Buildings] ([BuildingId]),
    CONSTRAINT [FK_BuildingPaymentTypes_PaymentTypes] FOREIGN KEY ([PaymentTypeId]) REFERENCES [dbo].[PaymentTypes] ([PaymentTypeId])
);
GO