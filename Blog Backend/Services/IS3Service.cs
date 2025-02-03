using Amazon.S3;

namespace Blog_Backend.Services;

public interface IS3Service
{

    Task UploadFileAsync(IAmazonS3 client, string bucketName, string objectName, MemoryStream memoryStream);
    Task DeleteFile(IAmazonS3 client, string bucketName, string objectName);
}
