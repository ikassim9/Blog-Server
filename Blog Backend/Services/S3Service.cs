using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace Blog_Backend.Services;

public class S3Service : IS3Service
{

    private readonly IAmazonS3 _amazonS3;


    public S3Service(IAmazonS3 amazonS3)
    {
        _amazonS3 = amazonS3;
    }

    public async Task<string> UploadThumbnailAsync(
     IFormFile file,
     string bucketName)
    {
        var objectName = "";
        var fileName = Path.GetFileName(file.FileName);
        var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);

        objectName = $"{Guid.NewGuid()}.{fileName}";

        var uploadRequest = new TransferUtilityUploadRequest()
        {
            InputStream = memoryStream,
            Key = objectName,
            BucketName = bucketName

        };

        var transferUtility = new TransferUtility(_amazonS3);
        await transferUtility.UploadAsync(uploadRequest);
        var thumbnail = $"https://{bucketName}.s3.amazonaws.com/{objectName}";

        return thumbnail;
    }


    public async Task DeleteThumbnailAsync(string bucketName, string objectName)
    {

        var request = new DeleteObjectRequest
        {
            BucketName = bucketName,
            Key = objectName
        };

        await _amazonS3.DeleteObjectAsync(request);
    }

}
