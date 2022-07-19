using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using PhotoAlbumWeb.AzureUtilities.Interfaces;

namespace PhotoAlbumWeb.AzureUtilities
{
    public class StorageUtils : IStorage
    {
        public async Task<BlobContainerClient> GetCloudBlobContainerAsync(string storageConnectionString, string containerName)
        {
            try
            {
                BlobServiceClient serviceClient = new BlobServiceClient(storageConnectionString);
                BlobContainerClient containerClient = serviceClient.GetBlobContainerClient(containerName);
                await containerClient.CreateIfNotExistsAsync();
                return containerClient;
            }
            catch (Exception)
            {
                return null;
            }            
        }

        public async Task<List<string>> GetAllImageUrls(string storageConnectionString, string containerName)
        {
            BlobContainerClient containerClient = await GetCloudBlobContainerAsync(storageConnectionString, containerName);

            List<string> results = new List<string>();
            if(containerClient != null)
            {
                await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
                {
                    results.Add(
                        Path.Combine(
                            containerClient.Uri.AbsoluteUri,
                            blobItem.Name
                        )
                    );
                }
            }

            return results;
        }

        public async Task<bool> UploadImage(string storageConnectionString, string containerName, Stream imageStream, string fileExtension)
        {
            BlobContainerClient containerClient = await GetCloudBlobContainerAsync(storageConnectionString, containerName);
            if(containerClient != null)
            {
                string blobName = Guid.NewGuid().ToString().ToLower().Replace("-", String.Empty) +fileExtension;
                BlobClient blobClient = containerClient.GetBlobClient(blobName);
                await blobClient.UploadAsync(imageStream);
                if (blobClient.Uri.ToString().Length > 0)
                    return true;
            }
            return false;
        }
    }
}
