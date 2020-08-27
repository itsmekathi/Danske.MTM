USE MTM;
GO

SET IDENTITY_INSERT  [dbo].[TaxScheduleTypes] ON
INSERT INTO [dbo].[TaxScheduleTypes] (ID, [NAME])
VALUES (1, 'Daily'),(2,'Weekly'),(3,'Monthly'),(4,'Yearly')
SET IDENTITY_INSERT [dbo].[TaxScheduleTypes] OFF

GO

