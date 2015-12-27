using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace instaPics_website.Models.POCO
{
    public class UserImageEntity : TableEntity
    {
        public string user { get; set; }

        public string imgOriginal { get; set; }

        public string imgOriginalThumb { get; set; }

        public string imgBN { get; set; }

        public string imgBNThumb { get; set; }
    }
}
