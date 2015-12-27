using instaPics_website.Models.POCO;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace instaPics_website.Models
{
    public class AccueilModel
    {
        public void UploadImage(HttpPostedFileBase _file)
        { 
            // Get the blob client that allows us to talk to Azure Blob
            string connectionString = CloudConfigurationManager.GetSetting(Constants.ConnStringKey);
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // Get container reference to work on it
            CloudBlobContainer container = blobClient.GetContainerReference(CloudConfigurationManager.GetSetting(Constants.BlobImgConvertStringKey));
            if (container.CreateIfNotExists())
            {
                // If the container has just been created, set its permissions
                container.SetPermissions(new BlobContainerPermissions()
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });
            }

            if (_file != null)
            {
                //insertion dans le blob pour imgtoconvert
                string extension = Path.GetExtension(_file.FileName);
                string guidFile = Guid.NewGuid().ToString() + extension;

               CloudBlockBlob blockBlob = container.GetBlockBlobReference(guidFile);
                blockBlob.UploadFromStream(_file.InputStream);

                //insertion dans la table user/img
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
                CloudTable table = tableClient.GetTableReference(CloudConfigurationManager.GetSetting(Constants.TableUserImgStringKey));
                table.CreateIfNotExists();

                UserImageEntity userImgToInsert = new UserImageEntity()
                {
                    RowKey = Guid.NewGuid().ToString(),
                    PartitionKey = "INGESUP InstaPics",
                    user = SessionUser.Username,
                    imgOriginal = guidFile,
                    imgOriginalThumb = "",
                    imgBN = "",
                    imgBNThumb = ""
                };

                TableOperation insertOperation = TableOperation.InsertOrReplace(userImgToInsert);
                table.Execute(insertOperation);

                //creation de la queueOrder

                var client = storageAccount.CreateCloudQueueClient();
                var queue = client.GetQueueReference(CloudConfigurationManager.GetSetting(Constants.QueueConvertStringKey));
                queue.CreateIfNotExists();

                queue.AddMessage(new CloudQueueMessage(guidFile));

            }
        }
    }
}
