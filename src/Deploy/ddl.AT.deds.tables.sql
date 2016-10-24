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