using System.Threading.Tasks;

namespace Domain.Interfaces
{
  public interface IAzureBlobService
  {
    Task<bool> DeleteBlobAsync(string blobName);
  }
}
