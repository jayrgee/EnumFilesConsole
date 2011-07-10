namespace EnumFilesConsole.Entities
{
    public enum SongLogType : byte
    {
        Information = 0,
        Warning,
        Error
    }

    public class SongLog
    {
        public virtual int SongErrorId { get; private set; }
        public virtual byte LogType { get; set; }
        public virtual string Filename { get; set; }
        public virtual string Source { get; set; }
        public virtual string Message { get; set; }
        public virtual string Filepath { get; set; }
    }
}
