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
        //variables utilisé pour la vue pour avoir la liste de simages
        public List<StringImage> lstImageUserPath { get; set; }
        public List<StringImage> lstImageBNUserPath { get; set; }
        public List<StringImage> lstOtherImgPath { get; set; }

        public AccueilModel()
        {
            //déclaration des variables
            this.lstImageUserPath = new List<StringImage>();
            this.lstImageBNUserPath = new List<StringImage>();
            this.lstOtherImgPath = new List<StringImage>();
        }

        public void UploadImage(HttpPostedFileBase _file)
        {
            CloudBlobContainer container = CreateCloudAzure.BlobContainer(Constants.BlobImgConvertStringKey);

            //insertion dans le blob pour imgblob
            string extension = Path.GetExtension(_file.FileName);
            string guidFile = Guid.NewGuid().ToString() + extension;

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(guidFile);
            blockBlob.UploadFromStream(_file.InputStream);

            //insertion dans la table user/img

            CloudTable table = CreateCloudAzure.TableClient(Constants.TableUserImgStringKey);

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

            //ajout du message dans la queueOrder

            CloudQueue queue = CreateCloudAzure.QueuOrder(Constants.QueueConvertStringKey);
            queue.AddMessage(new CloudQueueMessage(guidFile));
        }

        public void listImg()
        {
            CloudTable table = CreateCloudAzure.TableClient(Constants.TableUserImgStringKey);

            TableQuery<UserImageEntity> query = new TableQuery<UserImageEntity>();

            IEnumerable<UserImageEntity> lstTableUserImage = table.ExecuteQuery(query);

            CloudBlobContainer blobContainer = CreateCloudAzure.BlobContainer(Constants.BlobImgConvertStringKey);

            IEnumerable<ImgFavEntity> lstFav = this.listFavorite();

            foreach (UserImageEntity userImage in lstTableUserImage)
            {
                //si l'image appartien à l'utilisateur de la session
                if (userImage.user == SessionUser.Username)
                {
                    // si l'image a été traité
                    if (userImage.imgOriginalThumb.Length > 0)
                    {
                        //recheche de l'image original en thumb
                        CloudBlockBlob blockBlobOriginal = blobContainer.GetBlockBlobReference(userImage.imgOriginalThumb);
                        if (blockBlobOriginal.Name.Length > 0)
                        {
                            StringImage mymg = new StringImage();
                            mymg.name = blockBlobOriginal.Name;
                            mymg.uri = blockBlobOriginal.Uri.ToString();
                            mymg.like = false;
                            this.lstImageUserPath.Add(mymg);
                        }
                    }

                    // si l'image a été traité
                    if (userImage.imgBNThumb.Length > 0)
                    {
                        //recheche de l'image noir et blanc en thumb avec la gestion si l'utilisateur aime ou non l'image
                        CloudBlockBlob blockBlobBN = blobContainer.GetBlockBlobReference(userImage.imgBNThumb);
                        if (blockBlobBN.Name.Length > 0)
                        {

                            bool like = false;

                            StringImage myimg = new StringImage();
                            myimg.name = blockBlobBN.Name;
                            myimg.uri = blockBlobBN.Uri.ToString();

                           foreach(ImgFavEntity myfav in lstFav)
                            {
                                if(myfav.NameImage == myimg.name && SessionUser.Username == myfav.Username)
                                {
                                    like = true;
                                    break;
                                }
                            }

                            myimg.like = like;
                            this.lstImageBNUserPath.Add(myimg);
                        }
                    }
                }
                else
                {
                    // si l'image a été traité
                    if (userImage.imgBNThumb.Length > 0)
                    {
                        //recheche de l'image noir et blanc en thumb avec la gestion si l'utilisateur aime ou non l'image
                        CloudBlockBlob blockBlobBN = blobContainer.GetBlockBlobReference(userImage.imgBNThumb);
                        if (blockBlobBN.Name.Length > 0)
                        {
                            bool like = false;

                            StringImage myimg = new StringImage();
                            myimg.name = blockBlobBN.Name;
                            myimg.uri = blockBlobBN.Uri.ToString();

                            foreach (ImgFavEntity myfav in lstFav)
                            {
                                if (myfav.NameImage == myimg.name && SessionUser.Username == myfav.Username)
                                {
                                    like = true;
                                    break;
                                }
                            }

                            myimg.like = like;
                            this.lstOtherImgPath.Add(myimg);
                        }
                    }
                }
            }
        }

        public void addFavorite(string nameimg)
        {
            CloudTable table = CreateCloudAzure.TableClient(Constants.TableImgFavStringKey);

            ImgFavEntity favImgToInsert = new ImgFavEntity()
            {
                RowKey = Guid.NewGuid().ToString(),
                PartitionKey = "INGESUP InstaPics",
                Username = SessionUser.Username,
                NameImage = nameimg
            };

            TableOperation insertOperation = TableOperation.InsertOrReplace(favImgToInsert);
            table.Execute(insertOperation);
        }

        public IEnumerable<ImgFavEntity> listFavorite()
        {
            CloudTable table = CreateCloudAzure.TableClient(Constants.TableImgFavStringKey);

            TableQuery<ImgFavEntity> query = new TableQuery<ImgFavEntity>();
            IEnumerable<ImgFavEntity> lstImgFav = table.ExecuteQuery(query);
            return lstImgFav;
            
        }

        public void removeFavorite(string nameimg)
        {

            CloudTable table = CreateCloudAzure.TableClient(Constants.TableImgFavStringKey);
            IEnumerable<ImgFavEntity> query = (from ImgFav in table.CreateQuery<ImgFavEntity>() where ImgFav.NameImage == nameimg select ImgFav);

            List<ImgFavEntity> imgSelect = query.ToList<ImgFavEntity>();

            TableOperation deleteOperation = TableOperation.Delete(imgSelect[0]);

            table.Execute(deleteOperation);
        }
    }
}
