using Meteorology.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
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
            string appendBlobContent = null;
            try
            {
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(configuration.GetConnectionString("AzureStorage"));
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(configuration.GetConnectionString("ContainerName"));
                CloudAppendBlob cloudAppendBlob = cloudBlobContainer.GetAppendBlobReference(blobName);
                if (await cloudAppendBlob.ExistsAsync())
                {
                    appendBlobContent = await cloudAppendBlob.DownloadTextAsync();
                }
                else
                {
                    appendBlobContent = await GetZipAsync(blobName);
                }
                return appendBlobContent;
            }
            catch (Exception)
            {
                return appendBlobContent;
            }
        }

        /// <summary>
        /// Gets zip file from Storage.
        /// </summary>
        /// <returns>file from zip</returns>
        private async Task<string> GetZipAsync(string blobName)
        {
            string file = null;
            try
            {
                var relativePaths = blobName.Split('/');
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(configuration.GetConnectionString("AzureStorage"));
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference(configuration.GetConnectionString("ContainerName"));
                CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{relativePaths[0]}/{relativePaths[1]}/{configuration.GetConnectionString("Zip")}");
                using (var zippedBlob = new MemoryStream())
                {
                    await blockBlob.DownloadToStreamAsync(zippedBlob);
                    using (var zip = new ZipArchive(zippedBlob))
                    {
                        var files = zip.Entries.First(t=>t.FullName.Equals(relativePaths[2], StringComparison.InvariantCultureIgnoreCase));
                        using (StreamReader stream = new StreamReader(files.Open()))
                        {
                            file = stream.ReadToEnd();
                        }
                    }
                }
                return file;
            }
            catch (Exception)
            {
                return file;
            }
        }
    }
}
