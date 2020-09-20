USE [project]
GO

/****** Object:  Table [dbo].[JuridicalPersonContacts]    Script Date: 09.09.2020 17:15:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[JuridicalPersonContacts](
	[JuridicalPersonId] [uniqueidentifier] NOT NULL,
	[PhysicalPersonId] [uniqueidentifier] NOT NULL,
	[Position] [varchar](255) NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[JuridicalPersonContacts]  WITH CHECK ADD  CONSTRAINT [FK_JuridicalPersonContacts_JuridicalPersons] FOREIGN KEY([JuridicalPersonId])
REFERENCES [dbo].[JuridicalPersons] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[JuridicalPersonContacts] CHECK CONSTRAINT [FK_JuridicalPersonContacts_JuridicalPersons]
GO

ALTER TABLE [dbo].[JuridicalPersonContacts]  WITH CHECK ADD  CONSTRAINT [FK_JuridicalPersonContacts_PhysicalPersons] FOREIGN KEY([PhysicalPersonId])
REFERENCES [dbo].[PhysicalPersons] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[JuridicalPersonContacts] CHECK CONSTRAINT [FK_JuridicalPersonContacts_PhysicalPersons]
GO

