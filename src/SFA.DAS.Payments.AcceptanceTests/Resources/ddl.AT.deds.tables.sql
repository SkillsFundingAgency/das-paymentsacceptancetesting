IF NOT EXISTS (SELECT [schema_id] FROM sys.schemas WHERE [name] = 'AT')
	BEGIN
		EXEC('CREATE SCHEMA AT')
	END
GO
----------------------------------------------------------------------------------------------------------------------------
-- TestRuns
----------------------------------------------------------------------------------------------------------------------------
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
----------------------------------------------------------------------------------------------------------------------------
-- Logs
----------------------------------------------------------------------------------------------------------------------------
IF NOT EXISTS (SELECT [object_id] FROM sys.tables WHERE [name] = 'Logs' AND [schema_id] = SCHEMA_ID('AT'))
	BEGIN
		CREATE TABLE [AT].[Logs](
			[LogId] [uniqueidentifier] NOT NULL DEFAULT (newid()),
			[RunId] [varchar](50) NOT NULL,
			[LogLevel] [int] NOT NULL,
			[LogDtTm] [datetime] NOT NULL DEFAULT(GETDATE()),
			[LogMessage] [nvarchar](max) NOT NULL,
			[ExceptionDetails] [nvarchar](max) NULL,
			[ScenarioTitle] [nvarchar](max) NULL
			PRIMARY KEY CLUSTERED 
			(
				[LogId] ASC
			)
		)
	END
GO

----------------------------------------------------------------------------------------------------------------------------
-- GetLastRunLogs
----------------------------------------------------------------------------------------------------------------------------
IF EXISTS (SELECT [object_id] FROM sys.procedures WHERE [name] = 'GetLastRunLogs')
	BEGIN
		DROP PROCEDURE GetLastRunLogs
	END
GO

CREATE PROCEDURE GetLastRunLogs
AS
SET NOCOUNT ON

	DECLARE @LastRunId varchar(50) = (SELECT TOP 1 RunId FROM AT.TestRuns ORDER BY StartDtTm DESC)

	SELECT * FROM AT.Logs WHERE RunId = @LastRunId ORDER BY LogDtTm DESC
GO

----------------------------------------------------------------------------------------------------------------------------
-- ReferenceData
----------------------------------------------------------------------------------------------------------------------------
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