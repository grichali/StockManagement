using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;

public class S3Service
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;

    private readonly IConfiguration _configuration;
    public S3Service(IConfiguration configuration)
    {
        _configuration = configuration;

        // Initialize the S3 client with the region from configuration
        var config = new AmazonS3Config
        {
            RegionEndpoint = RegionEndpoint.GetBySystemName(_configuration["AWS:Region"])
        };

        _s3Client = new AmazonS3Client(config);
        _bucketName = _configuration["AWS:BucketName"];
    }

    // Upload an image to S3
    public async Task<string> UploadImageAsync(IFormFile imageFile, string prefix)
    {
        try
        {
            string key = $"{prefix}/{Guid.NewGuid()}_{imageFile.FileName}";
            using var fileStream = imageFile.OpenReadStream();

            var request = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = key,
                InputStream = fileStream,
                ContentType = imageFile.ContentType
            };

            await _s3Client.PutObjectAsync(request);
            return key;
        }
        catch (AmazonS3Exception ex)
        {
            throw new Exception($"AWS S3 error: {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error uploading file: {ex.Message}");
        }
    }

    public string GetImageUrl(string key)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = _bucketName,
            Key = key,
            Expires = DateTime.UtcNow.AddMinutes(10) 
        };

        return _s3Client.GetPreSignedURL(request);
    }

        public async Task DeleteImageAsync(string key)
    {
        var request = new DeleteObjectRequest
        {
            BucketName = _bucketName,
            Key = key
        };
     await _s3Client.DeleteObjectAsync(request);
    }
   

}
