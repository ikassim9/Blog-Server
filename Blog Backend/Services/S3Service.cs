using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace Blog_Backend.Services;

public class S3Service : IS3Service
{

    public async Task UploadFileAsync(
     IAmazonS3 client,
     string bucketName,
     string objectName,
     MemoryStream memoryStream)
    {
        var request = new PutObjectRequest
        {
            BucketName = bucketName,
            Key = objectName,
            InputStream = memoryStream

        };

        var uploadRequest = new TransferUtilityUploadRequest()
        {
            InputStream = memoryStream,
            Key = objectName,
            BucketName = bucketName

        };

        var transferUtility = new TransferUtility(client);
        await transferUtility.UploadAsync(uploadRequest);
          
    }

    public async Task DeleteFile(IAmazonS3 client, string bucketName, string objectName)
    {

        var request = new DeleteObjectRequest
        {
            BucketName = bucketName,
            Key = objectName
        };

        await client.DeleteObjectAsync(request);
    }

}
