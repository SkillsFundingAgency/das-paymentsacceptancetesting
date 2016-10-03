IF NOT EXISTS (SELECT [schema_id] FROM sys.schemas WHERE [name] = 'AT')
	BEGIN
		EXEC('CREATE SCHEMA AT')
	END
GO

IF NOT EXISTS (SELECT [object_id] FROM sys.tables WHERE [name] = 'TestRuns' AND [schema_id] = SCHEMA_ID('AT'))
	BEGIN
		CREATE TABLE [AT].[TestRuns](
			[RunId] [varchar](50) NOT NULL,
			[StartDtTm] [datetime] NULL,
			[MachineName] [varchar](50) NULL,
			PRIMARY KEY CLUSTERED 
			(
				[RunId] ASC
			)
		)
	END
GO

IF NOT EXISTS (SELECT [object_id] FROM sys.tables WHERE [name] = 'Logs' AND [schema_id] = SCHEMA_ID('AT'))
	BEGIN
		CREATE TABLE [AT].[Logs](
			[LogId] [uniqueidentifier] NOT NULL CONSTRAINT [DF__Logs__LogId__145C0A3F]  DEFAULT (newid()),
			[RunId] [varchar](50) NOT NULL,
			[LogLevel] [int] NOT NULL,
			[LogDtTm] [datetime] NOT NULL,
			[LogMessage] [nvarchar](max) NOT NULL,
			[ExceptionDetails] [nvarchar](max) NULL,
			CONSTRAINT [PK__Logs__5E54864804C01630] PRIMARY KEY CLUSTERED 
			(
			[LogId] ASC
			)
		)
	END
GO