using System.Threading.Tasks;

namespace Application.Interfaces
{
  public interface IAzureBlobService
  {
    Task DeleteAsync(string blobPath);
  }
}
