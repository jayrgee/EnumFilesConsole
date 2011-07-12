using EnumFilesConsole.Entities;
using FluentNHibernate.Mapping;

namespace EnumFilesConsole.Mappings
{
    public class SongPictureMap : ClassMap<SongPicture>
    {
        public SongPictureMap()
        {
            Table("SongPicture");
            Id(s => s.SongPictureId).GeneratedBy.Identity();
            Map(s => s.Filepath);
            Map(s => s.Picture);
        }
    }
}
