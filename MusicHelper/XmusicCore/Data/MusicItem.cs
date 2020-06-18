using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HundredMilesSoftware.UltraID3Lib;
using System.IO;
using System.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using TagLib;

namespace XmusicCore
{
    [Serializable]
    public class MusicItem : DbMusicItem
    {
        public TagLib.File TagLibFile { get; }
        public UltraID3 UltraID3Item { get; }

        public MusicItem()
        {

        }

        public MusicItem(string filePath)
        {
            try
            {
                TagLibFile = TagLib.File.Create(filePath);
                UltraID3Item = new UltraID3();
                UltraID3Item.Read(filePath);
                Init(TagLibFile, UltraID3Item);
            }
            catch (Exception ex)
            {
            }
        }
        public MusicItem(IDataReader data)
        {
            Load(data);
        }

        public bool UltraID3HasData()
        {
            try
            {
                return UltraID3Item != null &&
                          UltraID3Item.Album != null &&
                          UltraID3Item.Artist != null &&
                          UltraID3Item.Title != null &&
                          UltraID3Item.Year?.ToString() != null &&
                          UltraID3Item.TrackNum?.ToString() != null &&
                          UltraID3Item.Genre != null;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool TagLibHasData()
        {
            try
            {
                return TagLibFile != null &&
                          TagLibFile.Tag.Album != null &&
                          TagLibFile.Tag.Artists != null &&
                          TagLibFile.Tag.Title != null &&
                          TagLibFile.Tag.Year.ToString() != null &&
                          TagLibFile.Tag.Track.ToString() != null &&
                          TagLibFile.Tag.Genres != null;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string GetJson()
        {
            dynamic jText = new JObject();
            jText.Album = UltraID3Item.Album;
            jText.Artist = UltraID3Item.Artist;
            jText.Title = UltraID3Item.Title;
            jText.Year = UltraID3Item.Year?.ToString();
            jText.TrackNum = UltraID3Item.TrackNum?.ToString();
            jText.Genre = UltraID3Item.Genre;
            return jText.ToString();
        }

        public MusicItem(TagLib.File f, UltraID3 u)
        {
            Init(f, u);
        }

        private void Init(TagLib.File f, UltraID3 u)
        {
            Artist = u.Artist ?? f.Tag.AlbumArtists.FirstOrDefault();
            Title = f.Tag.Title ?? u.Title;
            FullFileName = u.FileName;
            MachineName = Environment.UserName;
            FileName = Path.GetFileName(FullFileName);
        }
    }
}
