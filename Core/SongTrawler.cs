using System;
using System.Collections.Generic;
using System.IO;
using HundredMilesSoftware.UltraID3Lib;
using EnumFilesConsole;
using EnumFilesConsole.Entities;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;

namespace EnumFilesConsole.Core
{
    public class SongTrawler
    {
        static ISessionFactory sessionFactory;
        private ISession session;

        public SongTrawler()
        {
            // create our NHibernate session factory
            sessionFactory = CreateSessionFactory();
            session = sessionFactory.OpenSession();
        }

        public void GetSongs(string folder)
        {
            Console.WriteLine("Folder: {0}", folder);
            var files = Directory.GetFiles(folder, "*.mp3", SearchOption.TopDirectoryOnly);

       
            
            foreach (var file in files)
            {
                //Console.WriteLine("  file: {0}", Path.GetFileName(file));
                UltraID3 id3 = new UltraID3();
                try
                {
                    id3.Read(file);
                }
                catch (Exception e)
                {
                    LogSongMessage(new SongLog
                        {
                            LogType = (byte)SongLogType.Error,
                            Filename = Path.GetFileName(file),
                            Filepath = file,
                            Source = e.Source,
                            Message = e.Message
                        });
                    continue;
                }
                //Console.WriteLine("{0} | {1} | {2} | {3}", id3.Artist, id3.Album, id3.Title, file);

                //TODO: Log any handled errors in UltraID3 object
                ID3MetaDataException[] id3Errors = id3.GetExceptions(ID3ExceptionLevels.Error);
                if (id3Errors.Length > 0)
                {
                    foreach (ID3MetaDataException id3Exception in id3Errors)
                    {
                        LogSongMessage(new SongLog
                        {
                            LogType = (byte)SongLogType.Error,
                            Filename = Path.GetFileName(file),
                            Filepath = file,
                            Source = id3Exception.Source,
                            Message = id3Exception.Message
                        });
                    }
                }
                ID3MetaDataException[] id3Warnings = id3.GetExceptions(ID3ExceptionLevels.Warning);
                if (id3Warnings.Length > 0)
                {
                    foreach (ID3MetaDataException id3Exception in id3Warnings)
                    {
                        LogSongMessage(new SongLog
                        {
                            LogType = (byte)SongLogType.Warning,
                            Filename = Path.GetFileName(file),
                            Filepath = file,
                            Source = id3Exception.Source,
                            Message = id3Exception.Message
                        });
                    }
                }

                //GetPictureData();
                ID3FrameCollection pictureFrames = id3.ID3v2Tag.Frames.GetFrames(MultipleInstanceID3v2FrameTypes.ID3v22Picture);
                if (pictureFrames.Count > 0)
                {
                    LogSongMessage(new SongLog
                    {
                        LogType = (byte)SongLogType.Information,
                        Filename = Path.GetFileName(file),
                        Filepath = file,
                        Source = "GetSongs",
                        Message = String.Format("picture count:{0}",pictureFrames.Count.ToString())
                    });
                    foreach (ID3v22PictureFrame picFrame in pictureFrames)
                    {
                        SavePicture(new SongPicture
                        {
                            Filepath = file,
                            Picture = picFrame.Picture
                        });
                    }
                }

                // add to the database
                using (var transaction = session.BeginTransaction())
                {
                    var song = new Song
                    {
                        Title = id3.Title,
                        Artist = id3.Artist,
                        Album = id3.Album,
                        Year = (short)id3.Year.GetValueOrDefault(),
                        Genre = id3.Genre,
                        Size = (long)id3.Size,
                        TrackCount = (short)id3.TrackCount.GetValueOrDefault(),
                        TrackNum = (short)id3.TrackNum.GetValueOrDefault(),
                        BitRate = id3.FirstMPEGFrameInfo.Bitrate,
                        Frequency = id3.FirstMPEGFrameInfo.Frequency,
                        Mode = id3.FirstMPEGFrameInfo.Mode.ToString(),
                        Duration = id3.Duration,
                        Filename = Path.GetFileName(file),
                        Filepath = file
                    };

                    //session.SaveOrUpdate(song);
                    session.Save(song);
                    transaction.Commit();
                }
            }

            var folders = Directory.GetDirectories(folder);

            foreach (var subfolder in folders)
            {
                GetSongs(subfolder);
            }
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                    .ConnectionString(x => x
                        .FromConnectionStringWithKey("SongData"))
                        //.ShowSql()
                        )
                .Mappings(map =>
                    {
                        map.FluentMappings
                            .AddFromAssemblyOf<Song>();
                        map.FluentMappings
                            .AddFromAssemblyOf<SongLog>();
                    })
                .BuildSessionFactory();
        }

        private void LogSongMessage(SongLog e)
        {
            // Console.WriteLine("ERROR {0} | {1}", e.Message, e.Filepath);
            // add to the database
            using (var transaction = session.BeginTransaction())
            {
                session.Save(e);
                transaction.Commit();
            }
        }
        
        private void SavePicture(SongPicture sp)
        {
            // add to the database
            using (var transaction = session.BeginTransaction())
            {
                session.Save(sp);
                transaction.Commit();
            }
        }
    }
}
