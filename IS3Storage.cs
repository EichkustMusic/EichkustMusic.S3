namespace EichkustMusic.S3
{
    public interface IS3Storage
    {
        public string GetPreSignedUploadUrl(string bucketName);

        public Task<bool> DeleteFileAsync(string fileURL);

        public Task<bool> DoesFileExistAsync(string fileURL);
    }
}
