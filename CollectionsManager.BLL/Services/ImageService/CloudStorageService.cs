using System.IO;
using System.Threading.Tasks;
using CollectionsManager.BLL.Services.Interfaces;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CollectionsManager.BLL.Services.ImageService
{
    public class CloudStorageService : IImageStorageService
    {
        private readonly GoogleCredential _googleCredential;
        private readonly StorageClient _storageClient;
        private readonly string _bucketName;

        public CloudStorageService(IConfiguration config)
        {
            var path = config.GetSection("GoogleCredentialsFile").Value;

            _googleCredential = GoogleCredential.FromFile(path);
            _storageClient = StorageClient.Create(_googleCredential);

            _bucketName = "collections-images-bucket";
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);

            var created = await _storageClient.UploadObjectAsync(_bucketName, $"{file.Name}.jpg", null, stream);

            return created.MediaLink;
        }
    }
}
