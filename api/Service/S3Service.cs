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
    public async Task UploadImageAsync(string key, Stream fileStream, string contentType)
    {
        var request = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = key,
            InputStream = fileStream,
            ContentType = contentType,
            CannedACL = S3CannedACL.PublicRead
        };

        await _s3Client.PutObjectAsync(request);
    }

    public string GetImageUrl(string key)
    {
        return $"https://{_bucketName}.s3.amazonaws.com/{key}";
    }

    public async Task<Stream> GetImageAsync(string key)
    {
        var request = new GetObjectRequest
        {
            BucketName = _bucketName,
            Key = key
        };

        var response = await _s3Client.GetObjectAsync(request);
        return response.ResponseStream;
    }
}
