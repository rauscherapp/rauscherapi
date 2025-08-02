using System;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
  public class AzureBlobService : IAzureBlobService
  {
    private readonly BlobContainerClient _containerClient;

    public AzureBlobService(IConfiguration configuration)
    {
      var connectionString = configuration["AZURE_CONNECTION_STRING"] ?? configuration["connectionStringAzureFiles"];
      var containerName = configuration["AZURE_CONTAINER_NAME"] ?? configuration["containerName"];

      if (string.IsNullOrWhiteSpace(connectionString))
        throw new ArgumentNullException("AZURE_CONNECTION_STRING");
      if (string.IsNullOrWhiteSpace(containerName))
        throw new ArgumentNullException("AZURE_CONTAINER_NAME");

      _containerClient = new BlobContainerClient(connectionString, containerName);
    }

    public async Task DeleteAsync(string blobPath)
    {
      if (string.IsNullOrWhiteSpace(blobPath))
        throw new ArgumentException("Blob path cannot be null or empty.", nameof(blobPath));

      await _containerClient.DeleteBlobIfExistsAsync(blobPath);
    }
  }
}
