using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instaPics_website.Models
{
    public static class Constants
    {
        //constants pour le web.config
        public const string ConnStringKey = "StorageAccountString";
        public const string TableUserStringKey = "TableUserName";
        public const string BlobImgConvertStringKey = "BlobImgConvertName";
        public const string TableUserImgStringKey = "TableUserImgName";
        public const string TableImgFavStringKey = "TableImgFavName";
        public const string QueueConvertStringKey = "QueueConvertName";
    }
}
