using Meteorology.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Threading.Tasks;

namespace Meteorology.Infrastructure
{
    /// <summary>
    /// Blob Storage Repository
    /// </summary>
    public class BlobStorage : IBlobStorage
    {
        /// <summary>
        /// Configuration variable
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Constructor to initialize variables
        /// </summary>
        /// <param name="configuration">configuration</param>
        public BlobStorage(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Gets the content from blob.
        /// </summary>
        /// <param name="blobName">Name of blob</param>
        /// <returns>csv data as string</returns>
        public async Task<string> GetContentAsync(string blobName)
        {
            try
            {
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(configuration.GetConnectionString("AzureStorage"));
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(configuration.GetConnectionString("ContainerName"));
                CloudAppendBlob cloudAppendBlob = cloudBlobContainer.GetAppendBlobReference(blobName);
                string appendBlobContent = await cloudAppendBlob.DownloadTextAsync();
                return appendBlobContent;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets zip file from Storage.
        /// </summary>
        /// <returns>file from zip</returns>
        public async Task GetZipAsync(string fileName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(configuration.GetConnectionString("AzureStorage"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(configuration.GetConnectionString("ContainerName"));
            //e.g. dockan/humidity/historical.zip
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
        }
    }
}
