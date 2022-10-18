using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CollectionsManager.BLL.Services.Interfaces
{
    public interface IImageStorageService
    {
        Task<string> UploadImageAsync(IFormFile file);
    }
}
