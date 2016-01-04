using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instaPics_website.Models.POCO
{
    //classe pour la tale qui permet de relier un utilisateur à une image en favoris 
    public class ImgFavEntity: TableEntity
    {
        public string Username { get; set; }
        public string NameImage { get; set; }
    }
}
