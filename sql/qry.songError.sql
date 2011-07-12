/*
TRUNCATE TABLE dbo.Song
TRUNCATE TABLE dbo.SongLog
TRUNCATE TABLE dbo.SongPicture
*/

SELECT
	[MinCreatedDate] = MIN(s.CreatedDate)
,	[MaxCreatedDate] = MAX(s.CreatedDate)
,	[ElapsedTimeSeconds] = DATEDIFF(s,MIN(s.CreatedDate),MAX(s.CreatedDate))
,	[SongCount] = COUNT(*)
,	[ErrorCount] = (SELECT COUNT(*) FROM dbo.SongLog WHERE LogType=2)
,	[WarningCount] = (SELECT COUNT(*) FROM dbo.SongLog WHERE LogType=1)
,	[InformationCount] = (SELECT COUNT(*) FROM dbo.SongLog WHERE LogType=0)
FROM dbo.song s;

SELECT * FROM dbo.SongLog
WHERE LogType = 0
ORDER BY LogType DESC, SongLogId
