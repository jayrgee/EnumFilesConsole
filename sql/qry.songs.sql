use mp3data
go
/*
TRUNCATE TABLE dbo.Song;
TRUNCATE TABLE dbo.SongLog;
*/
--SELECT COUNT(*) FROM dbo.song;
SELECT * FROM dbo.song

SELECT
	[MinCreatedDate] = MIN(s.CreatedDate)
,	[MaxCreatedDate] = MAX(s.CreatedDate)
,	[ElapsedTimeSeconds] = DATEDIFF(s,MIN(s.CreatedDate),MAX(s.CreatedDate))
,	[SongCount] = COUNT(*)
FROM dbo.song s

SELECT DISTINCT Artist FROM dbo.song ORDER BY Artist

SELECT DISTINCT Artist, Album FROM dbo.song ORDER BY Artist
SELECT DISTINCT Album, Artist FROM dbo.song ORDER BY Album

SELECT * FROM dbo.song
WHERE Artist = 'The Stooges'
ORDER BY Album, TrackNum;
