using CommentService.Infrastructure.Interfaces;

namespace CommentService.Infrastructure.Persistence
{
    public class LocalFileStorage : IFileStorageService
    {
        private readonly string _storageDirectory;

        public LocalFileStorage(string storageDirectory)
        {
            _storageDirectory = storageDirectory;
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
        {
            Console.WriteLine(Path.Combine(_storageDirectory, fileName));
            string filePath = GetFilePath(fileName);
            using (var file = File.Create(filePath))
            {
                await fileStream.CopyToAsync(file);
            }
            return filePath;
        }

        public async Task<Stream> DownloadFileAsync(string fileName)
        {
            string filePath = GetFilePath(fileName);
            return File.OpenRead(filePath);
        }

        public async Task<bool> DeleteFileAsync(string fileName)
        {
            string filePath = GetFilePath(fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }

        private string GetFilePath(string fileName)
        {
            return Path.Combine(_storageDirectory, fileName);
        }
    }

}
