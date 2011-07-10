namespace EnumFilesConsole.Entities
{
    public class Song
    {
        public virtual int SongId { get; private set; }
        public virtual string Title { get; set; }
        public virtual string Artist { get; set; }
        public virtual string Album { get; set; }
        public virtual short Year { get; set; }
        public virtual string Genre { get; set; }
        public virtual long Size { get; set; }
        public virtual short TrackCount { get; set; }
        public virtual short TrackNum { get; set; }
        public virtual short BitRate { get; set; }
        public virtual int Frequency { get; set; }
        public virtual string Mode { get; set; }
        public virtual System.TimeSpan Duration { get; set; }
        public virtual string Filename { get; set; }
        public virtual string Filepath { get; set; }
    }
}
