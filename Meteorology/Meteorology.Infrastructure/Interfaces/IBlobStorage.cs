using System.Threading.Tasks;

namespace Meteorology.Infrastructure.Interfaces
{
    /// <summary>
    /// Interface for Blob Storage
    /// </summary>
    public interface IBlobStorage
    {
        /// <summary>
        /// Gets the content from Blob storage
        /// </summary>
        /// <param name="blobName">Name of the Blob</param>
        /// <returns>csv</returns>
        Task<string> GetContentAsync(string blobName);

        ///// <summary>
        ///// Gets the zip file from blob
        ///// </summary>
        ///// <param name="fileName">absolute path of file</param>
        ///// <returns>task</returns>
        //Task GetZipAsync(string blobName);
    }
}
