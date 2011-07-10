using EnumFilesConsole.Entities;
using FluentNHibernate.Mapping;

namespace EnumFilesConsole.Mappings
{
    public class SongLogMap : ClassMap<SongLog>
    {
        public SongLogMap()
        {
            Table("SongLog");
            Id(s => s.SongErrorId).GeneratedBy.Identity();
            Map(s => s.LogType);
            Map(s => s.Filename);
            Map(s => s.Filepath);
            Map(s => s.Source);
            Map(s => s.Message);
        }
    }
}
