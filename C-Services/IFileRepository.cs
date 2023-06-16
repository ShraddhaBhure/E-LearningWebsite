using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace C_Services
{
    public interface IFileRepository
    {
        string GetWebRootPath();
        string GetArticleFilesPath();
        Task<string> SaveFile(IFormFile file, string folderName, string subfolderName);
        void DeleteFile(string filePath);
    }
}
