using System;
using System.IO;
using System.Threading.Tasks;
using CollectionsManager.BLL.Services.ImageService.Options;
using CollectionsManager.BLL.Services.Interfaces;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace CollectionsManager.BLL.Services.ImageService
{
    public class CloudStorageService : IImageStorageService
    {
        private readonly StorageClient _storageClient;
        private readonly string _bucketName;

        public CloudStorageService(IOptions<GoogleCloudStorageOptions> options)
        {
            var credentials = GoogleCredential.FromFile(options.Value.PathToCredentialFile);

            _storageClient = StorageClient.Create(credentials);

            _bucketName = options.Value.BucketName;
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            var fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(file.FileName)}";

            var created = await _storageClient.UploadObjectAsync(_bucketName, fileName, null, stream);

            return created.MediaLink;
        }
    }
}
