using System;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
  public class AzureBlobService : IAzureBlobService
  {
    private readonly BlobServiceClient _blobServiceClient;
    private readonly string _defaultContainerName;

    public AzureBlobService(IConfiguration configuration)
    {
      var connectionString = configuration["AZURE_CONNECTION_STRING"] ?? configuration["AzureBlobStorage"];
      var containerName = configuration["AZURE_CONTAINER_NAME"] ?? configuration["containerName"] ?? "imagens";

      if (string.IsNullOrWhiteSpace(connectionString))
        throw new ArgumentNullException("AZURE_CONNECTION_STRING");

      _blobServiceClient = new BlobServiceClient(connectionString);
      _defaultContainerName = containerName;
    }

    public async Task<bool> DeleteAsync(string blobPath)
    {
      if (string.IsNullOrWhiteSpace(blobPath))
        throw new ArgumentException("Blob path cannot be null or empty.", nameof(blobPath));

      var blobClient = ResolveBlobClient(blobPath);
      return await blobClient.DeleteIfExistsAsync();
    }

    private BlobClient ResolveBlobClient(string blobPath)
    {
      if (Uri.TryCreate(blobPath, UriKind.Absolute, out var blobUri))
      {
        var absolutePath = blobUri.AbsolutePath.Trim('/');
        var pathParts = absolutePath.Split(new[] { '/' }, 2, StringSplitOptions.RemoveEmptyEntries);

        if (pathParts.Length < 2)
          throw new ArgumentException("Blob URL must include container and blob name.", nameof(blobPath));

        var containerName = pathParts[0];
        var blobName = Uri.UnescapeDataString(pathParts[1]);

        return _blobServiceClient.GetBlobContainerClient(containerName).GetBlobClient(blobName);
      }

      var normalizedPath = blobPath.TrimStart('/');
      var containerPrefix = $"{_defaultContainerName}/";

      if (normalizedPath.StartsWith(containerPrefix, StringComparison.OrdinalIgnoreCase))
      {
        normalizedPath = normalizedPath.Substring(containerPrefix.Length);
      }

      return _blobServiceClient.GetBlobContainerClient(_defaultContainerName).GetBlobClient(normalizedPath);
    }
  }
}
