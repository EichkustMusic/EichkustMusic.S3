﻿using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;

namespace EichkustMusic.S3
{
    public class S3Storage : IS3Storage
    {
        private readonly AmazonS3Client _s3;

        public S3Storage(IConfigurationManager configuration)
        {
            var accessKey = configuration["S3:AccessKey"];
            var secretKey = configuration["S3:SecretKey"];
            var serviceUrl = configuration["S3:ServiceUrl"];

            if (accessKey == null || secretKey == null)
            {
                throw new Exception("Secret key or access key for S3 is null");
            }

            var s3Config = new AmazonS3Config()
            {
                ServiceURL = serviceUrl,
            };

            _s3 = new AmazonS3Client(accessKey, secretKey, s3Config);
        }

        public async Task<bool> DeleteFileAsync(string fileURL)
        {
            var splitedURL = fileURL.Split("/");

            var fileName = splitedURL[^1];
            var bucketName = splitedURL[^2];

            var deleteFileRequst = new DeleteObjectRequest()
            {
                BucketName = bucketName,
                Key = fileName,
            };

            var response = await _s3.DeleteObjectAsync(deleteFileRequst);

            return response.HttpStatusCode.IsSuccess();
        }

        public async Task<bool> DoesFileExistAsync(string fileURL)
        {
            var splitedURL = fileURL.Split("/");

            var fileName = splitedURL[^1];
            var bucketName = splitedURL[^2];

            var objectMetadataRequest = new GetObjectMetadataRequest()
            {
                BucketName = bucketName,
                Key = fileName
            };

            try
            {
                var response = await _s3.GetObjectMetadataAsync(objectMetadataRequest);

                return response.HttpStatusCode.IsSuccess();
            }
            catch
            {
                return false;
            }
        }

        public string GetPreSignedUploadUrl(string bucketName)
        {
            var objectName = Guid.NewGuid().ToString();

            var getPreSignedUrlRequest = new GetPreSignedUrlRequest()
            {
                BucketName = bucketName,
                Key = objectName,
                Expires = DateTime.UtcNow.AddMinutes(1),
            };

            return _s3.GetPreSignedURL(getPreSignedUrlRequest);
        }

        public async Task<bool> UploadFileAsync(string bucketName, string localFilePath)
        {
            var key = Guid.NewGuid().ToString();

            var PutFileRequest = new PutObjectRequest()
            {
                BucketName = bucketName,
                Key = key,
                FilePath = localFilePath
            };

            var response = await _s3.PutObjectAsync(PutFileRequest);

            return response.HttpStatusCode.IsSuccess();
        }

        public async Task<bool> UploadFileAsync(string bucketName, string localFilePath, string fileName)
        {
            var PutFileRequest = new PutObjectRequest()
            {
                BucketName = bucketName,
                Key = fileName,
                FilePath = localFilePath
            };

            var response = await _s3.PutObjectAsync(PutFileRequest);

            return response.HttpStatusCode.IsSuccess();
        }
    }
}
