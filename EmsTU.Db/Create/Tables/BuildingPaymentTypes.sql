print 'BuildingPaymentTypes'
GO 

CREATE TABLE [dbo].[BuildingPaymentTypes](
	[BuildingPaymentTypeId] [int] IDENTITY(1,1) NOT NULL,
	[BuildingId] [int] NOT NULL,
	[PaymentTypeId] [int] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_BuildingPaymentTypes] PRIMARY KEY CLUSTERED 
(
	[BuildingPaymentTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[BuildingPaymentTypes]  WITH CHECK ADD  CONSTRAINT [FK_BuildingPaymentTypes_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
GO
ALTER TABLE [dbo].[BuildingPaymentTypes] CHECK CONSTRAINT [FK_BuildingPaymentTypes_Buildings]
GO

ALTER TABLE [dbo].[BuildingPaymentTypes]  WITH CHECK ADD  CONSTRAINT [FK_BuildingPaymentTypes_PaymentTypes] FOREIGN KEY([PaymentTypeId])
REFERENCES [dbo].[PaymentTypes] ([PaymentTypeId])
GO
ALTER TABLE [dbo].[BuildingPaymentTypes] CHECK CONSTRAINT [FK_BuildingPaymentTypes_PaymentTypes]
GO

GO