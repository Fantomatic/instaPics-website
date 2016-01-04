using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instaPics_website.Models
{
    //classe permettant de créer/récupérer les tables, blob et queue
    public static class CreateCloudAzure
    {
        public static CloudTable TableClient(string tableName)
        {
            string connectionString = CloudConfigurationManager.GetSetting(Constants.ConnStringKey);
            var storageAccount = CloudStorageAccount.Parse(connectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference(CloudConfigurationManager.GetSetting(tableName));
            table.CreateIfNotExists();

            return table;
        }

        public static CloudBlobContainer BlobContainer(string blobname)
        {
            string connectionString = CloudConfigurationManager.GetSetting(Constants.ConnStringKey);
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(CloudConfigurationManager.GetSetting(blobname));
            if (container.CreateIfNotExists())
            {
                container.SetPermissions(new BlobContainerPermissions()
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });
            }

            return container;
        }

        public static CloudQueue QueuOrder(string queue)
        {
            string connectionString = CloudConfigurationManager.GetSetting(Constants.ConnStringKey);
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var client = storageAccount.CreateCloudQueueClient();
            CloudQueue queueorder = client.GetQueueReference(CloudConfigurationManager.GetSetting(queue));
            queueorder.CreateIfNotExists();

            return queueorder;
        }
    }
}
