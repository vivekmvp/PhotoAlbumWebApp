using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace PhotoAlbumWeb.AzureUtilities.Interfaces
{
    public interface IStorage
    {
        Task<BlobContainerClient> GetCloudBlobContainerAsync(string storageConnectionString, string containerName);
        Task<List<string>> GetAllImageUrls(string storageConnectionString, string containerName);
        Task<bool> UploadImage(string storageConnectionString, string containerName, Stream imageStream, string fileExtension);
    }
}
