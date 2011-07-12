using System.Drawing;
namespace EnumFilesConsole.Entities
{
    public class SongPicture
    {
        public virtual int SongPictureId { get; private set; }
        public virtual string Filepath { get; set; }
        public virtual Bitmap Picture { get; set; }
    }
}
