using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using instaPics_website.Models;
using Microsoft.WindowsAzure.Storage.Table;
using instaPics_website.Models.POCO;

namespace instaPics_website.Models
{
    public class LoginModel
    {
        public UserEntity Connect(string _username)
        {
            CloudTable table = CreateCloudAzure.TableClient(Constants.TableUserStringKey);

            try
            {
                //récupération de l'utilisateur passé en paramètre
                IEnumerable<UserEntity> query = (from User in table.CreateQuery<UserEntity>() where User.Username == _username select User);

                List<UserEntity> allUser = query.ToList<UserEntity>();
                //s'il existe, on le retourne sinon on le crée puis on le retourne
                if (allUser.Count > 0)
                {
                    return allUser[0];
                }
                else
                {

                    UserEntity userToInsert = new UserEntity()
                    {
                        RowKey = Guid.NewGuid().ToString(),
                        PartitionKey = "INGESUP InstaPics",
                        Username = _username,
                    };

                    TableOperation insertOperation = TableOperation.InsertOrReplace(userToInsert);
                    table.Execute(insertOperation);
                    return userToInsert;
                }
            }
            catch
            {
                UserEntity userError = new UserEntity()
                {
                    RowKey = Guid.NewGuid().ToString(),
                    PartitionKey = "error",
                    Username = "error",
                };
                return userError;
            }
        }
    }
}
