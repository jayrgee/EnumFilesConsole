USE [mp3data]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Song]') AND type in (N'U'))
DROP TABLE [dbo].[Song]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Song](
	[SongId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](500) NOT NULL,
	[Artist] [nvarchar](500) NULL,
	[Album] [nvarchar](500) NULL,
	[Year] [smallint] NULL,
	[Genre] [nvarchar](500) NULL,
	[Size] [int] NULL,
	[TrackCount] [smallint] NULL,
	[TrackNum] [smallint] NULL,
	[BitRate] [smallint] NULL,
	[Frequency] [int] NULL,
	[Mode] [nvarchar](20) NULL,
	[Duration] [Time] NULL,
	[Filename] [nvarchar](500) NULL,
	[Filepath] [nvarchar](1000) NULL,
	[CreatedBy] [nvarchar](100) NOT NULL CONSTRAINT DF_Song_CreatedBy DEFAULT suser_sname(),
	[CreatedDate] [datetime] NOT NULL CONSTRAINT DF_Song_CreatedDate DEFAULT getdate(),
	CONSTRAINT [PK_Song] PRIMARY KEY CLUSTERED (SongId)
) ON [PRIMARY]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SongLog]') AND type in (N'U'))
DROP TABLE [dbo].[SongLog]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SongLog](
	[SongLogId] [int] IDENTITY(1,1) NOT NULL,
	[LogType] [tinyint] NOT NULL,
	[Filename] [nvarchar](500) NOT NULL,
	[Source] [nvarchar](100) NULL,
	[Message] [nvarchar](1000) NOT NULL,
	[Filepath] [nvarchar](1000) NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL CONSTRAINT DF_SongLog_CreatedBy DEFAULT suser_sname(),
	[CreatedDate] [datetime] NOT NULL CONSTRAINT DF_SongLog_CreatedDate DEFAULT getdate(),
	CONSTRAINT [PK_SongLog] PRIMARY KEY CLUSTERED (SongLogId),
	CONSTRAINT [CK_SongLog_LogType] CHECK (LogType IN (0,1,2))
) ON [PRIMARY]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SongPicture]') AND type in (N'U'))
DROP TABLE [dbo].[SongPicture]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SongPicture](
	[SongPictureId] [int] IDENTITY(1,1) NOT NULL,
	[Filepath] [nvarchar](1000) NOT NULL,
	[Picture] [varbinary](max) NULL,
	[CreatedBy] [nvarchar](100) NOT NULL CONSTRAINT DF_SongPicture_CreatedBy DEFAULT suser_sname(),
	[CreatedDate] [datetime] NOT NULL CONSTRAINT DF_SongPicture_CreatedDate DEFAULT getdate(),
	CONSTRAINT [PK_SongPicture] PRIMARY KEY CLUSTERED (SongPictureId),
) ON [PRIMARY]
GO
