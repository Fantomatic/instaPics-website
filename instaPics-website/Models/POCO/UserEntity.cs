using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instaPics_website.Models.POCO
{
    //entity de l'utilisateur permettant l'ajout à la table
    public class UserEntity : TableEntity
    {
        public string Username { get; set; }
    }
}
