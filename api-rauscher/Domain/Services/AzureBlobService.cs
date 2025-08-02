using Azure.Storage.Blobs;
using Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Domain.Services
{
  public class AzureBlobService : IAzureBlobService
  {
    private readonly BlobContainerClient _containerClient;

    public AzureBlobService()
    {
      var connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
      var containerName = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONTAINER");

      if (string.IsNullOrWhiteSpace(connectionString) || string.IsNullOrWhiteSpace(containerName))
      {
        throw new InvalidOperationException("Azure storage configuration is missing.");
      }

      _containerClient = new BlobContainerClient(connectionString, containerName);
    }

    public async Task<bool> DeleteBlobAsync(string blobName)
    {
      if (string.IsNullOrWhiteSpace(blobName))
      {
        return false;
      }

      var response = await _containerClient.DeleteBlobIfExistsAsync(blobName);
      return response.Value;
    }
  }
}
