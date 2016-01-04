using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instaPics_website.Models.POCO
{
    //classe pour la page des images avec le nom de l'image, son uri et si l'utilisateur aime ou non l'image
    public class StringImage
    {
        public string name { get; set; }
        public string  uri { get; set; }
        public bool like { get; set; }
    }
}
