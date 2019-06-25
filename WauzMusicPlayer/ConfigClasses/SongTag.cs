using System;

namespace WauzMusicPlayer
{
    class SongTag
    {
        private TagLib.File tagFile;

        public string Title { get; }
        public string Track { get; }
        public string Album { get; }
        public string Artist { get; }
        public string Year { get; }
        public string Length { get; }
        public string WzID { get; }

        public SongTag(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
                return;

            TagLib.Id3v2.Tag.DefaultVersion = 3;
            TagLib.Id3v2.Tag.ForceDefaultVersion = true;

            tagFile = TagLib.File.Create(filePath);
            tagFile.RemoveTags(tagFile.TagTypes & ~tagFile.TagTypesOnDisk);

            Title = tagFile.Tag.Title; 
            Track = tagFile.Tag.Track.ToString();
            Album = tagFile.Tag.Album;
            Artist = tagFile.Tag.FirstPerformer;
            Year = tagFile.Tag.Year.ToString();
            Length = tagFile.Properties.Duration.ToString(@"h\:mm\:ss");
        }

        public void Update(string Title, uint? Track, string Album, string Artist, uint? Year)
        {
            if (tagFile == null)
                return;

            if(Title != null)
                tagFile.Tag.Title = Title;
            if(Track != null)
                tagFile.Tag.Track = Track.GetValueOrDefault();
            if(Album != null)
                tagFile.Tag.Album = Album;
            if(Artist != null)
                tagFile.Tag.Performers = new string[] { Artist };
            if(Year != null)
                tagFile.Tag.Year = Year.GetValueOrDefault();

            tagFile.Save();
        }

        public static string Serialize(string filePath, bool createIfNotExists)
        {
            try
            {
                TagLib.File tagFile = TagLib.File.Create(filePath);

                string uuid = tagFile.Tag.Comment;

                if (string.IsNullOrWhiteSpace(uuid) || !uuid.StartsWith("wzID_"))
                {
                    if (createIfNotExists)
                    {
                        uuid = "wzID_" + Guid.NewGuid() + Guid.NewGuid();
                        tagFile.Tag.Comment = uuid;
                        tagFile.Save();
                    }
                    else
                    {
                        return "";
                    }
                }

                return uuid;
            }
            catch
            {
                return "";
            }
        }

    }
}
