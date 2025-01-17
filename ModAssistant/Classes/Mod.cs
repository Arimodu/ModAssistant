using System;
using System.Collections.Generic;
using ModAssistant.Pages;

namespace ModAssistant
{
    [Serializable]
    public class Mod
    {
        public string name;
        public string version;
        public string gameVersion;
        public string _id;
        public string status;
        public string authorId;
        public string uploadedDate;
        public string updatedDate;
        public Author author;
        public string description;
        public string link;
        public string category;
        public DownloadLink[] downloads;
        public bool required;
        public Dependency[] dependencies;
        public List<Mod> Dependents = new List<Mod>();
        public Mods.ModListItem ListItem;

        [Serializable]
        public class Author
        {
            public string _id;
            public string username;
            public string lastLogin;
        }

        [Serializable]
        public class DownloadLink
        {
            public string type;
            public string url;
            public FileHashes[] hashMd5;
        }

        [Serializable]
        public class FileHashes
        {
            public string hash;
            public string file;
        }

        [Serializable]
        public class Dependency
        {
            public string name;
            public string _id;
            public Mod Mod;
        }
    }
}
