using EnumFilesConsole.Entities;
using FluentNHibernate.Mapping;

namespace EnumFilesConsole.Mappings
{
    public class SongMap : ClassMap<Song>
    {
        public SongMap()
        {
            Table("Song");
            Id(s => s.SongId).GeneratedBy.Identity();
            Map(s => s.Title);
            Map(s => s.Album);
            Map(s => s.Year);
            Map(s => s.Artist);
            Map(s => s.Genre);
            Map(s => s.Size);
            Map(s => s.TrackCount);
            Map(s => s.TrackNum);
            Map(s => s.Duration)
                .CustomType("TimeAsTimeSpan");
            Map(s => s.BitRate);
            Map(s => s.Frequency);
            Map(s => s.Mode);
            Map(s => s.Filename);
            Map(s => s.Filepath);
        }
    }
}
