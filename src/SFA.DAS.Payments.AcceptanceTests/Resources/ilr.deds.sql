IF NOT EXISTS (SELECT [object_id] FROM sys.tables WHERE [name]='FileDetails' AND [schema_id]=SCHEMA_ID('dbo'))
	BEGIN
		CREATE TABLE [dbo].[FileDetails](
			[ID] [int] IDENTITY(1,1) NOT NULL,
			[UKPRN] [int] NOT NULL,
			[Filename] [nvarchar](50) NULL,
			[FileSizeKb] [bigint] NULL,
			[TotalLearnersSubmitted] [int] NULL,
			[TotalValidLearnersSubmitted] [int] NULL,
			[TotalInvalidLearnersSubmitted] [int] NULL,
			[TotalErrorCount] [int] NULL,
			[TotalWarningCount] [int] NULL,
			[SubmittedTime] [datetime] NULL,
			[Success] [bit] NULL,
			CONSTRAINT [PK_dbo.FileDetails] UNIQUE NONCLUSTERED 
			(
				[UKPRN],
				[Filename],
				[Success]
			)
		)
	END
GO

IF NOT EXISTS (SELECT [object_id] FROM sys.tables WHERE [name]='ReferenceData' AND [schema_id]=SCHEMA_ID('AT'))
	BEGIN
		CREATE TABLE [AT].[ReferenceData](
			[ID] [int] IDENTITY(1,1) NOT NULL,
			[Key] [nvarchar] (100) NOT NULL,
			[Value] [nvarchar](500) NULL,
			[Type] [nvarchar](100) NULL,
			
			CONSTRAINT [PK_dbo.FileDetails] UNIQUE NONCLUSTERED 
			(
				[Key],
				[Type]
			)
		)
	END
GO