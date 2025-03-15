using Amazon.S3;

namespace Blog_Backend.Services;

public interface IS3Service
{

    /// <summary>
    /// Uploads new thumnail to s3 bucket
    /// </summary>
    /// <param name="file">The thumbnail to upload</param>
    /// <param name="bucketName">The bucket to upload thumbnail to</param>
    /// <returns></returns>
    Task<string> UploadThumbnailAsync(IFormFile file, string bucketName);
    Task DeleteThumbnailAsync(string bucketName, string objectName);
}
